using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZonesController : MonoBehaviour {


    public bool alertEnemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player" && !alertEnemy)
        {
            Debug.Log("playerEnterCollider");

            if (LevelManager.instance.isBlinking ==false)
            {
                alertEnemy = true;
               // Debug.Log("alertEnemy");
            }

        }

    }
}
