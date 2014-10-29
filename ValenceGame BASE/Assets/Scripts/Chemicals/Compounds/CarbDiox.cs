﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarbDiox : Chemical.Compound {

//	public CarbDiox() : base("CO2") {

//		atoms = new Dictionary<Chemical.Element, int>();
//		atoms.Add (new Oxygen(), 2);
//		atoms.Add (new Carbon (), 1);
	
//	}

	public override int damage (string obstacleName) {
		
		return 0;
	}
	
	public override int heal (string obstacleName) {
		
		return 0;
	}


	/*
	public override void interact() {
		/*
		use "is()" to check type!
		 *
	}
	
	public override bool augment() {
		/*
		use "is()" to check type!
		 *
		return false;
	}
	
	public override bool remove() {
		/*
		use "is()" to check type!
		 *
		return false;
	}

	*/

	public override void Start() {
        compoundName = "CarbDiox";
        formula = "CO2";
        shooter = Resources.Load("GasParticles", typeof(GameObject)) as GameObject;

		atoms = new Dictionary<Chemical.Element, int>();
		atoms.Add (new Oxygen(), 2);
		atoms.Add (new Carbon (), 1);
	
	}
	
	public override void Update() {}
}
