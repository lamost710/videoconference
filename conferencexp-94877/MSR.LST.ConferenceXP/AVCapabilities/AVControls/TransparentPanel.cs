using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace tongji.mConf.Controls
{
    public partial class TransparentPanel : DrawingArea
    {
        protected override void OnDraw()
        {
            // Gets the image from the global resources
            if (this.BackgroundImage != null)
            {
                Image broculoImage = this.BackgroundImage;

                // Sets the images' sizes and positions
                Rectangle big = new Rectangle(0, 0, this.Width, this.Height);

                //this.graphics.FillRectangle(Brushes.Black, 0, 0, broculoImage.Width, broculoImage.Height);
                //this.graphics.FillRectangle(Brushes.Aqua, big);
                ImageAttributes att = new ImageAttributes();
                att.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                this.graphics.DrawImage(broculoImage, big, 0, 0, broculoImage.Width, broculoImage.Height, GraphicsUnit.Pixel,att);

                // Draws the two images
                //this.graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                //this.graphics.DrawImage(broculoImage, big);
            }
        }
    }
}
