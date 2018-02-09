using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    public float damage;

	// Use this for initialization
	void Start () {

        Destroy(gameObject, 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
