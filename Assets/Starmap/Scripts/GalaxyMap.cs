using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GalaxyMap : MonoBehaviour {


	public List<Planet> worlds; 

	private bool showMenu = false;
	// Use this for initialization




	void OnEnable(){
		//these events are obsolete, replaced by onMultiTapE, but it's still usable
		Gesture.onShortTapE += OnTapShort;
		Gesture.onLongTapE += OnTap;
		

	}
	
	void OnDisable(){
		Gesture.onShortTapE -= OnTapShort;
		Gesture.onLongTapE -= OnTap;
	}



	//called when a long tap event is ended
	void OnTap(Tap tap){
		//do a raycast base on the position of the tap
		Ray ray = Camera.main.ScreenPointToRay(tap.pos);
		RaycastHit hit;
		Debug.Log ("hit");
		if (!showMenu) {
			Debug.Log("HI1");
			//if the tap lands on the longTapObj
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
					foreach(Planet p in worlds){
					Debug.Log("HI2");
					if (hit.collider.transform == p.transform) {
							Debug.Log(p.planetName);
						}
					}
			}
		}
	}


	void OnTapShort(Vector2 tap){
		//do a raycast base on the position of the tap
		Ray ray = Camera.main.ScreenPointToRay(tap);
		RaycastHit hit;
		if (!showMenu) {
			//if the tap lands on the longTapObj
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				foreach(Planet p in worlds){
					if (hit.collider.transform == p.transform) {
						Debug.Log(p.planetName);
					}
				}
			}
		}
	}

}
