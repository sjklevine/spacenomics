using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MarketController : MonoBehaviour {
	public GUIText[] myAmountLabels;
	public GUIText[] marketAmountLabels;
	public GUIText[] priceLabels;

	private MarketEngine myMarket;

	void Start() {
		//Grab references!
		myMarket = this.GetComponent<MarketEngine>();
	}

	void OnEnable(){
		//these events are obsolete, replaced by onMultiTapE, but it's still usable
		Gesture.onShortTapE += OnTapShort;
		Gesture.onLongTapE += OnTapLong;
	}
	
	void OnDisable(){
		Gesture.onShortTapE -= OnTapShort;
		Gesture.onLongTapE -= OnTapLong;
	}
	void OnTapLong(Tap tap){
		OnTapAny(tap.pos);
	}
	void OnTapShort(Vector2 tap){
		OnTapAny(tap);
	}

	void OnTapAny(Vector2 tap) {
		//do a raycast base on the position of the tap
		Ray ray = Camera.main.ScreenPointToRay(tap);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			//ASSUMING BUY FOR NOW
			string commodity = "Obtanium";
			buy(commodity);
			//Here, iterate through your stuff
			/*
			foreach(Planet p in worlds){
				if (hit.collider.transform == p.transform) {
					Debug.Log(p.planetName);
				}
			}
			*/
		}
	}

	void buy(string commodityString) {
		//Check to see if you can buy (cash and inv available)
		//TODO

		//Put a piece of cargo in your ship
		int newCargoAmount = (int)GameModel.Instance.cargo[commodityString] + 1;
		GameModel.Instance.cargo[commodityString] = newCargoAmount;

		//Remove the cargo from the market
		Element thisElement = (Element)myMarket.ElementTable[commodityString];
		thisElement.Supply -= 1;

		//Update the market prices!
		thisElement.CalculateNewMarketValue();
	}

	void updateEverything() {
		for (int i = 0; i < myAmountLabels.Length; i++) {
			myAmountLabels[i].text = GameModel.Instance.cargo[i].ToString();
		}
		for (int i = 0; i < marketAmountLabels.Length; i++) {
			string commodity = GameModel.elementNames[i];
			Element thisElement = (Element) myMarket.ElementTable[commodity];
			marketAmountLabels[i].text = thisElement.Supply.ToString();
		}
		for (int i = 0; i < priceLabels.Length; i++) {
			string commodity = GameModel.elementNames[i];
			Element thisElement = (Element) myMarket.ElementTable[commodity];
			priceLabels[i].text = thisElement.CurrentMarketValue.ToString();
		}
	}

}
