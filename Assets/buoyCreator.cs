using UnityEngine;
using System.Collections;

public class buoyCreator : MonoBehaviour {
	public Material newMat;
	// Use this for initialization
	void Start () {
		for (int lane = 1; lane < 4*2; lane+=2) {
			for (int i = 0; i < 20; i++) {
				GameObject sphereRight = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				GameObject sphereLeft = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				sphereRight.transform.position = new Vector3 (3.0f*lane, 0F, 20.0f * i);
				sphereLeft.transform.position = new Vector3 (-3.0f*lane, 0F, 20.0f * i);
				sphereLeft.transform.localScale -= new Vector3 (0.65f, 0.65f, 0.65f);
				sphereRight.transform.localScale -= new Vector3 (0.65f, 0.65f, 0.65f);
				sphereRight.GetComponent<MeshRenderer> ().material = newMat;
				sphereLeft.GetComponent<MeshRenderer> ().material = newMat;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
