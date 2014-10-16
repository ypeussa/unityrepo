using UnityEngine;
using System.Collections;

public class TurnGuardTowerLight : MonoBehaviour {

	public float rotationSpeedDegrees;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0.0f, rotationSpeedDegrees * Time.deltaTime, 0.0f);
	}
}
