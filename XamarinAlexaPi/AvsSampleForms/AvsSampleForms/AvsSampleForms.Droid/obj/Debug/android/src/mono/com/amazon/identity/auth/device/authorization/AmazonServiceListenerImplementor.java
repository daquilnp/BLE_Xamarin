package mono.com.amazon.identity.auth.device.authorization;


public class AmazonServiceListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.amazon.identity.auth.device.authorization.AmazonServiceListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onBindError:(Lcom/amazon/identity/auth/device/AuthError;)V:GetOnBindError_Lcom_amazon_identity_auth_device_AuthError_Handler:Amazon.Identity.Auth.Device.Authorization.IAmazonServiceListenerInvoker, Xamarin.Amazon.Login\n" +
			"n_onBindSuccess:(Landroid/os/IInterface;)V:GetOnBindSuccess_Landroid_os_IInterface_Handler:Amazon.Identity.Auth.Device.Authorization.IAmazonServiceListenerInvoker, Xamarin.Amazon.Login\n" +
			"";
		mono.android.Runtime.register ("Amazon.Identity.Auth.Device.Authorization.IAmazonServiceListenerImplementor, Xamarin.Amazon.Login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AmazonServiceListenerImplementor.class, __md_methods);
	}


	public AmazonServiceListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AmazonServiceListenerImplementor.class)
			mono.android.TypeManager.Activate ("Amazon.Identity.Auth.Device.Authorization.IAmazonServiceListenerImplementor, Xamarin.Amazon.Login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onBindError (com.amazon.identity.auth.device.AuthError p0)
	{
		n_onBindError (p0);
	}

	private native void n_onBindError (com.amazon.identity.auth.device.AuthError p0);


	public void onBindSuccess (android.os.IInterface p0)
	{
		n_onBindSuccess (p0);
	}

	private native void n_onBindSuccess (android.os.IInterface p0);

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
