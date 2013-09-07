using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GalaxyMap : MonoBehaviour {


	public List<Planet> worlds; 
	private bool showMenu = false;
	// Use this for initialization

	public Planet selected = null;
	public Planet current = null; 

	public GameObject Ship = null;
	public InfoBox theBox;

	private Color showColor = new Color (1.0f, 1.0f,1.0f);
	private Color hiddenColor = new Color (.3f, .3f,.3f);

	void OnEnable(){
		//these events are obsolete, replaced by onMultiTapE, but it's still usable
		Gesture.onShortTapE += OnTapShort;
		Gesture.onLongTapE += OnTap;
		

	}

	public GalaxyMap (){
		//selected = worlds [0];
	}
	void OnDisable(){
		Gesture.onShortTapE -= OnTapShort;
		Gesture.onLongTapE -= OnTap;
	}



	//called when a long tap event is ended
	void OnTap(Tap tap){
				//do a raycast base on the position of the tap
				Ray ray = Camera.main.ScreenPointToRay (tap.pos);
				rayCheck (ray);
	}

	void Start(){
		current = worlds [GameModel.Instance.thePlanet];
		selected = worlds [GameModel.Instance.thePlanet];
	}
 
	void OnTapShort(Vector2 tap){
		//do a raycast base on the position of the tap
		Ray ray = Camera.main.ScreenPointToRay(tap);
		rayCheck(ray);
	}
	void rayCheck( Ray r){
		RaycastHit hit;
		//if the tap lands on the longTapObj
		if (Physics.Raycast (r, out hit, Mathf.Infinity)) {
			Debug.Log(showMenu);
			if (!showMenu) {
				foreach (Planet p in worlds) {
					if (hit.collider.transform == p.transform) {
						selected = p;
						showMenu = true;
						showActionMenu ();
						break;
					}
				}
			} else {
				if( theBox.Action.transform == hit.collider.transform && selected.Equals(current) && current.hasMineralRights){
					MinePlanet();
				}else if(theBox.Market.transform == hit.collider.transform  && !selected.hasMineralRights){
					SetMiningRight();
				}else if (theBox.Travel.transform == hit.collider.transform && GameModel.Instance.money >= 50 ) {
					MoveToPlanet();
				}else if (theBox.Cancel.transform == hit.collider.transform  ) {
					//Nothing
					showMenu = false;
					theBox.gameObject.SetActive(false);
					this.renderer.material.color = showColor;
				}

				
			}
		}
				
	}

	void showActionMenu(){

		theBox.BuyText.gameObject.GetComponent<TextMesh>().text = "Buy Cost: "+ this.selected.buyCost;
		theBox.TravelText.gameObject.GetComponent<TextMesh>().text = "Travel Cost: "+ this.selected.travelCost;

		theBox.gameObject.SetActive(true);

		if(selected.hasMineralRights){
			theBox.Market.gameObject.renderer.material.color = hiddenColor;
		}else{
			theBox.Market.gameObject.renderer.material.color = showColor;
		}

		if(!selected.Equals(current)|| !current.hasMineralRights){
			theBox.Action.gameObject.renderer.material.color = hiddenColor;
		}else{
			theBox.Action.gameObject.renderer.material.color = showColor;
		}



		if( GameModel.Instance.money < 50){
			theBox.Travel.gameObject.renderer.material.color = hiddenColor;
		}else{
			theBox.Travel.gameObject.renderer.material.color = showColor;
		}
		
		//theBox.Market.gameObject.SetActive (!selected.hasMineralRights);
		//showMenu = false;
		this.renderer.material.color = hiddenColor;

	} 

	public void MinePlanet(){
		showMenu = false;
		GameModel.Instance.thePlanet = worlds.FindIndex ( delegate(Planet bk)
		                                                 {
			return bk.Equals(current);
		});


		if (current != worlds [0]) {
			Application.LoadLevel ("World");

		} else {
			Application.LoadLevel ("Market");
		}
	}
	
	public void SetMiningRight(){
		showMenu = false;
		selected.hasMineralRights = true;
		GameModel.Instance.money = GameModel.Instance.money - current.buyCost;
		theBox.gameObject.SetActive(false);
		this.renderer.material.color = showColor;
	}
	
	public void MoveToPlanet(){
		showMenu = false;
		current = selected;
		theBox.gameObject.SetActive(false);
		GameModel.Instance.money = GameModel.Instance.money - current.travelCost;
		Ship.transform.parent = current.transform;
		Ship.transform.localPosition=  new Vector3(0.0f,0.0f,-2);
		this.renderer.material.color = showColor;
	}
}
