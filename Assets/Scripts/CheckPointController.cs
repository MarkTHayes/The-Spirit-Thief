using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {

    private Animator myAnimator;

	// Use this for initialization
	void Start () {

        myAnimator = GetComponent<Animator>();
    
		
	}
	
	// Update is called once per frame
	void Update () {

        if (LevelManager.instance.canInteract && Input.GetKeyDown(KeyCode.F))
        {
            myAnimator.SetTrigger("Interacted");

        }

    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            LevelManager.instance.canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            LevelManager.instance.canInteract = false;
        }
    }
}
