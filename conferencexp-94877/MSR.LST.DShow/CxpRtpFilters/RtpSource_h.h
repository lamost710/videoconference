

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0595 */
/* at Wed Jul 03 09:14:36 2013
 */
/* Compiler settings for RtpSource.idl:
    Oicf, W3, Zp8, env=Win32 (32b run), target_arch=X86 8.00.0595 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __RtpSource_h_h__
#define __RtpSource_h_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IRtpSource_FWD_DEFINED__
#define __IRtpSource_FWD_DEFINED__
typedef interface IRtpSource IRtpSource;

#endif 	/* __IRtpSource_FWD_DEFINED__ */


#ifndef __RtpSource_FWD_DEFINED__
#define __RtpSource_FWD_DEFINED__

#ifdef __cplusplus
typedef class RtpSource RtpSource;
#else
typedef struct RtpSource RtpSource;
#endif /* __cplusplus */

#endif 	/* __RtpSource_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IRtpSource_INTERFACE_DEFINED__
#define __IRtpSource_INTERFACE_DEFINED__

/* interface IRtpSource */
/* [unique][nonextensible][uuid][object] */ 


EXTERN_C const IID IID_IRtpSource;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("72E42A61-6AA1-45ad-BA21-73065A897F5C")
    IRtpSource : public IUnknown
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Initialize( 
            /* [in] */ IUnknown *pRtpStream) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IRtpSourceVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IRtpSource * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IRtpSource * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IRtpSource * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Initialize )( 
            IRtpSource * This,
            /* [in] */ IUnknown *pRtpStream);
        
        END_INTERFACE
    } IRtpSourceVtbl;

    interface IRtpSource
    {
        CONST_VTBL struct IRtpSourceVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IRtpSource_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IRtpSource_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IRtpSource_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IRtpSource_Initialize(This,pRtpStream)	\
    ( (This)->lpVtbl -> Initialize(This,pRtpStream) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IRtpSource_INTERFACE_DEFINED__ */


/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


