﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int hp;
    public Text hpText;
    public Text dedText;
    //public Text countText;
    public GameObject enemy;
    private Rigidbody enemyRB;
    private Rigidbody playerRB;

    public float enemyVelocity;
    public float playerVelocity;

    public static bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        SetHPText();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyRB = enemy.GetComponent<Rigidbody>();
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyRB = enemy.GetComponent<Rigidbody>();
        playerRB = GetComponent<Rigidbody>();

        enemyVelocity = enemyRB.velocity.magnitude;
        playerVelocity = playerRB.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        if(collidedWith.tag == "Enemy")
        {
            if(enemyVelocity > playerVelocity)
            {
                hp -= 1;
                SetHPText();
            }
            
        }
    }

    public void SetHPText()
    {
        hpText.text = "Player HP: " + hp.ToString();
        if (hp == 0)
        {
            dedText.text = "ded";
            isAlive = false;
            Destroy(this.gameObject);
        }
    }


}
