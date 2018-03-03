using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientControl : MonoBehaviour {

	public SpriteRenderer myPhoto;
	public float myPrice;
	public string name;

	public int stock = 10;


	// Use this for initialization
	void Awake () {
		myPhoto = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnMouseDown(){
		Debug.Log("clicked");
		if (stock >= 1 && GameControl.me.money >= myPrice){
			GameControl.me.activeImage = myPhoto.sprite;
			GameControl.me.activeIngredient = name;
			GameControl.me.itemSelected = true;
			GameControl.me.activeIngControl = this;
		}
	}
	

}
