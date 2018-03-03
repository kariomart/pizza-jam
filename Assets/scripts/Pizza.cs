using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {


	public List<string> ingredientsNeeded; 
	public List<bool> hasIngredient;
	public List<string> currentIngredients = new List<string>();

	public float value;

	public float timer = 0;


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
		float timer = 0;
		while (timer < GameControl.timeToCook){
			timer += Time.fixedUnscaledDeltaTime;
			yield return new WaitForEndOfFrame();
		}

		Reset();
	}


	void Reset(){
		ingredientsNeeded = new List<string>();
		hasIngredient = new List<bool>();
		GameControl.me.money += value;
	}

}