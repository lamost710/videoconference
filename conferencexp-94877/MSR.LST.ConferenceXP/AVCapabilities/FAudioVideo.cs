﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using System.Collections;

namespace MSR.LST.ConferenceXP
{
    public partial class FAudioVideo :CapabilityControl
    {
        #region Statics
        //定义拖动的委托事件
        public delegate void MDDelegate(object sender, MouseEventArgs e);
        public event MDDelegate MouseEvent;
        internal enum UIState
        {
            LocalVideoSendStopped = 1, // Stop local sending
            LocalVideoPlayStopped = 2, // Stop local or remote playing

            LocalAudioSendStopped = 4, // Stop local sending
            LocalAudioPlayStopped = 8, // Stop local or remote playing

            RemoteVideoStopped = 16, // Remote sending stopped
            RemoteAudioStopped = 32  // Remote sending stopped
        }

        private readonly string Status = Strings.Status;
        private readonly string StatusNormal = Strings.StatusNormal;

        private readonly string LocalAudioSendStoppedMsg = Strings.AudioIsNotBeingSent;
        private readonly string LocalVideoSendStoppedMsg = Strings.VideoIsNotBeingSent;

        private readonly string LocalAudioPlayStoppedMsg = Strings.IncomingAudioIsMuted;
        private readonly string LocalVideoPlayStoppedMsg = Strings.IncomingVideoIsPaused;

        private readonly string RemoteVideoSendStoppedMsg = Strings.IncomingVideoIsNotBeingSent;
        private readonly string RemoteAudioSendStoppedMsg = Strings.IncomingAudioIsNotBeingSent;
       
       

        private bool pbMiddleFillIsUnVisble;//video面板是否需要显示

        // images in the order they are in the imageListSendButtons of AV Buttons
        // Note: xxxOver means icon state when the mouse is over the button 
        public enum AVSendButtonState
        {
            VideoSend=0, VideoSendOver, VideoStopSending, VideoStopSendingOver,
            AudioSend, AudioSendOver, AudioStopSending, AudioStopSendingOver
        };

        // images in the order they are in the imageListPlayButtons of AV Buttons
        // Note: xxxOver means icon state when the mouse is over the button
        //       VideoExtyyy means the second button of the video player (it has two buttons that toggle) 
        public enum AVPlayButtonState
        {
            VideoPlay=0, VideoExtPlay, VideoPlayOver, VideoExtPlayOver, VideoStopPlaying, VideoExtStopPlaying,
            AudioPlay, AudioPlayOver, AudioStopPlaying, AudioStopPlayingOver
        };


        public enum ConfigButtonState { VideoConfig, VideoConfigOver };

        #endregion Statics

        #region Members

        // This form is shared with video and audio capability
        private VideoCapability videoCapability = null;
        private AudioCapability audioCapability = null;

        // By default the video plays when the capability is lauched
        // Boolean that tells whether or not the button video playing is pushed
        private bool isVideoButtonPushed = true;
        private bool isLocalVideoButtonPushed = true;

        // By default the audio plays when the capability is lauched
        // Boolean that tells whether or not the button audio playing is pushed
        private bool isAudioButtonPushed = true;
        private bool isLocalAudioButtonPushed = true;

        private delegate void UpdateUIStateHandler(int uiState);

        private string videoInfo = null;
        private string audioInfo = null;

        #endregion Members
        #region Public

