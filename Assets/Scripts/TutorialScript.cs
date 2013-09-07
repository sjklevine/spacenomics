using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {
	Rect nextRect;

	void Start () {
		nextRect = new Rect(Screen.width*0.4f, Screen.height*0.8f, Screen.width*0.2f, Screen.height*0.15f);
	}
	

	void OnGUI() {
		if (GUI.Button(nextRect, "CONTINUE")) {
			Application.LoadLevel(2);
		}
	}
}
