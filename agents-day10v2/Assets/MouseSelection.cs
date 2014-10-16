using UnityEngine;
using System.Collections;

public class MouseSelection : MonoBehaviour {
	Color initialColor;
	GameManager gm;
	bool isSelected;
	Renderer meshRenderer;

	public void Unselect() {
		// small potential bug here: what if mouse is on top of the object
		// while the object is unselected?
		// (that can't actually happen at the moment because objects are
		// only unselected when the user clicks on another object)
		meshRenderer.material.color = initialColor;
		isSelected = false;
	}

	void Start () {
		// Note the difference between the two Find functions we're using.
		// GameObject.Find searches from the whole scene by name.
		// transform.Find searches for a child of this object by name.
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		meshRenderer = transform.Find("MeshAndCollider").renderer;
		initialColor = meshRenderer.material.color;
	}
	
	void OnMouseEnter() {
		if (!isSelected) { // the '!' operator negates a truth value
			meshRenderer.material.color = Color.red;
		}
	}

	void OnMouseExit() {
		if (isSelected == false) { // this is the same as !isSelected
			meshRenderer.material.color = initialColor;
		}
	}

	void OnMouseDown() {
		if (!isSelected) {
			meshRenderer.material.color = Color.blue;
			isSelected = true;
			// We tell the GameManager that the user selected this
			// object, so the GameManager knows which character to
			// move, and can also call Unselect() on this script later
			// to restore the normal color when the user selects something
			// else.
			//
			// 'this' is a reference to the current script.
			gm.NewSelection(this);
		}
	}

}
