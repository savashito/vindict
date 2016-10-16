using UnityEngine;
using System.Collections;
// using UnityEngine.SceneManager;
using UnityEngine.SceneManagement;
using SocketIO;
using System.Collections.Generic;

public class onClickLoadScene : MonoBehaviour {
	private SocketIOComponent socket;

	void Start () {
		//GameObject go = GameObject.Find ("SocketIO");
		socket = SocketSingleton.Instance.get ();//go.GetComponent<SocketIOComponent> ();
		UnityEngine.Debug.Log ("Start called",socket);
	}
	public void LoadScene(int scene){
		// SceneManager.load
		SceneManager.LoadScene(scene);
	}
	public void RowSolo(){
		Dictionary<string, string> a = new Dictionary<string, string> ();
		a.Add ("userId", "mN5oi2WjNaTshX8md"); // "vaXqjFSfBLzjnJ5vS");
		a.Add ("workoutName","workout1");// 7WFMgCerQk6Zqq2AM
		socket.Emit ("createWorkout", new JSONObject (a));
		a.Add("lane","3");
		// asign a lane
//		data = {"userId":'vaXqjFSfBLzjnJ5vS',"lane":"3"};
//		# data = {"userId":'mN5oi2WjNaTshX8md',"lane":"2","name":"RodrigoGoogle"};
		socket.Emit("setLane", new JSONObject (a));
		// start workout
		socket.Emit("startWorkout", new JSONObject (a));
		UnityEngine.Debug.Log ("Workout started");
//		# workout has to exist
//		#data = {"userId":'vaXqjFSfBLzjnJ5vS'};
//		socketIO.emit('startWorkout',data)
		SceneManager.LoadScene(1);
	}
}
