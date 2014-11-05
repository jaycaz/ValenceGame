﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterReac : Chemical.Reaction {
	
	
//	public WaterReac() : base("Water Maker") {
		
//		reactants = new Dictionary<Chemical.Compound, int>();
//		reactants.Add (new H2 (), 2);
//		reactants.Add (new O2 (), 1);

//		products = new Dictionary<Chemical.Compound, int>();
//		reactants.Add (new Water (), 2);


//		reactant1 = this.holder.AddComponent<H2> ();
//		rCoeff1 = 2;
//		reactant2 = this.holder.AddComponent<O2> ();
//		rCoeff2 = 1;
//		reactant3 = null;
//		rCoeff3 = 0;
//		product1 = this.holder.AddComponent<Water> ();
//		pCoeff1 = 2;
//		product2 = null;
//		pCoeff2 = 0;
//		product3 = null;
//		pCoeff3 = 0;
//
//		energyType = energy.Refreshing;
//	}



	public override void Start () {
		unlocked = false;

		reactName = "Water Fusion";

		reactant1 = this.gameObject.AddComponent<H2> ();
		rCoeff1 = 2;
		reactant2 = this.gameObject.AddComponent<O2> ();
		rCoeff2 = 1;
		reactant3 = null;
		rCoeff3 = 0;
//		reactant3 = this.gameObject.AddComponent<CarbDiox>();
//		rCoeff3 = 2;
//		reactant2 = null;
//		rCoeff2 = 0;
//		reactant3 = this.gameObject.AddComponent<O2> ();
//		rCoeff3 = 1;
		product1 = this.gameObject.AddComponent<H2O> ();
		pCoeff1 = 2;
		product2 = null;
		pCoeff2 = 0;
		product3 = null;
		pCoeff3 = 0;
		
		energyType = energy.Refreshing;
	}
	
	// Update is called once per frame
	public override void Update () {
		//	
	}
	
	
	

	
	
}