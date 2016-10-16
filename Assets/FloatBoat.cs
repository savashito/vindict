
using UnityEngine;
using System.Collections;
using SocketIO;

using System.Collections.Generic;
//using System.Web.UI;
//using   System.Web.Script.Serialization;

// 54.153.114.110:
public class FloatBoat : MonoBehaviour {
	Rigidbody mRigidBody;
	public GameObject water;
	private float waterLevel = -0.50f; // -0.16f;
	public float floatHeight;// = 2.0f;
	public float bounceDamp = 0.05f;﻿
	public Vector3 buoyancyCentreOffset;
	public float currentForce;
	/*
	public Text textDebug;
	public Text textDistance;
	public Text textParcial;
	public Text textWatts;
	public Text textTime;
	public Text textSPM;
*/
	private float forceFactor;
	private Vector3 actionPoint;
	private Vector3 uplift;
	private JSONObject prevErgData = null;
	public AssemblyCSharp.Authentification auth;
//	GameObject go;
	// Use this for initialization
	void Start () {
//		go =  GameObject.Find("SocketIO");
		mRigidBody = GetComponent<Rigidbody> ();
		// socket.On("boop", TestBoop);
//		gotErgData = false;
//		socket.On ("ergData", ErgData);
//		Dictionary<string, string> a = new Dictionary<string, string>();
//		a.Add("userId","mN5oi2WjNaTshX8md");
//		Debug.Log ("Workout started");
//		socket.Emit ("startWorkout", new JSONObject(a));
		/*
		socket.On("rowerGretting",(data) => {
			UnityEngine.Debug.Log("Got greeting");
//			JsonConvert.SerializeObject();
//			JSONObject.Create(




//			KeyValuePair<string, string> a = new KeyValuePair<string, string>("id","8u6iM6eLfRdqeDYGQ");
//			ICollection<KeyValuePair<string,string>> b = new ICollection<KeyValuePair<string,string>>(a);
						//			Dictionary<string, string> a = new KeyValuePair<string, string>("id","8u6iM6eLfRdqeDYGQ");
//			JavaScriptSerializer serializer = new JavaScriptSerializer();
//			var output = serializer.Serialize(o);
				//new JSONObject("8u6iM6eLfRdqeDYGQ")
			Dictionary<string, string> a = new Dictionary<string, string>();
			a.Add("userId","8u6iM6eLfRdqeDYGQ");
			socket.Emit ("userId", new JSONObject(a));
		});
		*/
		mRigidBody.freezeRotation = true;
		waterLevel = water.transform.position.y;
	}
	public GameObject getGameObject(){
		return this.gameObject;
		return GetComponent<GameObject> ();
	}

	public void SendData(SocketIOEvent e){
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

		float deltaD = -(d - prevD);
		float deltaT = Mathf.Max( t - prevT,0.005f);

		float force = deltaD / deltaT + (d-mRigidBody.position.z)*correctionParameter;
		currentForce = force;
		//		mRigidBody.position = new Vector3(mRigidBody.position.x,mRigidBody.position.y,d);
		//		setBoatFowardForce (force);
		mRigidBody.velocity = new Vector3(mRigidBody.velocity.x,mRigidBody.velocity.y,force);
		/*
		if (Mathf.Abs ((mRigidBody.position.z) - d) > 2.0f) {
			// correct for position
			mRigidBody.position = new Vector3(mRigidBody.position.x,mRigidBody.position.y,d);
			mRigidBody.velocity = new Vector3(0f,0f,force);
			mRigidBody.rotation = new Quaternion(0f,0f,0f,1.0f);
		}*/

		//Debug.Log ("muerte");
		//Debug.Log(string.Format("[name: {0}, data: {1}]", deltaD,deltaT));



		prevErgData = e.data;
		//mRigidBody
	}

	public float correctionParameter;


	void setBoatFowardForce (float force){
		float fowardSpeed = force;
		float upSpeed = 0.0f;
		upSpeed = force * 0.1f;//0.05f * Time.deltaTime * 1000.0f;
		/*
		// mRigidBody.velocity = new Vector3 (1.0f, 0.0f, 1.0f);
		if (Input.GetButtonDown ("Fire1")) {
			mRigidBody.velocity = new Vector3 (0.0f, 0.5f, 0.0f);
		}
		*/


		//		mRigidBody.transform.position
		mRigidBody.AddForceAtPosition (new Vector3 (0.0f, upSpeed, fowardSpeed), mRigidBody.position);

	}
	// Update is called once per frame

	void Update () {		
		/*
		if (Input.GetKey(KeyCode.I)) {
			// fowardSpeed = 0.1f * Time.deltaTime*1000.0f;
			setBoatFowardForce(2.5f);
			//print( fowardSpeed);

		}
		if (Input.GetKey(KeyCode.K)) {
			setBoatFowardForce(-2.5f);
//			fowardSpeed = -0.1f * Time.deltaTime*1000.0f;
//			print( fowardSpeed);
		}*/
		actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
		forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);
		if (forceFactor > 0f) 
		{
			uplift = -Physics.gravity * (forceFactor - mRigidBody.velocity.y * bounceDamp);
			mRigidBody.AddForceAtPosition(uplift, actionPoint);
		}
	}
}
