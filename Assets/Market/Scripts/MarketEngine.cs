using UnityEngine;
using System.Collections;

public class MarketEngine : MonoBehaviour {
	
	public Hashtable ElementTable;
	[SerializeField]
	private Element[] Elements;
	
	void Awake(){
		InitializeElementTable();
		ProcessAllNewMarketValues();
	}

	void InitializeElementTable(){
		ElementTable = new Hashtable();
		foreach(Element ThisElement in Elements){
			ElementTable.Add(ThisElement.Name, ThisElement);
		}
	}

	public void ProcessAllNewMarketValues(bool UseGlobalForces = true){
		foreach(var Key in ElementTable.Keys){
			Element ThisElement = (Element) ElementTable[Key];
			ThisElement.CalculateNewMarketValue(UseGlobalForces);
		}
	}
}

[System.Serializable]
public class Element{
	public string Name;
	public int MarketSupply;
	
	[SerializeField]
	private float MinimumValue;
	[SerializeField]
	private float MaximumValue;
	[SerializeField]
	private float MarketVariance;

	private float SupplyVariance = 0.05f;
	
	public float CurrentMarketValue;

	public void CalculateNewMarketValue(bool UseGlobalForces = false){
		//Apply Supply Variance
		if(UseGlobalForces){
			MarketSupply = (int)((float)MarketSupply * (1f + Random.Range(-SupplyVariance, SupplyVariance)));

			//Minimum Supply Value of 5 every time
			if(MarketSupply < 5) MarketSupply = 5;
		}

		//Calculate new price
		float PriceModifier = (MaximumValue - MinimumValue)/100f;
		CurrentMarketValue = MaximumValue - (PriceModifier * MarketSupply);

		//Make sure the price is not below the minimum floor
		CurrentMarketValue = CurrentMarketValue > MinimumValue ? CurrentMarketValue : MinimumValue;

		if(UseGlobalForces){
			//Apply Value Variance
			CurrentMarketValue *= (1f + Random.Range(-MarketVariance, MarketVariance));
		}
	}
}
