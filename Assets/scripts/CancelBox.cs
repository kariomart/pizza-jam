using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelBox : MonoBehaviour {

	void OnMouseDown(){
		GameControl.me.Deselect();
	}
}
