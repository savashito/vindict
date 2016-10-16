using UnityEngine;
using System.Collections;

public class enemyBoat : MonoBehaviour 
{

	private Rigidbody rb;
	public float speed;

	// Use this for initialization

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}

	//called every time the physics update
	void FixedUpdate()
	{
		Vector3 v3 = new Vector3 ( 0.0f, 0.0f,speed);

		rb.AddForce (v3);
	}
}
