﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chemical {

	public abstract class Reaction {
	    // A reaction consumes one or more Reactants and creates one or more Products

		protected string reactName;
		protected GameObject holder;	//for holding Components
		//public string ReactName {
		//	get {
		//		return reactName;
		//	}
		//}

		//LET'S TRY USING INSTANCE ID's OF COMPOUND!!
		//protected Dictionary<Chemical.Compound, int> reactants;	//contains correct coeff ratios
			//want coeffs given compounds
			//want list of compounds
		//-> maybe use accessors?



		//MODIFYING THIS TO TAKE INSTANCE IDs (which are ints), BECAUSE THEY ARE UNIQUE FOR EACH SCRIPT!
		//protected Dictionary<int, int> reactants;	//contains correct coeff ratios
		

		//protected Dictionary<Chemical.Compound, int> products;	//contains correct coeff ratios
		//public Dictionary<Chemical.Compound, int>.KeyCollection Products {
		//	get {
		//		return products.Keys;
		//	}
		
		//}

		//so it looks like monobehaviours can't (read: shouldn't) be used as keys to a dictionary.
			//i thought about using getInstanceID(), which is unique for each script, 
			//but i couldn't find a way to get the "compound" back using that ID.

		//instead i'll have to do something less elegant and more prone to bugs, until we can 
			//find a better solution.

		protected Chemical.Compound reactant1;
		public Chemical.Compound Reactant1
		{
			get {
				return reactant1;
			}
		}

		protected int rCoeff1;
		public int ReactCoeff1
		{
			get {
				return rCoeff1;
			}
		}

		protected Chemical.Compound reactant2;
		public Chemical.Compound Reactant2
		{
			get {
				return reactant2;
			}
		}

		protected int rCoeff2;
		public int ReactCoeff2
		{
			get {
				return rCoeff2;
			}
		}

		protected Chemical.Compound reactant3;
		public Chemical.Compound Reactant3
		{
			get {
				return reactant3;
			}
		}

		protected int rCoeff3;
		public int ReactCoeff3
		{
			get {
				return rCoeff3;
			}
		}

		protected Chemical.Compound product1;
		public Chemical.Compound Product1
		{
			get {
				return product1;
			}
		}

		protected int pCoeff1;
		public int ProdCoeff1
		{
			get {
				return pCoeff1;
			}
		}

		protected Chemical.Compound product2;
		public Chemical.Compound Product2
		{
			get {
				return product2;
			}
		}

		protected int pCoeff2;
		public int ProdCoeff2
		{
			get {
				return pCoeff2;
			}
		}

		protected Chemical.Compound product3;
		public Chemical.Compound Product3
		{
			get {
				return product3;
			}
		}

		protected int pCoeff3;
		public int ProdCoeff3
		{
			get {
				return pCoeff3;
			}
		}


		protected energy energyType;


		protected enum energy {
			Exotherm, Endotherm, Refreshing, None
		};


		protected Reaction(string rN) {
			reactName = rN;
		}

		public string getReactName() {
			return reactName;
		}
	
	}
}