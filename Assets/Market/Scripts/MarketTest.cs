using UnityEngine;
using System.Collections;

public class MarketTest : MonoBehaviour {

	public MarketEngine Market;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("9")){
			Market.ProcessAllNewMarketValues();
		}

		if(Input.GetKeyDown("0")){
			foreach(var Key in Market.ElementTable.Keys){
				Element ThisElement = (Element)Market.ElementTable[Key];
				print (ThisElement.Name + "'s price is " + ThisElement.CurrentMarketValue);
			}
		}
	}
}
