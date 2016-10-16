using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
public class TextController : MonoBehaviour {
	public Text textDebug;
	public Text textDistance;
	public Text textParcial;
	public Text textWatts;
	public Text textTime;
	public Text textSPM;



	public static string timeToString(float t){
		float m = Mathf.Floor(t/60);
		float s = Mathf.Floor(t%60);
		string sSeg,sMin;
		if (s < 10)
			sSeg = string.Format ("0{0}",s);
		else
			sSeg = string.Format ("{0}",s);
		if (m < 1)
			sMin = "";
		else
			sMin = string.Format ("{0}",m);
		return string.Format("{0}:{1}",sMin,sSeg);
	}

	public void SetFields(SocketIOEvent e){
		var data = e.data;
		float d = 0f, t = 0f;
		float power=0f,pace=0f,spm=0f;
		data.GetField(ref d,"distance");
		data.GetField(ref t,"time");
		data.GetField(ref power,"power");
		data.GetField(ref pace,"pace");
		data.GetField(ref spm,"spm");

		textDistance.text = string.Format("{0}",Mathf.Floor(d));
		textTime.text = timeToString (t);
		Debug.Log (t);
		textWatts.text = string.Format("{0}",power);
		textParcial.text = string.Format("{0}",timeToString(pace));
		textSPM.text = string.Format("{0}",spm);
	}
}
