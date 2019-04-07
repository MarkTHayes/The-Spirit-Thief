using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrowAtPoint : MonoBehaviour {

    public Transform targetPos;

    public Transform shootPoint;

    public GameObject arrowPrefab;


    public float arrowForce;


    public float distanceToTarget;

    

	// Use this for initialization
	void Start () {


        distanceToTarget = shootPoint.position.x - targetPos.position.x;

        

        StartCoroutine("shootArrow");
		
	}
	
	// Update is called once per frame
	void Update () {

        arrowForce = 3.5f;

        arrowForce *= distanceToTarget / 2 * Random.Range(0.8f, 1.2f);

        Vector3 difference = transform.position - targetPos.position;

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 20;

       // rotationZ = Mathf.Clamp(rotationZ, -80, 80);

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

    }


    IEnumerator shootArrow()
    {

        for (int i = 1; i > 0; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, shootPoint.transform.position, Quaternion.identity);
            arrow.GetComponent<ArrowController1>().spawnObject = shootPoint.gameObject;
            arrow.GetComponent<ArrowController1>().facing = transform.parent.transform.localScale.x;
            arrow.GetComponent<ArrowController1>().arrowForce = arrowForce;

            yield return new WaitForSeconds(2);
            

        }


    }

}
