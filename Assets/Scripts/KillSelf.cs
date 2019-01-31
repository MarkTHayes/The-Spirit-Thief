using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelf : MonoBehaviour {


    public float deathTimer;
    public string destroyTag;

	// Use this for initialization
	void Start () {

        deathTimer = Time.time + deathTimer;
        		
	}

    private void Update()
    {
        
        if (deathTimer < Time.time)
        {
            Destroy(gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == destroyTag)
        {

            Destroy(gameObject);

        }
    }


}
