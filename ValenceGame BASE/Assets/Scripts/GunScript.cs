﻿using UnityEngine;
using System.Collections;

public struct Tank {
	// Name of compound stored in tank
	public string name;
	// Rate at which a compound will be consumed
	public int rate;
	// Current amount of compound stored in tank
	public int capacity;

	public Tank(string n, int rt, int cp)
	{
		name = n;
		rate = rt;
		capacity = cp;
	}

	public Tank(string n, int rt)
	: this(n, rt, 0)
	{}
}

public class GunScript : MonoBehaviour
{
	public int power;
	//never used in GunScript
	public int numclicks;
	//what is this for?

	//public GameObject emitter;  //HARDCODE

	public bool isEmitting;
	public bool isEmpty;
	public bool reactSelected;	//was 'isEquation' //is there a specific equation that is being used to get elements
		//what does this do?
	public bool eqBalanced;		//was 'balanced'

	public GameObject effect;   //HARDCODE

	// Tanks for reactants
	public Tank reactTank1;
	public Tank reactTank2;
	public Tank reactTank3;

	// Tanks for products
	public Tank prodTank1;
	public Tank prodTank2;
	public Tank prodTank3;

	//compound capacities in tank
	public int combineCap;	
	private int fullCap = 400; //needs to be private to be accessed  by gui

	//compond names and compound rates will need to be set by the reaction
	//compound names
	public string cursorName;	//was "elemName"

	public int sprayDamage;		//to use with different elements and objects

	public string chemToShootName;
	public Chemical.Compound chemToShoot;
   	public Chemical.Reaction activeReact;
	public GameObject shootEffect;

	//used for different particle emitters
	public GameObject absorb1;		//for compound 1
	public GameObject absorb2;		//for compound 2

	public int getFullCap()  //So GUI can know the maximum capacity in order to scale the gui to screen
	{
		return fullCap;
	}

	// Use this for initialization
	void Start()
	{
		isEmpty = true;
		isEmitting = false;
		eqBalanced = false;
		reactSelected = false;
		combineCap = 0;
		cursorName = " ";
		reactTank1 = new Tank ("O2", 1);  // HARD CODED
		reactTank2 = new Tank ("H2", 2);  // HARD CODED

		//perhaps include tankElem3 at some point?

		sprayDamage = 0;
	}