        /// <summary>
        /// Position the volume slider according to the current volume
        /// </summary>
        /// <remarks>
        /// - AudioCapability MethodInvoke this method in RtpStream_FirstFrameReceived.
        /// - There is no parameter. The volume parameter is actually taken from the
        /// volume accessor in AudioCapability.
        /// </remarks>
        public void PositionVolumeSlider()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(_PositionVolumeSlider));
            }
            else
            {
                _PositionVolumeSlider();
            }
        }

        #endregion Public
        #region Private

        /// <summary>
        /// Filter the input image with a green color
        /// </summary>
        /// <remarks>
        /// The color supported so far are green and red
        /// </remarks>
        /// <param name="imageIn">Original image</param>
        /// <returns>Recolored image</returns>
        private Image ColorChange(Image imageIn, Color color)
        {
            // TODO: I was planning to use the red color filter for paused AV button.
            //       I finally decided to use an image list with all the states because 
            //       it looks better.
            //       I left the red color in case we see an use later on, otherwise
            //       the red color can be removed to decrease the amount of code.

            if (imageIn != null)
            {
                Image imageOut = (Image)imageIn.Clone();
                Graphics g = Graphics.FromImage(imageOut);

                ColorMatrix colorMatrix = null;

                // TODO: Find a better way to set the color matrix
                if (color == Color.Green)
                {
                    // Create a matrix that will convert the image to a green color
                    float[][] matrix = {
                                           new float[] {0.9f, 0, 0, 0, 0},
                                           new float[] {0, 0.9f, 0, 0, 0},
                                           new float[] {0, 0, 0.9f, 0, 0},
                                           new float[] {0, 0, 0, 0.9f, 0},
                                           new float[] {0, 0.15f, 0, 0, 0.9f}};

                    colorMatrix = new ColorMatrix(matrix);
                }
                else if (color == System.Drawing.Color.Red)
                {
                    // Create a matrix that will convert the image to a red color
                    float[][] matrix = {
                                           new float[] {0.9f, 0, 0, 0, 0},
                                           new float[] {0, 0.9f, 0, 0, 0},
                                           new float[] {0, 0, 0.9f, 0, 0},
                                           new float[] {0, 0, 0, 0.9f, 0},
                                           new float[] {0.9f, 0, 0, 0, 0.9f}};

                    colorMatrix = new ColorMatrix(matrix);
                }
                else
                {
                    throw new Exception(Strings.ColorNotSupported);
                }

                ImageAttributes imgAtt = new ImageAttributes();
                imgAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                TextureBrush tb = new TextureBrush(
                    imageOut,
                    new Rectangle(0, 0, imageOut.Width, imageOut.Height),
                    imgAtt);

                g.FillRectangle(tb, 0, 0, imageOut.Width, imageOut.Height);
                return imageOut;
            }
            else
            {
                return null;
            }
        }

        private void InitVideoCapability(ICapability capability)
        {
            if (videoCapability == null)
            {
                videoCapability = (VideoCapability)capability;

                this.Text = videoCapability.Name;

                // Unintuitively causes us to look at the vidSrcRatio and use it if it's been set
                TrimBlackFromVideo(base.Size);

                // TODO: Add a condition to not call InitVideoUI() if isFormLoaded is false to prevent
                // the form to show to early
                InitVideoUI();

                videoCapability.VideoWindowHandle = pbVideo.Handle;
                videoCapability.VideoWindowMessageDrain = pbVideo.Handle;
                videoCapability.ResizeVideoStream(pbVideo.Height, pbVideo.Width);
                //videoCapability.ResizeVideoStream(230, 200);

                // Set the UI borders so the video capability can calculate the
                // AV form size given some constrains due to the video ratio
                //videoCapability.UIBorderWidth = UIBorderWidth;
                //videoCapability.UIBorderHeight = UIBorderHeight;
            }
        }

        private void InitAudioCapability(ICapability capability)
        {
            if (audioCapability == null)
            {
                audioCapability = (AudioCapability)capability;

                // We want always to have the name of the video capability
                // if the form is shared with a video capability
                // => If the audio capability come after the video capability,
                // we want to overwrite with the name of the video capability.
                if (videoCapability != null)
                {
                    this.Text = videoCapability.Name;
                }

                // TODO: Add a condition to not call InitVideoUI() if isFormLoaded is false to prevent
                // the form to show to early
                InitAudioUI();

                // Set the UI borders so the audio capability can calculate the
                // AV form size given some constrains due to the video ratio
                //audioCapability.UIBorderWidth = UIBorderWidth;
                //audioCapability.UIBorderHeight = UIBorderHeight;
            }
        }

        /// <summary>
        /// This method reposition the AV buttons given the visibility of the tabs
        /// </summary>
        /// <remarks>
        /// The method assumes the following order:
        /// pnlLocalVideo, pnlLocalAudio, pnlLocalVideoConfig, pnlSeparatorSender
        /// pnlVideo, pnlAudio, pnlSeparatorPlayer, lblInfo
        /// </remarks>
        private void RepositionAVButtons()
        {
            // Note: The code below is also positioning invisible panel which is fine (otherwise we need 
            //       to add if statements)

            // Check if we need a separator after the Send buttons
            // Note: The separator is invisible by default
            // TODO: We only check about visibility of buttons before the separation, but not after
            ////       We should consider this case too
            //pnlSeparatorSender.Visible = ((pnlLocalVideo.Visible) || (pnlLocalAudio.Visible) || (pnlLocalVideoConfig.Visible));

            ////// Check if we need a separator after the Play buttons
            ////// Note: The separator is invisible by default
            ////// TODO: We only check about visibility of buttons before the separation, but not after
            //////       We should consider this case too
            //pnlSeparatorPlayer.Visible = ((pnlVideo.Visible) || (pnlAudio.Visible));

            //// No work needed for pnlLocalVideo: it is already placed correctly (1st one)

            //// Sender's buttons
            //pnlLocalAudio.Left = pnlLocalVideo.Left + (pnlLocalVideo.Visible ? pnlLocalVideo.Width : 0);
            ////pnlLocalVideoConfig.Left = pnlLocalAudio.Left + (pnlLocalAudio.Visible ? pnlLocalAudio.Width : 0);
            ////pnlSeparatorSender.Left = pnlLocalVideoConfig.Left + (pnlLocalVideoConfig.Visible ? pnlLocalVideoConfig.Width : 0);
            //pnlSeparatorSender.Left = pnlLocalAudio.Left + (pnlLocalAudio.Visible ? pnlLocalAudio.Width : 0);

            //// Player's buttons
            //pnlVideo.Left = pnlSeparatorSender.Left + (pnlSeparatorSender.Visible ? pnlLocalAudio.Width : 0);
            //pnlAudio.Left = pnlVideo.Left + (pnlVideo.Visible ? pnlVideo.Width : 0);
            //pnlSeparatorPlayer.Left = pnlAudio.Left + (pnlAudio.Visible ? pnlAudio.Width : 0);

            //// General Info label
            //lblInfo.Left = pnlSeparatorPlayer.Left + (pnlSeparatorPlayer.Visible ? pnlSeparatorPlayer.Width : 0);
        }

        /// <summary>
        /// Initialize the video specific part of the UI when the video capability is added
        /// </summary>
        private void InitVideoUI()
        {
            // Show the video play/stop button and the video tab if sender
            pnlVideo.Visible = true;

            if (videoCapability != null)
            {
                if (!videoCapability.IsPlaying)
                {
                    // Do not show controls for managing playing video if we're not playing
                    pnlVideo.Visible = false;
                }

                if (videoCapability.IsSender)
                {
                    //pbVideoTab.Visible = true;
                    pnlLocalVideo.Visible = true;
                    //pnlLocalVideoConfig.Visible = true;
                }
                else
                {
                    // Since there is no local video buttons, place the remote
                    // video control on the left
                    //pnlVideo.Left = 0;
                }

                // Test if the video capability is sharing a form with a audio capability
                if (!videoCapability.UsesSharedForm)
                {
                    // Reposition the info label and display a message
                    //pnlVideo.Left = 0;
                    //lblInfo.Left = pnlVideo.Left + pnlVideo.Width;
                }

            }

            // Needs to be called after setting the visibility of the panels
            RepositionAVButtons();
        }

        /// <summary>
        /// Initialize the audio specific part of the UI when the audio capability is added
        /// </summary>
        private void InitAudioUI()
        {
            // TODO: This code below could be removed or replaced by just showing the control disabled
            //       In fact, PositionVolumeSlider is already taking care of showing/enabling the trackbar control
            //       at the right time when you actually can control the volume... Enabling right here might be too early
            //       in some case. => Remove this code below and see if it still work correctly. 
            if (audioCapability.IsSender)
            {
                pnlLocalAudio.Visible = true;
            }

            if (audioCapability.IsPlaying)
            {
                tbVolume.Visible = true;
                // Show the speaker on/off button and volume slider
                pnlAudio.Visible = true;
            }

            // Test if the audio capability is sharing a form with a video capability
            if (!audioCapability.UsesSharedForm)
            {
                // We have audio only so we can fix the form size to a small one
                //this.MinimumSize = new Size(200, pbMiddleFill.Height);
                //this.MaximumSize = new Size(SystemInformation.WorkingArea.Width, pbMiddleFill.Height + Height - ClientSize.Height);

                //// Reposition the info label and display a message
                //pnlAudio.Left = 0;

            }
            else // We are in sharing a form with video, so we need to leave room for the video
            // button that has to be located on the left
            {
                // TODO: This does not cover all the starting cases and needs to be cleaned-up
                //pnlAudio.Left = pnlLocalVideo.Width;

                //if ((videoCapability != null) && (!videoCapability.IsSender))
                //{
                //    pnlAudio.Left = pnlVideo.Width;
                //}
            }

            //lblInfo.Left = pnlAudio.Left + pnlAudio.Width;

            // Needs to be called after setting the visibility of the panels
            RepositionAVButtons();
        }

        /// <summary>
        /// Uninitialize the video specific part of the UI when the video capability is removed
        /// </summary>
        private void UninitVideoUI()
        {
            // Hide the video play/stop button and the video tab if displayed
            //pbVideoTab.Visible = false;
            pnlVideo.Visible = false;
            pnlLocalVideo.Visible = false;
            //pnlLocalVideoConfig.Visible = false;

            // Needs to be called after setting the visibility of the panels
            RepositionAVButtons();
        }

        /// <summary>
        /// Uninitialize the audio specific part of the UI when the audio capability is removed
        /// </summary>
        private void UninitAudioUI()
        {
            // Hide the speaker on/off button and volume slider
            tbVolume.Visible = false;
            pnlAudio.Visible = false;
            pnlLocalAudio.Visible = false;

            // Needs to be called after setting the visibility of the panels
            RepositionAVButtons();
        }
        internal void TrimBlackFromVideo(System.Drawing.Size intendedSize)
        {
            // If we don't yet know the aspect ratio of the video, just keep what the system
            //  gave us.  We'll come back and resize later when we know the video aspect ratio.
            if (videoCapability == null || videoCapability.vidSrcRatio <= 0)
            {
                if (base.Size != intendedSize)
                    base.Size = intendedSize;
                return;
            }

            // videoWidth/videoHight represents the width and height of the video inside the form
            //   which is constraint by the source ratio

            // formWidth/formHeight represents the width and height of the AV form that comtains
            //   the video and also additional UI element of size uIBorderWidth/uIBorderHeight

            // Let's first assume that if we keep the height allowed (value.Height) and that the 
            //   form width generated will be smaller than the width allowed (value.Width)
            int videoHeight = intendedSize.Height - videoCapability.uIBorderHeight;
            int videoWidth = (int)(videoHeight / videoCapability.vidSrcRatio);
            int formWidth = videoWidth + videoCapability.uIBorderWidth;
            int formHeight = intendedSize.Height;

            // Check if the form width generated is greater than the width allowed (value.Width)
            if (formWidth > intendedSize.Width)
            {
                // If so, keep the width allowed (value.Width) and resize the height
                videoWidth = intendedSize.Width - videoCapability.uIBorderWidth;
                videoHeight = (int)(videoWidth * videoCapability.vidSrcRatio);
                formHeight = videoHeight + videoCapability.uIBorderHeight;
                formWidth = intendedSize.Width;
            }

            // Set the new size that takes in account the video ratio constraints
            base.Size = new System.Drawing.Size(formWidth, formHeight);
        }
        private int UIBorderWidth
        {
            get { return Width - ClientSize.Width; }
        }

        private int UIBorderHeight
        {
            get { return pbMiddleFill.Height + Height - ClientSize.Height; }
        }

        private void MinSize()
        {
            this.MinimumSize = new Size(80 + UIBorderWidth, 60 + UIBorderHeight);
        }


        private void _PositionVolumeSlider()
        {
            // Note: It gets the value from the audio capability
            // TODO: See if there is a better way to do that (i.e. Method Invoking with param)
            tbVolume.Value = audioCapability.Volume;
        }



        public void UpdateVideoUI(int uiState)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateUIStateHandler(_UpdateVideoUI), new object[] { uiState });
            }
            else
            {
                _UpdateVideoUI(uiState);
            }
        }
        private void _UpdateVideoUI(int uiState)
        {
            // Default state if audio is playing
            videoInfo = null;

            // Remote video stopped
            if ((uiState & (int)UIState.RemoteVideoStopped) == (int)UIState.RemoteVideoStopped)
            {
                videoInfo = RemoteVideoSendStoppedMsg;
            }

            // Because local video comes last, it will win

            // Video play stopped
            if ((uiState & (int)UIState.LocalVideoPlayStopped) == (int)UIState.LocalVideoPlayStopped)
            {
                videoInfo = LocalVideoPlayStoppedMsg;

                pbVideoButtonExt_Click(this, null);
            }
            else
            {
                pbVideoButton_Click(this, null);
            }

            // Enable / Disable the Stop and Play buttons by capability.IsPlaying
            // (You can't Stop or Play video in the UI if the capability isn't Playing it)
            Debug.Assert(videoCapability != null);

            pnlVideo.Visible = videoCapability.IsPlaying;

            // Local video send stopped
            if ((uiState & (int)UIState.LocalVideoSendStopped) == (int)UIState.LocalVideoSendStopped)
            {
                Debug.Assert(videoCapability.IsSender);

                videoInfo = LocalVideoSendStoppedMsg;
            }

            RepositionAVButtons();

            UpdateInfo();

            TrimBlackFromVideo(base.Size);
        }


        public void UpdateAudioUI(int uiState)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateUIStateHandler(_UpdateAudioUI), new object[] { uiState });
            }
            else
            {
                _UpdateAudioUI(uiState);
            }
        }

        private void _UpdateAudioUI(int uiState)
        {
            // Default state if audio is playing
            audioInfo = null;

            // Remote audio
            if ((uiState & (int)UIState.RemoteAudioStopped) == (int)UIState.RemoteAudioStopped)
            {
                audioInfo = RemoteAudioSendStoppedMsg;
            }

            // Because local audio comes last, it will win

            // Local audio play stopped
            if ((uiState & (int)UIState.LocalAudioPlayStopped) == (int)UIState.LocalAudioPlayStopped)
            {
                // Don't inform the Sender they stopped playing their own audio
                if (!audioCapability.IsSender)
                {
                    audioInfo = LocalAudioPlayStoppedMsg;
                }
            }

            // Enable / Disable the Stop and Play buttons by capability.IsPlaying
            // (You can't Stop or Play video in the UI if the capability isn't Playing it)
            Debug.Assert(audioCapability != null);

            pnlAudio.Visible = audioCapability.IsPlaying;
            tbVolume.Visible = audioCapability.IsPlaying;

            if (tbVolume.Visible)
            {
                PositionVolumeSlider();
            }

            isAudioButtonPushed = !audioCapability.IsPlaying;
            pbAudioButton_Click(this, null);

            // Local audio send stopped
            if ((uiState & (int)UIState.LocalAudioSendStopped) == (int)UIState.LocalAudioSendStopped)
            {
                Debug.Assert(audioCapability.IsSender);

                audioInfo = LocalAudioSendStoppedMsg;
            }

            RepositionAVButtons();

            UpdateInfo();
        }


        private void UpdateInfo()
        {
            string info = Status;

            if (videoInfo == null && audioInfo == null)
            {
                info = StatusNormal;
            }
            else
            {
                if (videoInfo != null)
                {
                    info += videoInfo;
                }

                if (audioInfo != null)
                {
                    // Separator between messages
                    if (videoInfo != null)
                    {
                        info += " -- ";
                    }

                    info += audioInfo;
                }
            }

            lblInfo.Text = info;
        }

        #endregion Private

        #region ICapabilityForm

        /// <summary>
        /// Add a capability object to the list of capability objects referring to this shared form
        /// </summary>
        /// <param name="capability">The capability object to add</param>
        public override void AddCapability(ICapability capability)
        {
            base.AddCapability(capability);

            if (capability is VideoCapability)
            {
                InitVideoCapability(capability);
            }
            else if (capability is AudioCapability)
            {
                InitAudioCapability(capability);
            }
        }

        /// <summary>
        /// Remove a capability object to the list of capability objects referring to this shared form
        /// </summary>
        /// <param name="capability">The capability object to remove</param>
        /// <returns>true if there is no more capability of this type, false otherwise</returns>
        public override bool RemoveCapability(ICapability capability)
        {
            bool lastCapability = base.RemoveCapability(capability);

            if (lastCapability)
            {

                // TODO - we need to keep track of how many instances of a capability are in use
                // so that we shut down when the last instance goes away, not the first
                if (capability is VideoCapability)
                {
                    UninitVideoUI();
                    videoCapability = null;
                }
                else if (capability is AudioCapability)
                {
                    UninitAudioUI();
                    audioCapability = null;
                }
            }

            return lastCapability;
        }


        #endregion ICapabilityForm
        public FAudioVideo()
        {
            InitializeComponent();
        }

        private void pbVideo_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEvent(this,e);
        }

        private void pbVideo_Resize(object sender, EventArgs e)
        {
            if (videoCapability != null)
            {
                videoCapability.ResizeVideoStream(pbVideo.Height, pbVideo.Width);
            }
        }

        private void pbVideoButton_Click(object sender, EventArgs e)
        {
            if (!isVideoButtonPushed)
            {
                // update the state of the button
                isVideoButtonPushed = !isVideoButtonPushed;

                // Restart Playing
                if (videoCapability.IsSender)
                {
                    pbVideoButton.Image = ColorChange(imageListPlayButtons.Images[(int)AVPlayButtonState.VideoPlay], Color.Green);
                    pbVideoButtonExt.Image = ColorChange(imageListPlayButtons.Images[(int)AVPlayButtonState.VideoExtPlay], Color.Green);
                }
                else
                {
                    pbVideoButton.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.VideoPlay];
                    pbVideoButtonExt.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.VideoExtPlay];
                }

                videoCapability.ResumePlayingVideo();
            }
        }

        private void pbVideoButton_MouseEnter(object sender, EventArgs e)
        {
            if ((videoCapability != null) && (!isVideoButtonPushed))
            {
                if (videoCapability.IsSender)
                {
                    pbVideoButton.Image = ColorChange(imageListPlayButtons.Images[(int)AVPlayButtonState.VideoPlayOver], Color.Green);
                }
                else
                {
                    pbVideoButton.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.VideoPlayOver];
                }
            }
        }

        private void pbVideoButton_MouseLeave(object sender, EventArgs e)
        {
            if ((videoCapability != null) && (!isVideoButtonPushed))
            {
                if (videoCapability.IsSender)
                {
                    pbVideoButton.Image = ColorChange(imageListPlayButtons.Images[(int)AVPlayButtonState.VideoStopPlaying], Color.Green);
                }
                else
                {
                    pbVideoButton.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.VideoStopPlaying];
                }
            }
        }

        private void pbAudioButton_Click(object sender, EventArgs e)
        {

            if (!isAudioButtonPushed)
            {
                // Restart Playing
                if (sender == pbAudioButton)
                {
                    audioCapability.ResumePlayingAudio();
                }

                if (audioCapability.IsSender)
                {
                    pbAudioButton.Image = ColorChange(
                        imageListPlayButtons.Images[(int)AVPlayButtonState.AudioPlay], Color.Green);
                }
                else
                {
                    pbAudioButton.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.AudioPlay];
                }

                toolTipAudioVideo.SetToolTip(this.pbAudioButton, Strings.MuteAudio);

                // update the state of the button
                isAudioButtonPushed = true;
            }
            else
            {
                // Stop playing
                if (sender == pbAudioButton)
                {
                    audioCapability.StopPlayingAudio();
                }

                if (audioCapability.IsSender)
                {
                    pbAudioButton.Image = ColorChange(
                        imageListPlayButtons.Images[(int)AVPlayButtonState.AudioStopPlaying], Color.Green);
                }
                else
                {
                    pbAudioButton.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.AudioStopPlaying];
                }

                toolTipAudioVideo.SetToolTip(this.pbAudioButton, Strings.PlayAudio);

                // update the state of the button
                isAudioButtonPushed = false;
            }
        }

        private void pbAudioButton_MouseEnter(object sender, EventArgs e)
        {
            if (audioCapability != null)
            {
                if (isAudioButtonPushed)
                {
                    if (audioCapability.IsSender)
                    {
                        pbAudioButton.Image = ColorChange(
                            imageListPlayButtons.Images[(int)AVPlayButtonState.AudioPlayOver], Color.Green);
                    }
                    else
                    {
                        pbAudioButton.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.AudioPlayOver];
                    }
                }
                else
                {
                    if (audioCapability.IsSender)
                    {
                        pbAudioButton.Image = ColorChange(
                            imageListPlayButtons.Images[(int)AVPlayButtonState.AudioStopPlayingOver], Color.Green);
                    }
                    else
                    {
                        pbAudioButton.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.AudioStopPlayingOver];
                    }
                }
            }
        }

        private void pbAudioButton_MouseLeave(object sender, EventArgs e)
        {
            if (audioCapability != null)
            {
                if (isAudioButtonPushed)
                {
                    if (audioCapability.IsSender)
                    {
                        pbAudioButton.Image = ColorChange(
                            imageListPlayButtons.Images[(int)AVPlayButtonState.AudioPlay], Color.Green);
                    }
                    else
                    {
                        pbAudioButton.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.AudioPlay];
                    }
                }
                else
                {
                    if (audioCapability.IsSender)
                    {
                        pbAudioButton.Image = ColorChange(
                            imageListPlayButtons.Images[(int)AVPlayButtonState.AudioStopPlaying], Color.Green);
                    }
                    else
                    {
                        pbAudioButton.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.AudioStopPlaying];
                    }
                }
            }
        }

        private void tbVolume_Scroll(int value)
        {
            audioCapability.SetVolume(value);
        }

        private void pbVideoButtonExt_Click(object sender, EventArgs e)
        {
            if (isVideoButtonPushed)
            {
                // update the state of the button
                isVideoButtonPushed = !isVideoButtonPushed;

                // Display the play button since we are in pause mode
                if (videoCapability.IsSender)
                {
                    pbVideoButton.Image = ColorChange(imageListPlayButtons.Images[(int)AVPlayButtonState.VideoStopPlaying], Color.Green);
                    pbVideoButtonExt.Image = ColorChange(imageListPlayButtons.Images[(int)AVPlayButtonState.VideoExtStopPlaying], Color.Green);
                }
                else
                {
                    pbVideoButton.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.VideoStopPlaying];
                    pbVideoButtonExt.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.VideoExtStopPlaying];
                }

                videoCapability.StopPlayingVideo();
            }
        }

        private void pbVideoButtonExt_MouseEnter(object sender, EventArgs e)
        {

            if ((videoCapability != null) && (isVideoButtonPushed))
            {
                if (videoCapability.IsSender)
                {
                    pbVideoButtonExt.Image = ColorChange(imageListPlayButtons.Images[(int)AVPlayButtonState.VideoExtPlayOver], Color.Green);
                }
                else
                {
                    pbVideoButtonExt.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.VideoExtPlayOver];
                }
            }
        }

        private void pbVideoButtonExt_MouseLeave(object sender, EventArgs e)
        {
            if ((videoCapability != null) && (isVideoButtonPushed))
            {
                if (videoCapability.IsSender)
                {
                    pbVideoButtonExt.Image = ColorChange(imageListPlayButtons.Images[(int)AVPlayButtonState.VideoExtPlay], Color.Green);
                }
                else
                {
                    pbVideoButtonExt.Image = imageListPlayButtons.Images[(int)AVPlayButtonState.VideoExtPlay];
                }
            }
        }

        private void pbLocalVideoButton_Click(object sender, EventArgs e)
        {
            // Thus button will appear only on the local participant AV form
            if (!isLocalVideoButtonPushed)
            {
                // Restart Sending / Playing
                pbLocalVideoButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.VideoSendOver], Color.Green);
                videoCapability.ResumeSendingVideo();
                toolTipAudioVideo.SetToolTip(this.pbLocalVideoButton, Strings.StopSendingMyVideo);
            }
            else
            {
                // Display the play button since we are in pause mode

                // Display the appropriate pause message
                pbLocalVideoButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.VideoStopSendingOver], Color.Green);
                videoCapability.StopSendingVideo();
                toolTipAudioVideo.SetToolTip(this.pbLocalVideoButton, Strings.SendMyVideo);
            }

            // update the state of the button
            isLocalVideoButtonPushed = !isLocalVideoButtonPushed;
        }

        private void pbLocalVideoButton_MouseEnter(object sender, EventArgs e)
        {
            if (!isLocalVideoButtonPushed)
            {
                pbLocalVideoButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.VideoStopSendingOver], Color.Green);
            }
            else
            {
                pbLocalVideoButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.VideoSendOver], Color.Green);
            }
        }

        private void pbLocalVideoButton_MouseLeave(object sender, EventArgs e)
        {
            if (!isLocalVideoButtonPushed)
            {
                pbLocalVideoButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.VideoStopSending], Color.Green);
            }
            else
            {
                pbLocalVideoButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.VideoSend], Color.Green);
            }
        }

        private void pbLocalAudioButton_Click(object sender, EventArgs e)
        {
            // Thus button will appear only on the local participant AV form
            if (!isLocalAudioButtonPushed)
            {
                // Restart Sending
                pbLocalAudioButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.AudioSendOver], Color.Green);
                audioCapability.ResumeSendingAudio();
                toolTipAudioVideo.SetToolTip(this.pbLocalAudioButton, Strings.StopSendingMyAudio);
            }
            else
            {
                // Display the play button since we are in pause mode

                // Display the appropriate pause message
                pbLocalAudioButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.AudioStopSendingOver], Color.Green);
                audioCapability.StopSendingAudio();
                toolTipAudioVideo.SetToolTip(this.pbLocalAudioButton, Strings.SendMyAudio);
            }

            // update the state of the button
            isLocalAudioButtonPushed = !isLocalAudioButtonPushed;
        }

        private void pbLocalAudioButton_MouseEnter(object sender, EventArgs e)
        {
            if (!isLocalAudioButtonPushed)
            {
                pbLocalAudioButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.AudioStopSendingOver], Color.Green);
            }
            else
            {
                pbLocalAudioButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.AudioSendOver], Color.Green);
            }
        }

        private void pbLocalAudioButton_MouseLeave(object sender, EventArgs e)
        {
            if (!isLocalAudioButtonPushed)
            {
                pbLocalAudioButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.AudioStopSending], Color.Green);
            }
            else
            {
                pbLocalAudioButton.Image = ColorChange(
                    imageListSendButtons.Images[(int)AVSendButtonState.AudioSend], Color.Green);
            }
        }

        private void pbVideoTab_Click(object sender, EventArgs e)
        {
            videoCapability.ShowCameraConfiguration();
        }

        private void pbVideoTab_MouseEnter(object sender, EventArgs e)
        {
            pbVideoTab.Image = ColorChange(
                   imageListConfig.Images[(int)ConfigButtonState.VideoConfigOver], Color.Green);
        }

        private void pbVideoTab_MouseLeave(object sender, EventArgs e)
        {
            pbVideoTab.Image = ColorChange(
               imageListConfig.Images[(int)ConfigButtonState.VideoConfig], Color.Green);
        }

        private void FAudioVideo_Load(object sender, EventArgs e)
        {
            this.lblInfo.Text = Strings.StatusNormal;
            this.toolTipAudioVideo.SetToolTip(this.pbVideoButton, Strings.PlayVideo);
            this.toolTipAudioVideo.SetToolTip(this.pbAudioButton, Strings.MuteAudio);
            this.toolTipAudioVideo.SetToolTip(this.lblInfo, Strings.StatusInformation);
            this.toolTipAudioVideo.SetToolTip(this.tbVolume, Strings.Volume);
            this.toolTipAudioVideo.SetToolTip(this.pbVideoButtonExt, Strings.PauseVideo);
            this.toolTipAudioVideo.SetToolTip(this.pbLocalVideoButton, Strings.StopSendingMyVideo);
            this.toolTipAudioVideo.SetToolTip(this.pbLocalAudioButton, Strings.StopSendingMyAudio);
            this.toolTipAudioVideo.SetToolTip(this.pbVideoTab, Strings.CameraSettings);

            if ((videoCapability != null && videoCapability.IsSender) ||
                (audioCapability != null && audioCapability.IsSender))
            // Initiator UI
            {
                // Dynamically re-coloring the UI in green using GDI.NET
                pbMiddleFill.BackgroundImage = ColorChange(pbMiddleFill.BackgroundImage, Color.Green);
                this.pbMiddleFill.Visible=false;

                // Video UI
                pnlVideo.BackgroundImage = ColorChange(pnlVideo.BackgroundImage, Color.Green);
                pbVideoButton.Image = ColorChange(pbVideoButton.Image, Color.Green);
                pbVideoButtonExt.Image = ColorChange(pbVideoButtonExt.Image, Color.Green);
                pbVideoTab.Image = ColorChange(pbVideoTab.Image, Color.Green);
                pnlLocalVideo.BackgroundImage = ColorChange(pnlLocalVideo.BackgroundImage, Color.Green);
                pnlLocalVideoConfig.BackgroundImage = ColorChange(pnlLocalVideoConfig.BackgroundImage, Color.Green);
                pbLocalVideoButton.Image = ColorChange(pbLocalVideoButton.Image, Color.Green);
                pbVideoSeparation.BackgroundImage = ColorChange(pbVideoSeparation.BackgroundImage, Color.Green);
                //pnlSeparatorSender.BackgroundImage = ColorChange(pnlSeparatorSender.BackgroundImage, Color.Green); ;

                // Audio UI
                pnlAudio.BackgroundImage = ColorChange(pnlAudio.BackgroundImage, Color.Green);
                pbAudioButton.Image = ColorChange(pbAudioButton.Image, Color.Green);
                tbVolume.BackgroundImage = ColorChange(tbVolume.BackgroundImage, Color.Green);
                tbVolume.CursorImage = ColorChange(tbVolume.CursorImage, Color.Green);
                pbLocalAudioButton.Image = ColorChange(pbLocalAudioButton.Image, Color.Green);
                //pnlSeparatorPlayer.BackgroundImage = ColorChange(pnlSeparatorPlayer.BackgroundImage, Color.Green);

                // Info UI
                lblInfo.Image = ColorChange(lblInfo.Image, Color.Green);

                // Set tooltips that will change depending on state
                toolTipAudioVideo.SetToolTip(this.pbLocalVideoButton, Strings.StopSendingMyVideo);
                toolTipAudioVideo.SetToolTip(this.pbLocalAudioButton, Strings.StopSendingMyAudio);
                toolTipAudioVideo.SetToolTip(this.pbAudioButton, Strings.MuteAudio);

                // TODO: Don't forget to add code like the following later on when we add imageLists
            }
            else // Remote user UI
            {
                // TODO: Add code if needed to customize remote user UI (such as button invisible)
            }

            // TODO: Discuss form load with Jason during code review
            // TODO: So far InitVideoUI and InitAudioUI might be called twice if called from AddCapa... fix that
            if (videoCapability != null)
            {
                InitVideoUI();
            }
            if (audioCapability != null)
            {
                InitAudioUI();
            }

            // The launch process is the following: 
            //         - Create Form
            //         - Prepare the UI
            //         - Show Form
            // InitUI methods access the form which causes the form to show if not displayed
            // This would make the form appear too early before all the UI is ready (bad user experience)
            // To prevent that we need to have a flag that indicates that the form has been loaded
            // so a capabability added doesn't directly call the Init UI code if the form has not been
            // loaded
            // TODO: Code to add: isFormLoaded = true;
            this.MouseEvent += new MDDelegate(this.frm_MouseDown);
        }
        void frm_MouseDown(object sender, MouseEventArgs e)
        {
            Control ctr = sender as Control;
            DoDragDrop(ctr, DragDropEffects.Move);
        }
        

        private void pbVideo_MouseEnter(object sender, EventArgs e)
        {
            this.pbMiddleFill.Visible = true; ;
            pbMiddleFillIsUnVisble = false;
            timer1.Enabled = true;

            //this.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(pbMiddleFillIsUnVisble)
            {
                this.pbMiddleFill.Visible=false;
                timer1.Enabled = false;
                pbMiddleFillIsUnVisble = false;
            }
        }

        private void pbMiddleFill_MouseLeave(object sender, EventArgs e)
        {
            pbMiddleFillIsUnVisble = true;
        }

        private void menuEnter(object sender, EventArgs e)
        {
            pbMiddleFillIsUnVisble = false;
        }

        private void pbVideo_MouseLeave(object sender, EventArgs e)
        {
            pbMiddleFillIsUnVisble = true;
        }

        private void FAudioVideo_MouseLeave(object sender, EventArgs e)
        {
            pbMiddleFillIsUnVisble = true;
        }

    }
}
