package md5023215d593d44f50438f36261234faca;


public class Adapter
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.bluetooth.BluetoothAdapter.LeScanCallback
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onLeScan:(Landroid/bluetooth/BluetoothDevice;I[B)V:GetOnLeScan_Landroid_bluetooth_BluetoothDevice_IarrayBHandler:Android.Bluetooth.BluetoothAdapter/ILeScanCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Robotics.Mobile.Core.Bluetooth.LE.Adapter, Robotics.Mobile.Core.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", Adapter.class, __md_methods);
	}


	public Adapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Adapter.class)
			mono.android.TypeManager.Activate ("Robotics.Mobile.Core.Bluetooth.LE.Adapter, Robotics.Mobile.Core.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onLeScan (android.bluetooth.BluetoothDevice p0, int p1, byte[] p2)
	{
		n_onLeScan (p0, p1, p2);
	}

	private native void n_onLeScan (android.bluetooth.BluetoothDevice p0, int p1, byte[] p2);

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
