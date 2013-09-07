using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {
	Rect startRect;


	void Start () {
		startRect = new Rect(Screen.width*0.4f, Screen.height*0.78f, Screen.width*0.2f, Screen.height*0.15f);
	}
	

	void OnGUI() {
		if (GUI.Button(startRect, "START")) {
			Application.LoadLevel(1);
		}
	}
}
