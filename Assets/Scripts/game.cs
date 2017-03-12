using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour {
	//Array of fully grown Fish
	public GameObject[] array;

	//prefab (just fish1)

	public GameObject fish1, fish2, fish3, fish4;
	private GameObject [] prefabs;

	//Time
	public int[] timePerFish;
	int time1, time2, time3, time4;
	public int [] timeSinceAdded;

	//Prices
	public int [] prices;
	int price1, price2, price3, price4;

	//Game Stats
	public Text ShowMoney;
	public Text ShowSpace;

	public int pos;
	public int maxCap;
	public int numFish;
	public int level;
	public int money;
	public int fishPrice;
	public int time;

	private int start = 0; 
	public int setFish;
	private GameObject stats;
	private canvas info;

	private numFish globalStats;

	DateTime date;
	 
	void Start () { //Initial variable conifuration/setup
		this.prefabs = new GameObject[4];
		this.prefabs[0] = this.fish1;
		this.prefabs[1] = this.fish2;
		this.prefabs[2] = this.fish3;
		this.prefabs[3] = this.fish4;

		this.start = PlayerPrefs.GetInt ("start");

		this.prices = new int[4];
		this.timePerFish = new int[4];
		this.timeSinceAdded = new int[this.maxCap];
		this.array = new GameObject[maxCap];


		if (this.start == 0) {
			this.level = 0;
			this.maxCap = 30;
			this.money = 5000;
			this.numFish = 0;

			this.price1 = 20;
			this.price2 = 40;
			this.price3 = 80;
			this.price4 = 160;

			this.time1 = 18;
			this.time2 = 27;
			this.time3 = 36;
			this.time4 = 45;
				
			this.prices[0] = this.price1;
			this.prices[1] = this.price2;
			this.prices[2] = this.price3;
			this.prices[3] = this.price4;

			this.timePerFish[0] = this.time1;
			this.timePerFish[1] = this.time2;
			this.timePerFish[2] = this.time3;
			this.timePerFish[3] = this.time4;

		}
		else {
			this.level = PlayerPrefs.GetInt ("Level");
			this.maxCap = PlayerPrefs.GetInt ("maxCap");
			this.money = PlayerPrefs.GetInt ("Money");
			this.numFish = PlayerPrefs.GetInt ("Fish");
			this.prices[0] = PlayerPrefs.GetInt ("price1");
			this.prices[1] = PlayerPrefs.GetInt ("price2");
			this.prices[2] = PlayerPrefs.GetInt ("price3");
			this.prices[3] = PlayerPrefs.GetInt ("price4");
			this.timePerFish[0] = PlayerPrefs.GetInt ("time1");
			this.timePerFish[1] = PlayerPrefs.GetInt ("time2");
			this.timePerFish[2] = PlayerPrefs.GetInt ("time3");
			this.timePerFish[3] = PlayerPrefs.GetInt ("time4");
		}

		this.pos = 0;
		this.fishPrice = 20;
		this.setFish = 0;

		stats = GameObject.Find ("stats");
		this.globalStats = stats.GetComponent<numFish>();
		info = stats.GetComponent<canvas> ();
		showText ();
		timeBonus ();
		addFishOnLoad (); // gets the count of gameobjects with "Tag"
		getSeconds();
		InvokeRepeating("Save",5, 5);
		InvokeRepeating("freeMoney",.01f, 3);
	}

	void Update () {
	}

	void timeBonus (){
		int restTime = PlayerPrefs.GetInt ("Time"); 
		this.money += Mathf.Abs (PlayerPrefs.GetInt ("Time") - System.DateTime.Now.Hour) * 200; // Genera $ 200 por cada hora ausente.
		showText();
	}

	void saveSeconds(){  
		//To save current time to PlayerPrefs
			//gets current date
		this.date = DateTime.Now;

			//convert to binary
		long date_bin = date.ToBinary();

			//save binary as string
		PlayerPrefs.SetString("Date", date_bin.ToString());
	}

	void getSeconds(){
		// Retrieve current time from PLayerPrefs to get time difference
			//get the binary back from the save data
		long old_date_bin = long.Parse(PlayerPrefs.GetString("Date"));
			//convert from binary to datetime.
		DateTime oldDate = DateTime.FromBinary(old_date_bin);
			//get the time difference
		DateTime newDate = DateTime.Now;
		TimeSpan ts = newDate - oldDate;
			//get the difference in days or seconds or whatever
		int seconds = (int) Math.Ceiling(ts.TotalSeconds);
		Debug.Log("SECONDS DIFFERENCE = " + seconds);
	}


	public void setLevel(int level){
		this.setFish = level;
	}

	void showText(){
		this.ShowMoney.text = this.money + "";
		this.ShowSpace.text = this.numFish + "/" + this.maxCap;
	}

	void createFish(int setFish){
		GameObject nuevo = Instantiate (prefabs[setFish], new Vector3 (UnityEngine.Random.Range (-5f, 5f), UnityEngine.Random.Range (-3.5f, 3.5f), 0), Quaternion.identity) as GameObject;
		int num = UnityEngine.Random.Range (-5, 5);
		if (num < 0) {
			nuevo.transform.Rotate (180, 0, 180 + num);
		}

		moveFish info = nuevo.GetComponent<moveFish> ();
		info.setTime (this.timePerFish[setFish]);
		info.setPrice (this.prices[setFish] + (this.prices[setFish])/2);
		showText ();
	}

	public void AddFish(){
		if (this.numFish < this.maxCap && this.money > this.prices[this.setFish]) {
			this.numFish++;
			this.money -= this.prices[this.setFish];
			this.createFish (this.setFish);
		}
	}

	void addFishOnLoad(){

		int numFish1 = PlayerPrefs.GetInt ("curFish1");
		int numFish2 = PlayerPrefs.GetInt ("curFish2");
		int numFish3 = PlayerPrefs.GetInt ("curFish3");
		int numFish4 = PlayerPrefs.GetInt ("curFish4");

		if ((numFish1 + numFish2 + numFish3 + numFish4 + 1) > 0) {
			if (numFish1 > 0) {
				for (int i = 0; i < numFish1; i++) {
					this.createFish (0);
				}
			}
			if (numFish2 > 0) {
				for (int i = 0; i < numFish2; i++) {
					this.createFish (1);
				}
			}
			if (numFish3 > 0) {
				for (int i = 0; i < numFish3; i++) {
					this.createFish (2);
				}
			}
			if (numFish4 > 0) {
				for (int i = 0; i < numFish4; i++) {
					this.createFish (3);
				}
			}
		}
	}

	public void addArray(GameObject x){
		array[pos] = x;
		pos++;
	}

	public void increaseArray(int size){
		GameObject[] temp1 = this.array;
		this.array = new GameObject[size];
		for (int i = 0; i < temp1.Length; i++){
			this.array[i] = temp1[i];
		}

		int [] temp2 = this.timeSinceAdded;
		this.timeSinceAdded = new int[size];
		for (int i = 0; i < temp2.Length; i++){
			this.timeSinceAdded[i] = temp2[i];
		}
	}

	public void sellAll(){
		if (this.pos > 0){
			int i;
			for(i = 0; i < this.pos; i++){
				moveFish info = array[i].GetComponent<moveFish> ();
				this.money += info.getPrice ();
				Destroy (array[i].gameObject);
			}
			this.numFish -= this.pos;
			this.pos = 0;
		}
		showText ();
	}

	public void freeMoney(){
		this.ShowMoney.text = this.money + "";
		this.money += 5;
		this.info.getters ();
	}

	public void upgradeSpace(){
		if (this.money > 150) {
			this.money -= 150;
			this.maxCap += 10;
			this.increaseArray (this.maxCap);
			this.ShowSpace.text = this.numFish + "/" + this.maxCap;
		}
	}
		
	public void upgradeTank(){ 
		this.level++;
		this.money -= 750;
		this.ShowMoney.text = this.money + "";
	}

	public void Save(){

		//Game started at least 1
		PlayerPrefs.SetInt ("start", 1);
		//Sets global stats for number of current fish
		globalStats.curFish1 = GameObject.FindGameObjectsWithTag ("Fish1").Length;
		globalStats.curFish2 = GameObject.FindGameObjectsWithTag ("Fish2").Length;
		globalStats.curFish3 = GameObject.FindGameObjectsWithTag ("Fish3").Length;
		globalStats.curFish4 = GameObject.FindGameObjectsWithTag ("Fish4").Length;

		//Saves number of current fish of type n.
		PlayerPrefs.SetInt ("curFish1", globalStats.curFish1);
		PlayerPrefs.SetInt ("curFish2", globalStats.curFish2);
		PlayerPrefs.SetInt ("curFish3", globalStats.curFish3);
		PlayerPrefs.SetInt ("curFish4", globalStats.curFish4);

		//Saves money, level, local time and elapsed time since exit.
		PlayerPrefs.SetInt ("Money", money);
		PlayerPrefs.SetInt ("Level", level);
		PlayerPrefs.SetInt ("Fish", numFish);
		PlayerPrefs.SetInt ("maxCap", this.maxCap);
		int sysHour = System.DateTime.Now.Hour;
		PlayerPrefs.SetInt ("Time", sysHour);
		saveSeconds (); // Calls function to establish timespan.



		PlayerPrefs.SetInt ("price1", price1);
		PlayerPrefs.SetInt ("price2", price2);
		PlayerPrefs.SetInt ("price3", price3);
		PlayerPrefs.SetInt ("price4", price4);

		PlayerPrefs.SetInt ("time1", time1);
		PlayerPrefs.SetInt ("time2", time2);
		PlayerPrefs.SetInt ("time3", time3);
		PlayerPrefs.SetInt ("time4", time4);
	}

	void OnApplicationQuit() {
		Save ();
		PlayerPrefs.Save ();
	}
}
