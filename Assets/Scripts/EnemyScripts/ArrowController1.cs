using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController1 : MonoBehaviour {

    private Rigidbody2D arrowRB;
    public GameObject centerOfMass;
    public float facing;

    public GameObject spawnObject;
    

    public float arrowForce;
	// Use this for initialization
	void Start () {

         
        if (facing > 0)
        {
            facing = 1;
        }
        else if (facing < 0)
        {
            facing = -1;
        }


        arrowRB = GetComponent<Rigidbody2D>();
       
       // arrowRB.AddForce(LevelManager.instance.rangerAttackPoint.transform.right * LevelManager.instance.arrowForce * LevelManager.instance.moveRight, ForceMode2D.Impulse);
        arrowRB.AddForce(spawnObject.transform.right * arrowForce * -facing, ForceMode2D.Impulse);

        transform.localScale = transform.localScale * -facing;

    }
	
	// Update is called once per frame
	void Update () {

        transform.rotation = Quaternion.Euler(0, 0, arrowRB.velocity.y * 5 * facing);





    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.tag == "Enemy")
        //{
        //    Destroy(gameObject);
        //}
    }
}
