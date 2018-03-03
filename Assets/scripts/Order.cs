using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour {

	public SpriteRenderer[] spriteRenderers;
	public Pizza myPizza;

	public List<int> ints = new List<int>();

	public List<orderItem> items = new List<orderItem>();

	[System.Serializable]
	public class orderItem{
		public string name;
		public int count;
		public Sprite photo;

		public orderItem(string s, int integer){
			name = s;
			count = integer;

			for (int i = 0; i < GameControl.me.ingredidentPhotos.Length; i ++){
				if (s == GameControl.Ingredients.ElementAt(i).Key){
					photo = GameControl.me.ingredidentPhotos[i];
				}
			}

		}

	}

	bool started = false;



	void SetOrder(){
		foreach (SpriteRenderer s in spriteRenderers){ // resets sprites to null
			s.sprite = null;
		}

		List<string> myItems = myPizza.ingredientsNeeded;
		Debug.Log(myItems.Count);

		foreach (string s in myItems){
			try{
				Debug.Log(s);
				for (int i = 0; i < items.Count; i ++){
					
					Debug.Log(items[i].name);
					
					if (items[i].name == s) {

						items[i].count++;
					}
				}

				items.Add(new orderItem(s, 1));
				
			}
			catch{
				Debug.Log("b");
				items.Add(new orderItem(s, 1));
			}
			Debug.Log("");
		}


		for (int i = 0; i < items.Count; i ++){
			SpriteRenderer s = spriteRenderers[i];
			s.sprite = items[i].photo;
			s.gameObject.GetComponent<Text>().text = items[i].count.ToString();

		}
	}


	void DebugMe(){
		foreach (orderItem o in items){
			Debug.Log(o.count);
			
		}
	}

	void Update(){
		if (!started){
		SetOrder();
		DebugMe();
		started = true;
		}

	}


}
