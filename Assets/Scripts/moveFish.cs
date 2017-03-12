using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFish : MonoBehaviour {

	public GameObject stats;
	private game variables;

	public int price = 0;
	public int time = 0;
	public int secs = 0;

	private bool size = false;
	private static int direction = -1;

	void Start () {

		int num = Random.Range (-5, 5);
		transform.Rotate (0, 0, num);
		InvokeRepeating("increase",0, 1);
		stats = GameObject.Find ("stats");
		variables = stats.GetComponent<game> ();

	}

	void Update () {
		transform.Translate (direction*-.5f*Time.deltaTime, 0, 0);

		if(this.size == true){
			CancelInvoke ();
			this.size = false;
			variables.addArray (this.gameObject);
		}
	}		

	void OnTriggerExit2D(Collider2D c){
		if(c.gameObject.name == "bagCollider"){
			changeDirection ();
		}
	}

	void changeDirection(){
		int num = Random.Range (-5, 5);
		transform.Rotate (180, 0, 180 + num);
	}

	void increase(){
		this.secs++;


		if(this.secs >= 0 && this.secs < (time/3)){
			transform.localScale = new Vector3 (0.3722777f, 0.3722777f, 0);
		}

		else if(this.secs >= (time/3) && this.secs < (2*time/3)){
			transform.localScale = new Vector3 (0.5f, 0.5f, 0);
		}

		else if(this.secs >= (2*time/3) && this.secs < (time)){
			transform.localScale = new Vector3 (0.63f, 0.63f, 0);
		}

		else if(this.secs == (time)){
			transform.localScale = new Vector3 (0.76f, 0.76f, 0);
			this.size = true;
		}
	}

	public void setPrice (int price){
		this.price = price;
	}

	public void setTime (int time){
		this.time = time;
	}

	public int getPrice(){
		return this.price;
	}
		
}
