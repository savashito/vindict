using UnityEngine;
using System.Collections;
using SocketIO;



public class SocketSingleton : Singleton<SocketSingleton> {
	private static SocketIOComponent mSocket= null;
	private static GameObject go;
	protected SocketSingleton(){

	}
	public SocketIOComponent get(){
		if (mSocket == null) {
			//GameObject go = new GameObject();
			/*
			go =  GameObject.Find("SocketIO");
			go.SetActive (true);
			mSocket = go.GetComponent<SocketIOComponent>();
*/

			// go =  GameObject.Find("MainCamera");//new GameObject ();

			// go.SetActive (true);

			// mSocket.Start ();
			mSocket = transform.gameObject.GetComponent<SocketIOComponent>();
		}
		return mSocket;
	}
	/*
	public string getUserId(){

	}*/
	void Start(){
		UnityEngine.Debug.Log ("Mack");


	}
//	void Update(){
//		if (mSocket != null)
//			mSocket.Update ();
//	}
//	void OnDestroy(){
//		if (mSocket != null)
//			mSocket.OnDestroy ();
//	}
	void initSocket(){
		go =  GameObject.Find("SocketIO");
		go.SetActive (true);
		mSocket = go.GetComponent<SocketIOComponent>();
		/*
		
		mSocket = transform.gameObject.AddComponent<SocketIOComponent> (); 
		mSocket.enabled = true;
		// mSocket.
		mSocket.url = "ws://jupitar.org:8080/socket.io/?EIO=4&transport=websocket";
		mSocket.autoConnect = true;
		mSocket.reconnectDelay = 5;
		mSocket.ackExpirationTime = 1800;
		mSocket.pingInterval = 25;
		mSocket.pingTimeout = 60;*/
		UnityEngine.Debug.Log ("Socket inited");

	}
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		if (mSocket == null)
			initSocket ();

	}
}
