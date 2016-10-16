using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	public GameObject target;
	public Vector3 cameraOffset = new Vector3(0,300000,0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setTargetBoat(FloatBoat boat){
		target = boat.getGameObject();
	}

	void LateUpdate() {
		transform.LookAt(target.transform.position+new Vector3(0f,0f,300*10f));
//		transform.loo
		transform.position = cameraOffset + new Vector3 (target.transform.position.x,0,target.transform.position.z);
		//			target.transform.position.x; transform.position.y
//		transform.position.z = target.transform.position.z-10;
	}



}
