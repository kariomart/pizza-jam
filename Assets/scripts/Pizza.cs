using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {


    public List<string> ingredientsNeeded; 
    public List<bool> hasIngredient;
    public List<string> currentIngredients = new List<string>();

    public Sprite sauced;
    public Sprite cheesed;

    public float value = 8;

    public float timer = 0;

    SpriteRenderer mySpr;
    BoxCollider2D myCol;

    bool isCooking = false;
    float toppingOffset = .2f;

	public Order myOrder;


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
            
            if (GameControl.me.activeIngredient == "sauce") {
                mySpr.sprite = sauced;
            }

            if(GameControl.me.activeIngredient == "cheese") {
                mySpr.sprite = cheesed;
            }

            else {
                AddToppingImage();
            }

            currentIngredients.Add(ingredientName);
            GameControl.me.activeIngControl.stock--;
            GameControl.me.money -= GameControl.me.activeIngControl.myPrice;
            GameControl.me.Deselect();


            if (AllIngrdientsAdded()){
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

    void AddToppingImage() {

        for(int i = 0; i < Random.Range(1, 5); i++) {
            GameObject tempImage = new GameObject(GameControl.me.activeImage.name);
            SpriteRenderer tempSprite = tempImage.AddComponent<SpriteRenderer>();
            tempSprite.sprite = GameControl.me.activeImage;
            tempImage.transform.parent = this.transform;
            tempImage.transform.localScale = new Vector3(.85f, .85f, .85f);
            tempImage.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            tempImage.transform.localPosition = new Vector3(Random.Range(-toppingOffset, toppingOffset), Random.Range(-toppingOffset, toppingOffset), -.1f);
        }
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

        foreach (Transform child in this.transform) {
                GameObject.Destroy(child.gameObject);
        }

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
