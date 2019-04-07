using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRearHitbox : MonoBehaviour
{

    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.parent.GetComponent<MediumMeleeController>().canTakeDamage && LevelManager.instance.tAttack && LevelManager.instance.leftMouse)
        {
            Debug.Log("Attacks");            
            if (transform.parent.GetComponent<MediumMeleeController>().takeDamage)
            {
                Debug.Log("TakesDamageRear");
                if (transform.parent.GetComponent<MediumMeleeController>().canBeAssassinated)
                {
                    transform.parent.GetComponent<MediumMeleeController>().canTakeDamage = false;
                    Debug.Log("10 damage");
                    transform.parent.GetComponent<MediumMeleeController>().health = transform.parent.GetComponent<MediumMeleeController>().health - LevelManager.instance.assassinateDamageValue;
                    transform.parent.GetComponent<MediumMeleeController>().StartCoroutine("DamageCooldown");
                    transform.parent.GetComponent<MediumMeleeController>().StartCoroutine("PushEnemy", "Thief");
                }
            }

        }




    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "T_AttackBox")
        {


            //transform.parent.GetComponent<MediumMeleeController>().health -= LevelManager.instance.assassinateDamageValue; 
            transform.parent.GetComponent<MediumMeleeController>().takeDamage = true;

        }

    }
}
