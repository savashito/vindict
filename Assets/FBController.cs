using UnityEngine;
using System.Collections;

public class FBController : MonoBehaviour {

	//string userId = "mN5oi2WjNaTshX8md";
	// mN5oi2WjNaTshX8md // 
	string userId = "mN5oi2WjNaTshX8md";// "nfKTNXsYZJL7jjykD"; //  "mN5oi2WjNaTshX8md"; // "vaXqjFSfBLzjnJ5vS";// "nfKTNXsYZJL7jjykD";
	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/
	public bool isUser(string userId){
		//Debug.Log (this.userId);
		//Debug.Log (userId);
		int val = string.Compare (this.userId, userId);
//		Debug.Log (val);
		return val ==0;

	}
}
