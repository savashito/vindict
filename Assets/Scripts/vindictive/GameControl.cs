using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;


public class GameControl : MonoBehaviour 
{
	private FloatBoat player;
	private enemyBoat enemy1;
	private enemyBoat enemy2;
//	private enemyBoat enemy3;
//	private enemyBoat enemy4;
//	private enemyBoat enemy5;
	private float totalForce;
	private float averageForce;
	private Boolean notSaved;
	private Boolean clone1;
	private Boolean clone2;

	private float speed1;
	private float speed2;
	private float speed3;
	private float speed4;
	private float speed5;

	private float startTime;

	private Boolean start;
	private Boolean noRepeat;

	public Text winText;

	// Use this for initialization
	void Start () 
	{

		//print (Application.persistentDataPath);

		player = GameObject.Find ("player").GetComponent (typeof(FloatBoat)) as FloatBoat;
		enemy1 = GameObject.Find ("enemy boat 1").GetComponent (typeof(enemyBoat)) as enemyBoat;
		enemy2 = GameObject.Find ("enemy boat 2").GetComponent (typeof(enemyBoat)) as enemyBoat;
//		enemy3 = GameObject.Find ("enemy boat 3").GetComponent (typeof(enemyBoat)) as enemyBoat;
//		enemy4 = GameObject.Find ("enemy boat 4").GetComponent (typeof(enemyBoat)) as enemyBoat;
//		enemy5 = GameObject.Find ("enemy boat 5").GetComponent (typeof(enemyBoat)) as enemyBoat;

		enemy1.speed = 0;
		enemy2.speed = 0;

		start = false;
		noRepeat = true;

		totalForce = 0;
		averageForce = 0;
		notSaved = true;
		clone1 = true;
		clone2 = true;

		Load ();
	}
	
	//called every time the physics update
	void FixedUpdate()
	{
		if (player.currentForce < 0) 
		{
			start = true;
		}

		if (noRepeat && start) 
		{
			enemy1.speed = speed1;
			enemy2.speed = speed2;
//			enemy3.speed = speed3;
//			enemy4.speed = speed4;
//			enemy5.speed = speed5;
			startTime = Time.time;
			noRepeat = false;
		}

		totalForce += player.currentForce;
		print (player.transform.position.z);
		if (notSaved && player.transform.position.z > 100) 
		{
			//print (totalForce);
			float currentTime = Time.time;
			float time = (currentTime - startTime)*50;
			//print (time);
			averageForce = totalForce / time;
			//print (averageForce);
			Save ();
			notSaved = false;

			winText.text = winText.text + "Your time: " + (currentTime - startTime) + "\n";
		}
		if (clone1 && enemy1.transform.position.z > 100) 
		{
			clone1 = false;
			winText.text = winText.text + "Your Last time: " + (Time.time - startTime) + "\n";
		}
		if (clone2 && enemy2.transform.position.z > 100) 
		{
			clone2 = false;
			winText.text = winText.text + "Your Last Last time: " + (Time.time - startTime) + "\n";
		}
	}

	void Save()
	{
//		print ("save\n");
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/playerData.dat", FileMode.Open);

		PlayerData data = new PlayerData ();
		data.boat1 = averageForce;
		data.boat2 = enemy1.speed;
//		data.boat3 = enemy2.speed;
//		data.boat4 = enemy3.speed;
//		data.boat5 = enemy4.speed;

		bf.Serialize (file, data);
		file.Close ();
	}

	void Load()
	{

		if (File.Exists (Application.persistentDataPath + "/playerData.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerData.dat", FileMode.Open);

			PlayerData data;
			try
			{
				data = (PlayerData)bf.Deserialize (file);
			}
			catch(System.Exception) 
			{
				data = new PlayerData ();
				data.boat1 = -4;
				data.boat2 = -4;
		//		data.boat3 = -4;
		//		data.boat4 = -4;
		//		data.boat5 = -4;
				bf.Serialize (file, data);

				speed1 = -4;
				speed2 = -4;
	//			speed3 = -4;
	//			speed4 = -4;
	//			speed5 = -4;
			}
			file.Close ();

			speed1 = data.boat1;
			speed2 = data.boat2;
//			speed3 = data.boat3;
//			speed4 = data.boat4;
//			speed5 = data.boat5;
		}
		else 
		{
			FileStream file = File.Open (Application.persistentDataPath + "/playerData.dat", FileMode.Create);
			BinaryFormatter bf = new BinaryFormatter ();
			PlayerData data = new PlayerData();
			data.boat1 = -4;
			data.boat2 = -4;
//		data.boat3 = -4;
//		data.boat4 = -4;
//		data.boat5 = -4;
			bf.Serialize (file, data);
			file.Close ();

			speed1 = -4;
			speed2 = -4;
//			speed3 = -4;
//			speed4 = -4;
//			speed5 = -4;
		}
	}
}

[Serializable]
class PlayerData
{
	public float boat1;
	public float boat2;
//	public float boat3;
//	public float boat4;
//	public float boat5;
}