using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_PlayerChaser : MonoBehaviour
{
    public int hp;
    public Text hpText = GameObject.Find("EnemyHP").GetComponent<Text>();
    public Text winText = GameObject.Find("WinConditionText").GetComponent<Text>();
    public Text helpText = GameObject.Find("HelpfulText").GetComponent<Text>();
    public GameObject player = GameObject.Find("Player");
    public Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
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
    public static bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        //hp = hp;
        hpText.text = "Enemy HP: " + hp.ToString();
        enemyRB = GetComponent<Rigidbody>();
        playerRB = player.GetComponent<Rigidbody>();

        hpText = GameObject.Find("EnemyHP").GetComponent<Text>();
        winText = GameObject.Find("WinConditionText").GetComponent<Text>();
        helpText = GameObject.Find("HelpfulText").GetComponent<Text>();
        player = GameObject.Find("Player");
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        helpText.text = "Crash into the Cylinder to reduce its HP!";
        StartCoroutine(waiter());
        //helpText.text = "";
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
            helpText.text = "BEAT IT UP!";
            StartCoroutine(waiter());
        }
    }

    public void SetHPText()
    {
        hpText.text = "Enemy HP: " + hp.ToString();
        if (hp == 0)
        {
            winText.text = "Wow you totally owned that red cylinder";
            //Instantiate(this.gameObject);
            isAlive = false;
            Destroy(this.gameObject);
        }
    }

    public void SetHelpText()
    {

    }

    IEnumerator waiter()
    {
        canMove = false;

        if (helpText.text != "BEAT IT UP!")
        {
            yield return new WaitForSecondsRealtime(waitTime);
            helpText.text = "";
            canMove = true;
        }

        else if (helpText.text != "")
        {
            yield return new WaitForSecondsRealtime(4);
            helpText.text = "";
            canMove = true;
        }

        



        yield return new WaitForSecondsRealtime(waitTime);

        canMove = true;
    }
}
