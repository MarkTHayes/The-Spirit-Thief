using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIManager : MonoBehaviour
{

    public GameObject HP0;
    public GameObject HP1;
    public GameObject HP2;
    public GameObject HP3;
    public GameObject HP4;
    public GameObject HP5;
    public GameObject HP6;
    public GameObject HP7;
    public GameObject HP8;
    public GameObject HP9;
    public GameObject HP10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        playerHealthController();

    }

    //Player health
    public void playerHealthController()
    {
        if (LevelManager.instance.playerHealth >= 10)
        {
            LevelManager.instance.playerHealth = 10;
        }

        if (LevelManager.instance.playerHealth <= 0)
        {
            LevelManager.instance.playerHealth = 0;
        }

        if (LevelManager.instance.playerHealth == 0)
        {
            HP0.SetActive(true);
            HP1.SetActive(false);
            HP2.SetActive(false);
            HP3.SetActive(false);
            HP4.SetActive(false);
            HP5.SetActive(false);
            HP6.SetActive(false);
            HP7.SetActive(false);
            HP8.SetActive(false);
            HP9.SetActive(false);
            HP10.SetActive(false);
        }
        if (LevelManager.instance.playerHealth == 1)
        {
            HP0.SetActive(false);
            HP1.SetActive(true);
            HP2.SetActive(false);
            HP3.SetActive(false);
            HP4.SetActive(false);
            HP5.SetActive(false);
            HP6.SetActive(false);
            HP7.SetActive(false);
            HP8.SetActive(false);
            HP9.SetActive(false);
            HP10.SetActive(false);
        }
        if (LevelManager.instance.playerHealth == 2)
        {
            HP0.SetActive(false);
            HP1.SetActive(false);
            HP2.SetActive(true);
            HP3.SetActive(false);
            HP4.SetActive(false);
            HP5.SetActive(false);
            HP6.SetActive(false);
            HP7.SetActive(false);
            HP8.SetActive(false);
            HP9.SetActive(false);
            HP10.SetActive(false);
        }
        if (LevelManager.instance.playerHealth == 3)
        {
            HP0.SetActive(false);
            HP1.SetActive(false);
            HP2.SetActive(false);
            HP3.SetActive(true);
            HP4.SetActive(false);
            HP5.SetActive(false);
            HP6.SetActive(false);
            HP7.SetActive(false);
            HP8.SetActive(false);
            HP9.SetActive(false);
            HP10.SetActive(false);
        }
        if (LevelManager.instance.playerHealth == 4)
        {
            HP0.SetActive(false);
            HP1.SetActive(false);
            HP2.SetActive(false);
            HP3.SetActive(false);
            HP4.SetActive(true);
            HP5.SetActive(false);
            HP6.SetActive(false);
            HP7.SetActive(false);
            HP8.SetActive(false);
            HP9.SetActive(false);
            HP10.SetActive(false);
        }
        if (LevelManager.instance.playerHealth == 5)
        {
            HP0.SetActive(false);
            HP1.SetActive(false);
            HP2.SetActive(false);
            HP3.SetActive(false);
            HP4.SetActive(false);
            HP5.SetActive(true);
            HP6.SetActive(false);
            HP7.SetActive(false);
            HP8.SetActive(false);
            HP9.SetActive(false);
            HP10.SetActive(false);
        }
        if (LevelManager.instance.playerHealth == 6)
        {
            HP0.SetActive(false);
            HP1.SetActive(false);
            HP2.SetActive(false);
            HP3.SetActive(false);
            HP4.SetActive(false);
            HP5.SetActive(false);
            HP6.SetActive(true);
            HP7.SetActive(false);
            HP8.SetActive(false);
            HP9.SetActive(false);
            HP10.SetActive(false);
        }
        if (LevelManager.instance.playerHealth == 7)
        {
            HP0.SetActive(false);
            HP1.SetActive(false);
            HP2.SetActive(false);
            HP3.SetActive(false);
            HP4.SetActive(false);
            HP5.SetActive(false);
            HP6.SetActive(false);
            HP7.SetActive(true);
            HP8.SetActive(false);
            HP9.SetActive(false);
            HP10.SetActive(false);
        }
        if (LevelManager.instance.playerHealth == 8)
        {
            HP0.SetActive(false);
            HP1.SetActive(false);
            HP2.SetActive(false);
            HP3.SetActive(false);
            HP4.SetActive(false);
            HP5.SetActive(false);
            HP6.SetActive(false);
            HP7.SetActive(false);
            HP8.SetActive(true);
            HP9.SetActive(false);
            HP10.SetActive(false);
        }
        if (LevelManager.instance.playerHealth == 9)
        {
            HP0.SetActive(false);
            HP1.SetActive(false);
            HP2.SetActive(false);
            HP3.SetActive(false);
            HP4.SetActive(false);
            HP5.SetActive(false);
            HP6.SetActive(false);
            HP7.SetActive(false);
            HP8.SetActive(false);
            HP9.SetActive(true);
            HP10.SetActive(false);
        }
        if (LevelManager.instance.playerHealth == 10)
        {
            HP0.SetActive(false);
            HP1.SetActive(false);
            HP2.SetActive(false);
            HP3.SetActive(false);
            HP4.SetActive(false);
            HP5.SetActive(false);
            HP6.SetActive(false);
            HP7.SetActive(false);
            HP8.SetActive(false);
            HP9.SetActive(false);
            HP10.SetActive(true);
        }
    }

}




