using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject[] enemies;
    public GameObject player;
    PlayerMovement playerMovement;

    public GameObject camera;
    CameraControl cameraControl;

    public GameObject winCard;

    //var playerMovement;

    bool killedAllEnemies = false;
    bool winTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        cameraControl = camera.GetComponent<CameraControl>();

        winCard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        //Debug.Log("Enemies: " + enemies.Length);
        
        if(enemies.Length <= 0)
        {
            killedAllEnemies = true;
        }

        if(killedAllEnemies && !winTrigger)
        {
            Win();
        }


    }

    void Win()
    {
        Debug.Log("You Win");
        playerMovement.Freeze();
        cameraControl.UnlockCursor();
        winCard.SetActive(true);
        winTrigger = true;
    }
}
