using UnityEngine;
using System.Collections;

// State machine demo code from class.
// - different behavior states for a game logic object 
// - transitioning between behavior states
//
// Also check out the alternative implementation commented underneath.
// It is simpler because it gets rid of the state-dependent variable,
// but a lot of the time that's not possible.

public class GuardMovement : MonoBehaviour {
	public Transform waypoint1;
	public Transform waypoint2;

	public float speed;
	
	// This guard movement AI is a simple state machine. It is always
	// in one of a few specific logical states. Its behavior can vary
	// depending on the state it's in. Part of that behavior are the
	// conditions for when the guard will transition to a different state.
	// 
	// Note how the internal helper variable "movementTarget" needs to have
	// a different value in the two states, so correct initial setup (in Start)
	// and transitioning between states require setting that variable
	// to what it should be in the new state.
	public enum GuardState { ToWaypoint1, ToWaypoint2 }; 
	public GuardState currentState;
	
	Vector3 movementTarget;

	void Start () {
		// I wrote Start poorly in class, like so:
//		currentState = GuardState.ToWaypoint1;
//		movementTarget = waypoint1.position;

		// That code works, but there's no reason to force the Guard
		// to always head towards Waypoint 1 first.
		// Instead, let's just see what initial state is set, and set movementTarget
		// accordingly. With this, you can easily flip the waypoint order from
		// the Editor by adjusting an individual Guard's starting state.
		// Waypoint 1 is still default because it comes first in the enum.
		if (currentState == GuardState.ToWaypoint1) {
			movementTarget = waypoint1.position;
		} else { // currentState == GuardState.ToWaypoint2
			movementTarget = waypoint2.position;
		}
	}
	
	void Update () {
		// Possible state transitions: if the guard reaches its current
		// waypoint in either state, it transitions to the other state.

		if (currentState == GuardState.ToWaypoint1) {
			if (Vector3.Distance(transform.position, movementTarget) < Mathf.Epsilon) {
				currentState = GuardState.ToWaypoint2;
				movementTarget = waypoint2.position;
			}
		} else { // currentState == GuardState.ToWaypoint2
			if (Vector3.Distance(transform.position, movementTarget) < Mathf.Epsilon) {
				currentState = GuardState.ToWaypoint1;
				movementTarget = waypoint1.position;
			}
		}

		// Move.
		// This code is used by both states. If we didn't use the helper variable,
		// it would be slightly different depending on state, and would have to be
		// duplicated inside if-else branches.
		transform.position = Vector3.MoveTowards(transform.position,
		                                         movementTarget,
		                                         Time.deltaTime * speed);
	
	}
}

// Same thing written with no shared code between the two states.
// The idea of a state machine may be clearer from this version.
/*
public class GuardMovement : MonoBehaviour {
	public Transform waypoint1;
	public Transform waypoint2;
	
	public float speed;
	
	public enum GuardState { ToWaypoint1, ToWaypoint2 }; 
	public GuardState currentState;

	// notice we don't need Start() anymore since there is no variable
	// that has to be set differently depending on state; you can even flip
	// currentState from the editor while the game is on, and the character
	// will just react correctly

	void Update () {
		if (currentState == GuardState.ToWaypoint1) {
			// Move
			transform.position = Vector3.MoveTowards(transform.position,
			                                         waypoint1.position,
			                                         Time.deltaTime * speed);
			// If waypoint reached, transition to other state
			if (Vector3.Distance(transform.position, waypoint1.position) < Mathf.Epsilon) {
				currentState = GuardState.ToWaypoint2;
			}
			
		} else { // currentState == GuardState.ToWaypoint2
			// Move
			transform.position = Vector3.MoveTowards(transform.position,
			                                         waypoint2.position,
			                                         Time.deltaTime * speed);
			// If waypoint reached, transition to other state  
			if (Vector3.Distance(transform.position, waypoint2.position) < Mathf.Epsilon) {
				currentState = GuardState.ToWaypoint1;
			}
		}
	}
}
*/

