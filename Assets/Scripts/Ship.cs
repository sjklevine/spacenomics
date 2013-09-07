using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
	int cargoSpace = 50;

	public TextMesh text;

	public List<Blocks> cargo { get; private set;}

	void Start(){
		cargo = new List<Blocks>();
	}
	
	public bool AddCargo(Blocks b){
		if( cargo.Count >= cargoSpace){
			return false;
		}

		cargo.Add(b);

		text.text = "Cargo " + cargo.Count.ToString("00") + "/50";

		int newCargoAmount = 0;

		switch(b){
		case Blocks.MoonRocks:
			newCargoAmount = (int)GameModel.Instance.cargo[GameModel.elementNames[5]] + 1;
			GameModel.Instance.cargo[commodityString] = newCargoAmount;
			break;
		case Blocks.Spice:
			newCargoAmount = (int)GameModel.Instance.cargo[GameModel.elementNames[0]] + 1;
			GameModel.Instance.cargo[commodityString] = newCargoAmount;	
			break;
		case Blocks.Unobtainium:
			newCargoAmount = (int)GameModel.Instance.cargo[GameModel.elementNames[1]] + 1;
			GameModel.Instance.cargo[commodityString] = newCargoAmount;	
			break;
		case Blocks.OxyAle:
			newCargoAmount = (int)GameModel.Instance.cargo[GameModel.elementNames[2]] + 1;
			GameModel.Instance.cargo[commodityString] = newCargoAmount;	
			break;
		case Blocks.VesperGas:
			newCargoAmount = (int)GameModel.Instance.cargo[GameModel.elementNames[3]] + 1;
			GameModel.Instance.cargo[commodityString] = newCargoAmount;	
			break;
		case Blocks.TheFifthElement:
			newCargoAmount = (int)GameModel.Instance.cargo[GameModel.elementNames[4]] + 1;
			GameModel.Instance.cargo[commodityString] = newCargoAmount;	
			break;

		return true;
	}
}
