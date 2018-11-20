using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_LightChange : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Renderer render = GetComponent<Renderer>();
		render.material.SetFloat("_RimPower", 5*Mathf.Cos(Time.time * 5) + 6);
	}
}
