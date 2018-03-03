using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {


	public List<string> ingredientsNeeded; 
	public List<bool> hasIngredient;
	public List<string> currentIngredients = new List<string>();

	public float value;

	public float timer = 0;

	SpriteRenderer mySpr;
	BoxCollider2D myCol;

	bool isCooking = false;


	void Awake(){
		mySpr = GetComponent<SpriteRenderer>();
		myCol = GetComponent<BoxCollider2D>();
	}


	void OnMouseDown(){
		if (GameControl.me.activeIngControl != null){
			Add(GameControl.me.activeIngredient);

		}
	}


	public void Add(string ingredientName){
		if (CanAddIngredent(ingredientName) != -100){
			currentIngredients.Add(ingredientName);
			GameControl.me.activeIngControl.stock--;
			GameControl.me.money -= GameControl.me.activeIngControl.myPrice;
			Debug.Log("added");
			GameControl.me.Deselect();


			if (AllIngrdientsAdded()){
				Debug.Log("started");
				StartCoroutine(Cook());
			}

		}
	}


	bool AllIngrdientsAdded(){
		foreach (bool b in hasIngredient){
			if (!b){
				return false;
			}
		}
		return true;
	}


	int CanAddIngredent(string s){
		for (int i = 0; i < ingredientsNeeded.Count; i ++){
			if (ingredientsNeeded[i] == s && hasIngredient[i] == false){
				hasIngredient[i] = true;
				return i;
			}
		}
		return -100;
	}


	IEnumerator Cook(){
		isCooking = true;
		mySpr.enabled = false;
		myCol.enabled = false;

		while (timer < GameControl.timeToCook){
			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		mySpr.enabled = true;
		myCol.enabled = true;
		isCooking = false;
		Reset();
	}


	void Reset(){
		ingredientsNeeded = new List<string>();
		hasIngredient = new List<bool>();
		currentIngredients = new List<string>();
		GameControl.me.money += value;
		GameControl.ordersCompleted ++;
		GameControl.currentNumOrders--;
		timer = 0;
	}


	void Update(){

		if (!isCooking){
			mySpr.enabled = (ingredientsNeeded.Count != 0);
			myCol.enabled = (ingredientsNeeded.Count != 0);

		}
	}
}