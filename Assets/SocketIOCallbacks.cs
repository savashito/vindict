using UnityEngine;
using System.Collections;
using SocketIO;
using System.Collections.Generic;

public class SocketIOCallbacks : MonoBehaviour {
	
	private JSONObject prevErgData = null;
	private SocketIOComponent socket;
	public FloatBoat floatBoat1,floatBoat2,floatBoat3;
	public TextController mTextControoller;
	public FBController fbController;
	public LookAtCamera mCamera;
	// Use this for initialization
	void Start () {
		
		socket = SocketSingleton.Instance.get ();

		socket.On ("ergData", ErgData);
		socket.On ("connected", startWorkout);
		//startWorkout ();
	}
	
	// Update is called once per frame
	void Update () {
		// this is for starting workout
		if (Input.GetKey(KeyCode.I)) {
			startWorkout (null);
		}
	}
	void startWorkout(SocketIOEvent e){
		UnityEngine.Debug.Log ("Workout Started");
		Dictionary<string, string> a = new Dictionary<string, string> ();
		a.Add ("userId", "mN5oi2WjNaTshX8md"); // "vaXqjFSfBLzjnJ5vS"); 
		socket.Emit ("startWorkout", new JSONObject (a));
	}
	string getUserId(SocketIOEvent e){
		var data = e.data;
		string userId = "";
		data.GetField(ref userId,"userId");
		return userId;
	}
	int getBoatIndexFromUserId(SocketIOEvent e){
		var data = e.data;

		string lane = "";

		data.GetField(ref lane,"lane");

		int laneInt = int.Parse(lane);
		/*Debug.Log ("user: ");
//		Debug.Log (userId);
		Debug.Log (laneInt);
		Debug.Log (lane);*/
		return laneInt;
	}
	public FloatBoat getBoatFromLane(int lane){
		switch (lane) {
		case 1:
			return floatBoat1;

		case 2:
			return floatBoat2;

		case 3:
			return floatBoat3;
		}
		return null;
	}
	public void ErgData(SocketIOEvent e){
		int i = getBoatIndexFromUserId (e);
		string id = getUserId (e);
		FloatBoat boat = getBoatFromLane (i);
		Debug.Log (e);
		boat.SendData (e);
	 	Debug.Log (id); 
		Debug.Log (fbController.isUser (id));
		if (fbController.isUser (id)) {
			// Debug.Log ("TIs the same"); 
			mCamera.setTargetBoat (boat);
			mTextControoller.SetFields (e);
		}
		/*
		Debug.Log ("Hiii");
		//		gotErgData = true;
		//Debug.Log(string.Format("[name: {0}, data: {1}]", e.name, e.data));
		var data = e.data;

		//		float d = (float)data["distance"];
		//		float t = (float)data["time"];
		float d = 0f, t = 0f, prevD = 0f, prevT = 0f;
		if (prevErgData == null){
			prevErgData = data;
			return;
		}
		prevErgData.GetField(ref prevD,"distance");
		prevErgData.GetField(ref prevT,"time");
		data.GetField(ref d,"distance");
		data.GetField(ref t,"time");

		float deltaD = d - prevD;
		float deltaT = t - prevT;

		float force = deltaD / deltaT + (d-mRigidBody.position.z)*correctionParameter;
		//		mRigidBody.position = new Vector3(mRigidBody.position.x,mRigidBody.position.y,d);
		//		setBoatFowardForce (force);
		mRigidBody.velocity = new Vector3(mRigidBody.velocity.x,mRigidBody.velocity.y,force);
		if (Mathf.Abs ((mRigidBody.position.z) - d) > 2.0f) {
			// correct for position
			mRigidBody.position = new Vector3(mRigidBody.position.x,mRigidBody.position.y,d);
			mRigidBody.velocity = new Vector3(0f,0f,force);
			mRigidBody.rotation = new Quaternion(0f,0f,0f,1.0f);
		}

		Debug.Log ("muerte");
		Debug.Log(string.Format("[name: {0}, data: {1}]", deltaD,deltaT));
		textDistance.text = string.Format("{0}",Mathf.Floor(d));
		textTime.text = timeToString (t);
		float power=0f,pace=0f,spm=0f;
		data.GetField(ref power,"power");
		data.GetField(ref pace,"pace");
		data.GetField(ref spm,"spm");
		textWatts.text = string.Format("{0}",power);
		textParcial.text = string.Format("{0}",timeToString(pace));
		textSPM.text = string.Format("{0}",spm);
		prevErgData = e.data;
		//mRigidBody*/
	}
}
