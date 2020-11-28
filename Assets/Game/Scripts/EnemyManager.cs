using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject[] enemies;

    bool killedAllEnemies = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        Debug.Log("Enemies: " + enemies.Length);
        
        if(enemies.Length <= 0)
        {
            killedAllEnemies = true;
        }

        if(killedAllEnemies)
        {
            Win();
        }


    }

    void Win()
    {
        Debug.Log("You Win");
    }
}
