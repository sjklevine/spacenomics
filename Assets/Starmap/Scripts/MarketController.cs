﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MarketController : MonoBehaviour {
	public GUIText[] myAmountLabels;
	public GUIText[] marketAmountLabels;
	public GUIText[] priceLabels;

	private MarketEngine myMarket;
	private MarketGUI myMarketGUI;

	void Start() {
		//Make Singleton!
		new GameModel();

		//Grab references!
		myMarket = GetComponent<MarketEngine>();
		myMarketGUI = GetComponentInChildren<MarketGUI>();

		//Update Market Prices
		updateMarketGUI();
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
			//Send Button to ButtonClick if it is tagged "Button"
			if(hit.transform.tag == "Button"){
				ButtonClick(hit.transform.name);
			}

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
		//Get Element from market
		Element thisElement = (Element)myMarket.ElementTable[commodityString];

		//Check to see if you can buy (cash and inv available)
		if(GameModel.Instance.money > (int)thisElement.CurrentMarketValue 
		   && thisElement.MarketSupply > 0){
			//Put a piece of cargo in your ship
			int newCargoAmount = (int)GameModel.Instance.cargo[commodityString] + 1;
			GameModel.Instance.cargo[commodityString] = newCargoAmount;

			//Remove the cargo from the market
			thisElement.MarketSupply -= 1;

			//Subtract the Moneh
			GameModel.Instance.money -= (int)thisElement.CurrentMarketValue;

			//Update the market prices!
			thisElement.CalculateNewMarketValue();

			//Update GUI
			updateMarketGUI();
		}
	}

	void sell(string commodityString){
		//Get Element from market
		Element thisElement = (Element)myMarket.ElementTable[commodityString];
		
		//Check to see if you can buy (cash and inv available)
//		if(GameModel.Instance.money > (int)thisElement.CurrentMarketValue 
//		   && thisElement.MarketSupply > 0){

		//Get Cargo from Hold
		int CargoAmount = (int)GameModel.Instance.cargo[commodityString];

		if(CargoAmount > 0){
			//Remove one Cargo from Ship
			GameModel.Instance.cargo[commodityString] = CargoAmount - 1;
			
			//Remove the cargo from the market
			thisElement.MarketSupply += 1;
			
			//Subtract the Moneh
			GameModel.Instance.money += (int)thisElement.CurrentMarketValue;
			
			//Update the market prices!
			thisElement.CalculateNewMarketValue();
			
			//Update GUI
			updateMarketGUI();
		}
	}

	void updateMarketGUI() {
		for(int i = 0; i < myMarketGUI.Display.Length; i++){
			//Get Commodity Name
			string commodityName = GameModel.elementNames[i];
			//Get Element
			Element thisElement = (Element)myMarket.ElementTable[commodityName];
			//Get GUI Stuff
			CommodityDisplay thisDisplay = (CommodityDisplay)myMarketGUI.DisplayTable[commodityName];

			//SET THE STUFF
			thisDisplay.Price.text = "Space $: " + thisElement.CurrentMarketValue.ToString("0.00");
			thisDisplay.MarketSupply.text = thisElement.MarketSupply.ToString() + " SpaceTons";
			thisDisplay.YourSupply.text = ((int)GameModel.Instance.cargo[commodityName]).ToString() + " SpaceTons";
		}

		myMarketGUI.YourMoney.text = "Space $: " + GameModel.Instance.money.ToString("0.00");
	}

	void ButtonClick(string buttonName){
		string[] StringArray = buttonName.Split('.');
		switch(StringArray[0]){
			case "Buy":
				buy(StringArray[1]);
				break;
			case "Sell":
				sell(StringArray[1]);
				break;
			case "Back":
				Application.LoadLevel(2);
				break;
			default:
				break;
		}
	}
}
