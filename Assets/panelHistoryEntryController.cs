using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class panelHistoryEntryController : MonoBehaviour {
	public Text  name;
	void setName(string sName){
		name.text = sName;
	}
}
