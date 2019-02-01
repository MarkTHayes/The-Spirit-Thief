using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    private Rigidbody2D arrowRB;

	// Use this for initialization
	void Start () {

        arrowRB = GetComponent<Rigidbody2D>();
        Vector2 v = LevelManager.instance.rangerAttackPoint.transform.position - LevelManager.instance.rangerRotatePoint.localPosition;
        arrowRB.AddForce(LevelManager.instance.rangerAttackPoint.transform.right * LevelManager.instance.arrowForce, ForceMode2D.Impulse);
        Debug.DrawLine(LevelManager.instance.rangerRotatePoint.position, v, Color.red,10f);



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
