using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	MouseSelection selected;

	public void NewSelection(MouseSelection m) {
		// deselect last selection (if any)
		if (selected != null) {
			selected.Unselect();
		}
		selected = m;
	}

	void Update () {
		// If an object is selected and the user clicks with the right mouse
		// button over the ground, move the object to that point.
		// We use a raycast to find the point the user clicked.

		if (selected) {
			// left button = 0, right button = 1, middle = 2, ...
			if (Input.GetMouseButtonDown(1)) {
				// Rays are abstract geometric objects that are defined by
				// a starting position and a direction; think about a
				// laser pointer's beam.
	
				// We use a Unity helper function to obtain a ray that starts from
				// the surface of the virtual camera, under the mouse cursor, and
				// has a direction directly away from the viewer.
				//
				// In other words, this ray will hit whatever object in the game
				// world the user sees under the mouse cursor.
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				// We'll build a layer mask so that the ray will pass through anything else
				// and can only hit objects in the Ground layer.

				// You can look up a layer index number from Project Settings -> Layers.
				int groundLayerIndex = 8;

				// This is how you build a layer mask for raycasting.
				// For the purposes of this course, there's no need to understand why
				// the calculation is done like that, or what << means.
				// Just google it or look in the Unity online manual under "Layers".
				int mask = 1 << groundLayerIndex;

				// This is how you would allow the ray to hit either one of two layers.
				// int mask = (1 << groundLayerId) | (1 << someOtherLayerID);

				// And this would hit any layer _except_ the one whose ID we're giving.
				// int mask = ~(1 << someLayerID);

				// Physics.Raycast returns a truth value that shows whether the ray
				// hit anything or not.
				//
				// If you give Raycast a RaycastHit object as an argument, it
				// will give you additional information.
				// Through the RaycastHit object you can access the exact position
				// where the hit occurred, but also the collider (and thus,
				// GameObject or some script) of the object which the ray hit.
				//
				// When we give a layer mask argument, we must always first give
				// a maximum ray length argument - that's a limitation of how
				// C# functions with optional arguments work.
				// Otherwise we would not need to specify a length in this case.
				// Raycast uses infinitely long rays as the default.
				RaycastHit hit;
				bool isHit = Physics.Raycast(ray, out hit, Mathf.Infinity, mask);

				// We have to check the ray actually hit something before trying
				// to read any data about the hit from the RaycastHit object. 
				if (isHit) {
					selected.transform.position = hit.point;
				}
			}
		}
	}
}
