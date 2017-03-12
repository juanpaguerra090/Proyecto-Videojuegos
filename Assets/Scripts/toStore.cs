using UnityEngine;
using System.Collections;

public class toStore : MonoBehaviour {

	public void ChangeScene(string scene){
		Application.LoadLevel (scene);
	}
}
