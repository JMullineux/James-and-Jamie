﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float range = 100f;
    public float equipSpeed = 1f;
    public float rotateSpeed = 50f;

    

    bool isEquipped = false;
    bool isEquipMoving = false;
    bool isEquipRotating = false;

    //bool isEquipping = false;

    public Camera playerCamera;
    public Transform equipTransform;
    public Transform lookAtTransform;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.transform.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isEquipped)
            {
                EquipFire();
            }

            else if (isEquipped)
            {
                ShootFire();
            }
            
        }

        if(isEquipMoving)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, equipTransform.position, equipSpeed * Time.deltaTime);

            /*
            if(this.transform.position == equipTransform.position)
            {
                isEquipMoving = false;
            }
            */
        }

        if(isEquipped)
        {
            //this.transform.Rotate(Vector3.left, rotateSpeed * Time.deltaTime);
            this.transform.LookAt(equipTransform);

            //if(this.transform.rotation == equipTransform.rotation)
            //{
                //isEquipRotating = false;
            //}
        }
    }

    void EquipFire()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range)) //If there is a hit result
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.name == this.transform.name) // check if the hit result is the pistol
            {
                isEquipped = true; // logically equip weapon
                isEquipMoving = true;
                isEquipRotating = true;


                //isEquipping = true;

                //Physically animate equipping the weapon
                EquipAnimation();


            }
        }
    }

    void ShootFire()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("FIRE");

        }
    }

    void EquipAnimation()
    {
        
    }
}