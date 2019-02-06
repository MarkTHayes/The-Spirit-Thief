using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRearHitbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "T_AttackBox")
        {

            transform.parent.GetComponent<MediumMeleeController>().health -= LevelManager.instance.assassinateDamageValue; 

        }


    }

}