	// Update is called once per frame
	void Update()
	{
		//venting
		if (Input.GetKeyDown(KeyCode.Q))
		{
			reactTank1.capacity = 0;
			reactTank2.capacity = 0;
		}
		//identifying elements
		Ray ray1 = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

		RaycastHit hit;
		Physics.Raycast(ray1, out hit, 1000f);

		if (hit.transform.tag == "Absorbable")
		{
			if (hit.distance < 8)
			{
				cursorName = hit.transform.name;
			}
		}
		else
		{
			cursorName = " ";
		}

		if (eqBalanced && this.gameObject.GetComponent<Chemical.Compound>() == null)
		{
			//SEMI-HARDCODE NEEDS TO BE CHANGED WHEN PRODUCT SELECTION IMPLEMENTED
			chemToShootName = activeReact.Product1.CompoundName;

			chemToShoot = this.gameObject.AddComponent(chemToShootName) as Chemical.Compound;
			chemToShoot.init();

			shootEffect = GameObject.Find("ShootGun").GetComponent<GunParticleSwitcher>().setParticleSystem(chemToShoot.state);

		}

		//identifying objects (for damaging)
		if (this.gameObject.GetComponent<Chemical.Compound>() != null && !isEmpty)
		{
			sprayDamage = chemToShoot.damage(hit.transform.name);
			//this will be used in script Extinguished

			//shootEffect.GetComponent<Extinguished>().particleDamage = sprayDamage;
		}

		//absorbing
		if (Input.GetMouseButton(1) && (eqBalanced || !reactSelected))
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

			RaycastHit hit2;
			Physics.Raycast(ray, out hit2, 1000f);

			if (hit2.transform.tag == "Absorbable")
			{
				if (hit2.distance < 8)
				{
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3(hit2.normal.x - ray.direction.x, hit2.normal.y - ray.direction.y, hit2.normal.z - ray.direction.z);

					if (reactSelected && activeReact != null)
					{	//specific absorb if specific reaction selected
						if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == activeReact.Reactant1.getFormula())
						{
							reactTank1.name = activeReact.Reactant1.getFormula();
							reactTank1.rate = activeReact.ReactCoeff1;
							if (reactTank1.capacity < fullCap)
							{
								reactTank1.capacity += 2;

								Instantiate(effect, hit2.point, q);
								isEmpty = false;

							}
						}
						if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == activeReact.Reactant2.getFormula())
						{
							reactTank2.name = activeReact.Reactant2.getFormula();
							reactTank2.rate = activeReact.ReactCoeff2;

							if (reactTank2.capacity < fullCap)
							{
								reactTank2.capacity += 2;
								Instantiate(effect, hit2.point, q);
								isEmpty = false;
							}
						}
					}
					else if (!reactSelected)
					{	//to absorb anything without specific reaction selected
						if (reactTank1.capacity == 0)  //can absorb anything into tank1
						{
							reactTank1.name = hit2.transform.GetComponent<Chemical.Compound>().getFormula();
							chemToShoot = this.gameObject.AddComponent(reactTank1.name) as Chemical.Compound;
							chemToShoot.init();

							shootEffect = GameObject.Find("ShootGun").GetComponent<GunParticleSwitcher>().setParticleSystem(chemToShoot.state);
							
							reactTank1.capacity += 2;

							Instantiate(effect, hit2.point, q);
							isEmpty = false;
						}
						else if (hit2.transform.GetComponent<Chemical.Compound>().getFormula() == reactTank1.name)
						{
							if (reactTank1.capacity < fullCap)
							{
								reactTank1.capacity += 2;

								Instantiate(effect, hit2.point, q);
								isEmpty = false;
							}
						}
					}
				}
			}
		}


		//shooting
		//if (eqBalanced)
		if (eqBalanced || !reactSelected)	//to allow to shoot if no reaction selected
		{
			if (Input.GetButtonDown("Fire1"))
			{
				numclicks++;
				if (!isEmitting && !isEmpty)	//will need to code the absorb mechanic
				{
					isEmitting = true;
					shootEffect.particleSystem.Play();
				}
			}
			if (Input.GetButton("Fire1"))
			{
				if (isEmitting && !isEmpty)
				{
					if (reactSelected && activeReact != null) //is using more than one element
					{
						if (reactTank1.capacity > 0 && reactTank2.capacity > 0)
						{
							isEmpty = false;
							isEmitting = true;
							shootEffect.particleSystem.Play();
							reactTank1.capacity -= 1 * reactTank1.rate;
							reactTank2.capacity -= 1 * reactTank2.rate;
						}
						else
						{
							isEmpty = true;
							isEmitting = false;
							shootEffect.particleSystem.Stop();
						}
					}
					else //not a reaction, just single compound
					{
						if (reactTank1.capacity > 0)
						{
							isEmpty = false;
							isEmitting = true;
							shootEffect.particleSystem.Play();
							reactTank1.capacity -= 1 * reactTank1.rate;
						}
						else
						{
							isEmpty = true;
							isEmitting = false;
							shootEffect.particleSystem.Stop();
						}
					}
				}
			}
			if (Input.GetButtonUp("Fire1")) //stop emitting
			{
				if (isEmitting)
				{
					isEmitting = false;
					shootEffect.particleSystem.Stop();

					if (reactTank1.capacity <= 0 && reactTank1.capacity <= 0)
					{
						isEmpty = true;
					}
				}
			}
		}
	}
}
