using UnityEngine;
using System.Collections;

public class GuardTowerLightTrigger : MonoBehaviour {
	// Triggers once more!
	//
	// To eliminate useless trigger events, you can divide your objects to
	// suitable layers and set the layers' allowed interactions under
	// Project Settings -> Tags and Layers + Physics. You can't have more than
	// ~30 layers. For more fine-grained control, you can have your trigger
	// event code test specific properties of the other object, such as if it has
	// a specific script attached.
	//
	// Unity gives you the other object's Collider as a function argument when
	// it calls the OnTriggerXYZ() functions. All other data about the object
	// can be reached through the Collider - see Collider's documentation.
	//
	// In the functions below we have named the argument as "c", so
	// the other object's GameObject would be c.gameObject and its rigidbody
	// would be c.rigidbody.

	void OnTriggerEnter(Collider c) {
		Debug.Log("Something entered the light!");
	}

	void OnTriggerExit(Collider c) {
		Debug.Log("Something exited the light!");
	}
	
//	void OnTriggerStay(Collider c) {
//		Debug.Log("Something is in the light now (between last and next FixedUpdate)!");
//	}
}
