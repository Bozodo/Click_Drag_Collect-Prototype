﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_PlayerChaser : MonoBehaviour
{
    public int hp;
    public Text hpText;
    public Text winText;
    public GameObject player;
    public Transform playerTransform;
    public int MoveSpeed = 1; // you can adjust speed based on different level
    public int trackingDistance = 0;
    public int lungeDistance = 3;
    public int lungeMultiplier = 3;
    public int waitTime = 3;
    private Rigidbody enemyRB;
    private Rigidbody playerRB;

    [Header("Dynamic Stuff")]
    public Vector3 enemyPos;
    public float distanceFromPlayer;
    public float enemyVelocity;
    public float playerVelocity;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        hp = 5;
        enemyRB = GetComponent<Rigidbody>();
        playerRB = player.GetComponent<Rigidbody>();

        StartCoroutine(waiter());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyPos = transform.position;
        //technically not correct but I need floats
        enemyVelocity = enemyRB.velocity.magnitude;
        playerVelocity = playerRB.velocity.magnitude;
        distanceFromPlayer = Vector3.Distance(enemyPos, playerTransform.position);
        

        //transform.LookAt(playerTransform);

        if(canMove == true)
        {
            enemyRB.AddForce((playerTransform.position - transform.position) * MoveSpeed);

            if (distanceFromPlayer <= lungeDistance)
            {
                enemyRB.AddForce((playerTransform.position - transform.position) * MoveSpeed * lungeMultiplier);
            }
        }
        

        

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Player")
        {
            //yield return new WaitForSecondsRealtime(5);
            //enemyRB.Sleep();
            if(playerVelocity > enemyVelocity)
            {
                hp -= 1;
                SetHPText();
            }
            StartCoroutine(waiter());
        }
    }

    public void SetHPText()
    {
        hpText.text = "Enemy HP: " + hp.ToString();
        if (hp == 0)
        {
            winText.text = "Wow you totally owned that red cylinder";
            Destroy(this.gameObject);
        }
    }

    IEnumerator waiter()
    {
        canMove = false;

        yield return new WaitForSecondsRealtime(waitTime);

        canMove = true;
    }
}
