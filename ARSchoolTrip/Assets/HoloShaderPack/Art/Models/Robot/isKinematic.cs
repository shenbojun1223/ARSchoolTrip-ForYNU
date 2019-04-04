using UnityEngine;
using System.Collections;

public class isKinematic : MonoBehaviour {
	public Component[] rigidbodies;
	// Use this for initialization
	void Start () {


		rigidbodies = GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody Kinematic in rigidbodies) {
			Kinematic.isKinematic = true;
			}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
