using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrototypeDriver : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector3 enemySpawn;
    public bool enemyAlive = true;
    public bool playerAlive = true;
    public static int count;
    public Text countText;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        countText.text = "Cylinders Destroyed: ";
        Instantiate<GameObject>(enemyPrefab);
        enemyPrefab.transform.position = enemySpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_PlayerChaser.isAlive == false)
        {
            count = count + 1;
            SetCountText();
            Invoke("EnemySpawner", 2f);
            Enemy_PlayerChaser.isAlive = true;
        }

        if(PlayerHealth.isAlive == false)
        {
            Invoke("SceneLoader", 3f);
            PlayerHealth.isAlive = true;
        }
    }

    void SceneLoader()
    {
        SceneManager.LoadScene("_Scene_1");
    }

    void EnemySpawner()
    {
        Instantiate<GameObject>(enemyPrefab);
        //PlayerHealth.enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyPrefab.transform.position = enemySpawn;
    }

    void SetCountText()
    {
        countText.text = "Cylinders Destroyed: " + count.ToString();
    }
}
