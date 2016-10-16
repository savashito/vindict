using UnityEngine;
using System.Collections;

public class oarHinge : MonoBehaviour
{
	private HingeJoint hinge;
	private JointMotor oar;
	private int oarSpeed;
	private int maxAngle;

	// Use this for initialization
	void Start () 
	{
		hinge = GetComponent<HingeJoint> ();
		oar = hinge.motor;
		oarSpeed = 70;
		maxAngle = 25;
		oar.force = 20;
		oar.targetVelocity = -1 * oarSpeed;
		hinge.useMotor = true;
		hinge.motor = oar;
	}
	
	//called every time the physics update
	void FixedUpdate()
	{
		if (hinge.angle <= -1*maxAngle) 
		{
			oar.targetVelocity = oarSpeed;
			hinge.motor = oar;
		}
		else if (hinge.angle >= maxAngle) 
		{
			oar.targetVelocity = -1 * oarSpeed;
			hinge.motor = oar;
		}
			
	}
}
