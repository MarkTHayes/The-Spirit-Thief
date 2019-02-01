using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    private Rigidbody2D arrowRB;

	// Use this for initialization
	void Start () {

        arrowRB = GetComponent<Rigidbody2D>();
        Vector2 v = new Vector2(LevelManager.instance.arrowForce, 0);
        arrowRB.AddForce(v, ForceMode2D.Impulse);


		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
