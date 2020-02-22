using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeDriver : MonoBehaviour
{
    public GameObject enemy;
    public Vector3 enemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate<GameObject>(enemy);
        enemy.transform.position = enemySpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemy_PlayerChaser.isAlive == false)
        {
            enemy = Instantiate<GameObject>(enemy) as GameObject;
            enemy.transform.position = enemySpawn;
            Enemy_PlayerChaser.isAlive = true;
        }
    }
}
