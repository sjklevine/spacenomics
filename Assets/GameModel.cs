using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;

public class GameModel
{
	private static GameModel instance;
	public static string[] elementNames = {"Spice", "Unobtanium", "Oxy Ale", "Vesper Gas", "The Fifth Element", "Obtanium"};
	public Hashtable cargo;

	public GameModel () 
	{
		if (instance != null)
		{
			Debug.LogError ("Cannot have two instances of singleton. Self destruction in 3...");
			return;
		}
		
		instance = this;
		cargo = new Hashtable();
		foreach (string name in elementNames) {
			cargo.Add(name, 0);
		}
	}
 
	public static GameModel Instance
	{
		get
		{
			if (instance == null)
			{
				new GameModel ();
			}
 
			return instance;
		}
	}

}
