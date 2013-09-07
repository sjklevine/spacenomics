﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
	int cargoSpace = 50;

	public List<Blocks> cargo { get; private set;}

	void Start(){
		cargo = new List<Blocks>();
	}
	
	public bool AddCargo(Blocks b){
		if( cargo.Count >= cargoSpace){
			return false;
		}

		cargo.Add(b);

		return true;
	}
}