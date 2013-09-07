using UnityEngine;
using System.Collections;

public class MarketGUI : MonoBehaviour{
	public CommodityDisplay[] Display;
	public GUIText YourMoney;
	public GUIText MarketMoney;

	public Hashtable DisplayTable;

	void Awake(){
		//Initialize hashtable
		DisplayTable = new Hashtable();
		foreach(CommodityDisplay ThisDisplay in Display){
			DisplayTable.Add(ThisDisplay.CommodityName, ThisDisplay);
		}
	}
}

[System.Serializable]
public class CommodityDisplay{
	public string CommodityName;
	public GUIText Price;
	public GUIText YourSupply;
	public GUIText MarketSupply;
}
