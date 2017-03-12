using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numFish : MonoBehaviour {


	//Presentes y Acumuladas publicas
	//Conteo global de peces vendidos por tipo
	public int soldFish1, 
				soldFish2,
				soldFish3,
				soldFish4,
				soldFish5,
				soldFish6,
				soldFish7,
				soldFish8,
				soldFish9;

	public int curFish1,
				curFish2,
				curFish3,
				curFish4,
				curFish5,
				curFish6,
				curFish7,
				curFish8,
				curFish9;

	public int getAllSoldFish(){
		return soldFish1 + soldFish2 + soldFish3 + soldFish4 + soldFish5 + soldFish6 + soldFish7 + soldFish8 + soldFish9;
	}



}
