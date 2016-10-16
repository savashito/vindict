using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelMainController : MonoBehaviour {
	public GameObject panelHistoryEntry;
	// Use this for initialization
	void Start () {
		
		// Add as many panels as entries fit
		for( int i=0;i<12;i++){
			insertEntry (i, "Melanie");
		}
		panelHistoryEntry.gameObject.SetActive (false);
		// this.transform.parent
	}
	GameObject insertEntry(int index,string sName){
		GameObject entry = Instantiate(panelHistoryEntry,panelHistoryEntry.transform.position, panelHistoryEntry.transform.rotation) as GameObject;
		entry.transform.parent = this.transform;
		entry.transform.localScale = entry.transform.lossyScale;
		entry.transform.position = entry.transform.position + new Vector3 (0f, -3f*index, 0f);
		setName(entry,sName+index);
		setDistance (entry, 69.7f);
		setDate (entry,29,7,1989);
		setTime (entry, 6932);
		return entry;
	}
	void setAttribute(GameObject entry,string attribute, string sText){
		Text nameText = entry.transform.FindChild(attribute).GetComponent<Text>();
		nameText.text = sText;
	}
	void setDate(GameObject entry,int day,int month,int year){
		setAttribute (entry, "TextDate", string.Format("{0}/{1}/{2}",day,month,year));
	}
	void setDistance(GameObject entry,float d){
		setAttribute (entry, "TextDistance", d + " m");
	}
	void setTime(GameObject entry,int t){
		setAttribute (entry, "TextTime", TextController.timeToString( t));
	}
	void setName(GameObject entry,string sName){
		Text nameText = entry.transform.FindChild("TextName").GetComponent<Text>();
		nameText.text = sName;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
