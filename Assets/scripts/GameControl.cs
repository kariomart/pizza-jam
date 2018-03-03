using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public Sprite[] ingredidentPhotos;

	public IngredientControl[] myIngredientControls;

	public static Dictionary<string, float> Ingredients = new Dictionary<string, float> (){
		{"sauce", 1},
		{"cheese", 1},
		{"pepperoni", 1},
		{"pineapple", 1},
		{"bacon", 1},
		{"bbqchick", 1},
		{"bufchick", 1},
		{"mushrooms", 1},
		{"sausage", 1},
		{"spinach", 1},
		{"onions", 1},
		{"olives", 1},
	};

	public static GameControl me;
	List<order> currentOrders = new List<order>();

	int timePerDay;
	public float time = 0;

	public float money = 10;
	public Text moneyText;

	int maxIngredients = 8;

	public int ordersCompleted = 0;
	public int maxOrdersAtOnce = 3;

	public Pizza[] pizzas;

	public bool itemSelected = false;
	public GameObject activeObj;
	public IngredientControl activeIngControl;
	SpriteRenderer activeObjSpriteRenderer;
	public Sprite activeImage;
	public string activeIngredient;


	int timeToCook = 30;


	public class order {
		public string[] ingredients;
		public float price;

		public order(){
			FindPrice();

		}

		public void FindPrice(){
			foreach  (string s in ingredients){
				price += Ingredients[s];
			}
		}
	}


	void SetIngredents(){
		int numIngredients = Ingredients.Count;
		string[] ingredientNames = new string[numIngredients];

		int count = 0;

		foreach (KeyValuePair<string, float> entry in Ingredients){
			ingredientNames[count] = entry.Key;
			count++;
		}
		

		for (int i = 0; i < numIngredients; i ++){
			myIngredientControls[i].myPhoto.sprite = ingredidentPhotos[i];
			myIngredientControls[i].name = ingredientNames[i];
			myIngredientControls[i].myPrice = Ingredients[myIngredientControls[i].name];

		}
	}


	Pizza FindEmptyPizza(){
		foreach (Pizza p in pizzas){
			if (p.ingredientsNeeded.Count == 0){
				return p;
			}
		}

		return null;
		
	}


	void MakeOrder(){
		Pizza currentZa = FindEmptyPizza();

		if (currentZa != null){
			List<string> newOrder = new List<string>(){
				"sauce",
				"cheese"
			};

			int numberOfIngredients = (int) Random.Range(.5f, maxIngredients + .5f);

			for (int i = 0; i < numberOfIngredients; i ++){
				string newIngredient;
				currentZa.hasIngredient.Add(false);

				do {
					newIngredient = Ingredients.ElementAt((int) Random.Range(0, Ingredients.Count)).Key;		
				}
				while(
					newIngredient == "sauce" || newIngredient == "cheese"
				);

				newOrder.Add(newIngredient);
			}

			
			currentZa.ingredientsNeeded = newOrder;
		}


	}


	

	public void Deselect(){
		itemSelected = false;
		activeObjSpriteRenderer.sprite = null;
		activeIngredient = null;
		activeIngControl = null;		
	}




	void Awake () {
		if (me == null){
			me = this;
			activeObjSpriteRenderer = activeObj.GetComponent<SpriteRenderer>();
			GameObject[] tempPizzas = GameObject.FindGameObjectsWithTag("za");
			Debug.Log(tempPizzas.Length);
			Debug.Log(pizzas.Length);
			for (int i = 0; i < tempPizzas.Length; i ++){
				pizzas[i] = tempPizzas[i].GetComponent<Pizza>();
			}
		}
		else{
			Destroy(this.gameObject);
		}
	}




	void Start(){
		SetIngredents();
		MakeOrder();
	}




	void Update () {
		time = Time.timeSinceLevelLoad;
		moneyText.text = "$$$: " + money.ToString();
	


		if (itemSelected) {
			activeObjSpriteRenderer.sprite = activeImage;
			activeObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
			activeObj.transform.position = new Vector3(activeObj.transform.position.x, activeObj.transform.position.y, 0f);
		}
		else {
			Deselect();
		}
	}

}