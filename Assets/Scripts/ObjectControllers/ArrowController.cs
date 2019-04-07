using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    private Rigidbody2D arrowRB;
    public GameObject centerOfMass;
    private int playerFacing;

	// Use this for initialization
	void Start () {

        playerFacing = LevelManager.instance.moveRight;
        arrowRB = GetComponent<Rigidbody2D>();
        Vector2 v = LevelManager.instance.rangerAttackPoint.transform.position - LevelManager.instance.rangerRotatePoint.localPosition;
        arrowRB.AddForce(LevelManager.instance.rangerAttackPoint.transform.right * LevelManager.instance.arrowForce * LevelManager.instance.moveRight, ForceMode2D.Impulse);
        Debug.DrawLine(LevelManager.instance.rangerRotatePoint.position, v, Color.red,10f);

        transform.localScale = transform.localScale * LevelManager.instance.moveRight;

    }
	
	// Update is called once per frame
	void Update () {

        transform.rotation = Quaternion.Euler(0, 0, arrowRB.velocity.y * 5 * playerFacing);





    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
