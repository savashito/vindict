using UnityEngine;
using System.Collections;
using SocketIO;
using System.Collections.Generic;

public class Rower{
	public string name,id;
	int lane;
	public Rower(string name,string id,int lane){
		this.lane = lane;
		this.name = name;
		this.id = id;
	}

}

public class Workout{ 
	public string name,id;
	public Rower rowers;
	public Workout(string name,string id){
		this.name = name;
		this.id = id;
	}
	public void setRowers(Rower rowers){
		this.rowers = rowers;
	}
};
public class ViewWorkoutsComponnent : MonoBehaviour {
	Workout[] ws ;
	SocketIOComponent socket;
	// Use this for initialization
	void Start () {
		ws= new Workout[5];

		for (int i = 0; i < ws.Length; i++) {
			ws [i] = new Workout ("Jello " + i, "red"+i);
		}
		socket = SocketSingleton.Instance.get ();

		socket.On ("responseListWorkouts", responseListWorkouts);
		socket.On ("connected", connected);



		UnityEngine.Debug.Log ("Bark");
//		getApplicationContext ();
		/*
		activity.runOnUiThread(new Runnable() {
			public void run() {
				//Toast.makeText(activity, "Hello, world!", Toast.LENGTH_SHORT).show();
			}
		});*/


		/*
		activity.runOnUiThread(new Runnable() {
			public void run() {
		CreateGUIListWorkouts (ws);
			}
		});
		*/
	}
	void connected(SocketIOEvent e){
		UnityEngine.Debug.Log ("Connected");
		socket.Emit ("requestListWorkouts");
	}
	void responseListWorkouts(SocketIOEvent e){
		UnityEngine.Debug.Log ("Miauuu");
		/*Dictionary<string, string> a = new Dictionary<string, string> ();
		a.Add ("userId", "vaXqjFSfBLzjnJ5vS");
		socket.Emit ("requestListWorkouts", new JSONObject (a));
		*/
		Workout[] ws = getWorkouts (e);

	}
	Workout[] getWorkouts(SocketIOEvent e){
		var data = e.data;

		int lane ;
		int len = 0;
		//var tData = data as JToken;


		string s1="", s2="",s3="";
//		workout.GetField(ref lane,"name");
//		UnityEngine.Debug.Log (workout["name"]);
		data.GetField(ref len,"length");
//		UnityEngine.Debug.Log (len);
		var workoutsJSON = data["workouts"];
		ws = new Workout[len];
		for (int i = 0; i < ws.Length; i++) {
			var workoutJSON = workoutsJSON [i];
			workoutJSON.GetField (ref s1,"name");
			workoutJSON.GetField (ref s2,"_id");
			ws [i] = new Workout (s1,s2);
			var rowersJSON = workoutJSON ["_rowers"];
			int nRowers = rowersJSON.Count;
			Rower[] rowers = new Rower[nRowers];
			for (int j = 0; j < nRowers; j++) {
				var rowerJSON = rowersJSON [i];

				rowerJSON.GetField(ref s1,"name");
				rowerJSON.GetField(ref s2,"userId");
				rowerJSON.GetField(ref s3,"lane");
				lane = int.Parse (s3);
				rowers [j] = new Rower (s1,s2,lane);
				UnityEngine.Debug.Log (rowers[j].name);
			}
		}

		return ws;
		// int laneInt = int.Parse(lane);
	}
	// Update is called once per frame
	void Update () {
	
	}
	public void CreateGUIListWorkouts(Workout[] wks){
		int cx = Screen.width/2;
		int hb = 30;
		int offsetY = 30;
		int w = 100;
		for (int i = 0; i < wks.Length; i++) {
			Workout wk = wks [i];
			if (GUI.Button (new Rect (cx-w/2, offsetY + hb * i, w, 20), wk.name)) {
				UnityEngine.Debug.Log (wk.id);
			}
		}
	}
//	public Vector2 scrollPosition = Vector2.zero;

	void OnGUI() {
		CreateGUIListWorkouts (ws);
		/*
//		scrollPosition = GUI.BeginScrollView(new Rect(10, 300, 100, 100), scrollPosition, new Rect(0, 0, 220, 200));
		int cx = Screen.width/2;
		int hb = 30;
		int offsetY = 30;
		GUI.Button(new Rect(cx,offsetY+ hb*0, 100, 20), "Top-left");
		GUI.Button(new Rect(cx,offsetY+ hb*1, 100, 20), "Top-right");
		GUI.Button(new Rect(cx, offsetY+hb*2, 100, 20), "Bottom-left");
		GUI.Button(new Rect(cx, offsetY+hb*3, 100, 20), "Bottom-right");
		*/
//		GUI.EndScrollView();
	}

}
