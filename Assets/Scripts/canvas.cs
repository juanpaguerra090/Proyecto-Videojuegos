using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class canvas : MonoBehaviour {

	//Buttons
	public GameObject nextLevel;
	public GameObject upgrade; 
	public GameObject sell;
	//Fish Type Spawners / Setters
	public GameObject spFish1; 
	public GameObject spFish2;
	public GameObject spFish3; 
	public GameObject spFish4;
	//Game variables
	private int money;
	private int numFish;
	private int pos;
	private int maxCap;
	//Game info
	private GameObject stats;
	private game info;

	void Start () {
		stats = GameObject.Find ("stats");
		info = stats.GetComponent<game> ();

		nextLevel = GameObject.Find ("Level"); 
		upgrade = GameObject.Find ("Upgrade");
		sell = GameObject.Find ("Sell");
		spFish1 = GameObject.Find ("AddFish1");
		spFish2 = GameObject.Find ("AddFish2");
		spFish3 = GameObject.Find ("AddFish3");
		spFish4 = GameObject.Find ("AddFish4");

	}
	
	void Update () {
		getters ();
		checkLevelButtons ();
	}

	public void getters(){
		this.pos = info.pos;
		this.money = info.money;
		this.numFish = info.numFish;
		this.maxCap = info.maxCap;  
	}

	void checkLevelButtons(){
		if (this.pos > 0) {
			this.sell.GetComponent<CanvasGroup> ().interactable = true;
		} 
		else {
			this.sell.GetComponent<CanvasGroup> ().interactable = false;
		}

		if (this.money >= 750 && info.level != 4) {
			this.nextLevel.GetComponent<CanvasGroup> ().interactable = true;
		}
		else{
			this.nextLevel.GetComponent<CanvasGroup> ().interactable = false;
		}
		if (this.money >= 160) {
			this.upgrade.GetComponent<CanvasGroup> ().interactable = true;
		} 
		else {
			this.upgrade.GetComponent<CanvasGroup> ().interactable = false;
		}

		if(info.level >= 0){
			spFish1.GetComponent<CanvasGroup> ().interactable = true;
		}
		if(info.level >= 1){
			spFish2.GetComponent<CanvasGroup> ().interactable = true;
		}
		if(info.level >= 2){
			spFish3.GetComponent<CanvasGroup> ().interactable = true;
		}
		if(info.level >= 3){
			spFish4.GetComponent<CanvasGroup> ().interactable = true;
		}
	}
}
