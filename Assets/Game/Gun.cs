using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float range = 100f;
    public float equipSpeed = 1f;
    public float rotateSpeed = 50f;

    bool isEquipped = false;

    //bool isEquipping = false;

    public Camera playerCamera;
    public Transform equipTransform;
    public Transform lookAtTransform;
    public GameObject player;
    public ParticleSystem muzzleFlash;

    public GameObject defaultImpactEffect;
    public GameObject npcImpactEffect;

    private AudioSource shootSound;

    public LayerMask npcLayer;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.transform.name);

        shootSound = GetComponent<AudioSource>();
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

    }

    void EquipFire()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range)) //If there is a hit result
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.name == this.transform.name) // check if the hit result is the pistol
            {

                //Equip the weapon
                isEquipped = true;
                EquipWeapon();

            }
        }
    }

    void ShootFire()
    {

        // Muzzle Flash and Shot Sound
        muzzleFlash.Play();
        shootSound.Play();

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {

            // If NPC is hit, use blood particle
            if (hit.transform.gameObject.layer == npcLayer)
            {
                GameObject npcImpactGO = Instantiate(npcImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(npcImpactGO, 2f);
            }

            // Else use dust particle
            else
            {
                GameObject defaultImpactGO = Instantiate(defaultImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(defaultImpactGO, 2f);
            }

        }
    }

    void EquipWeapon()
    {
        this.transform.parent = playerCamera.transform;
        this.transform.localPosition = equipTransform.localPosition;
        this.transform.localRotation = equipTransform.localRotation;
    }
}
