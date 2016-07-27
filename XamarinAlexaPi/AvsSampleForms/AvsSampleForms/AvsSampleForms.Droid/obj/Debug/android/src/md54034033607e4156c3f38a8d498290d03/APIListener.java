package md54034033607e4156c3f38a8d498290d03;


public class APIListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.amazon.identity.auth.device.shared.APIListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onError:(Lcom/amazon/identity/auth/device/AuthError;)V:GetOnError_Lcom_amazon_identity_auth_device_AuthError_Handler:Amazon.Identity.Auth.Device.Shared.IAPIListenerInvoker, Xamarin.Amazon.Login\n" +
			"n_onSuccess:(Landroid/os/Bundle;)V:GetOnSuccess_Landroid_os_Bundle_Handler:Amazon.Identity.Auth.Device.Shared.IAPIListenerInvoker, Xamarin.Amazon.Login\n" +
			"";
		mono.android.Runtime.register ("Amazon.Identity.Auth.Device.APIListener, Xamarin.Amazon.Login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", APIListener.class, __md_methods);
	}


	public APIListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == APIListener.class)
			mono.android.TypeManager.Activate ("Amazon.Identity.Auth.Device.APIListener, Xamarin.Amazon.Login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onError (com.amazon.identity.auth.device.AuthError p0)
	{
		n_onError (p0);
	}

	private native void n_onError (com.amazon.identity.auth.device.AuthError p0);


	public void onSuccess (android.os.Bundle p0)
	{
		n_onSuccess (p0);
	}

	private native void n_onSuccess (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
