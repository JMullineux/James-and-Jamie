using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    Animator animator;

    public float range = 100f;
    public float equipSpeed = 1f;
    public float rotateSpeed = 50f;

    

    public badguyAnimationStateController HitEnemy;

    bool isEquipped = false;
    bool canFire = true;

    public Camera playerCamera;
    public Transform equipTransform;
    public Transform lookAtTransform;
    public GameObject player;
    public ParticleSystem muzzleFlash;

    public GameObject defaultImpactEffect;
    public GameObject npcImpactEffect;

    private AudioSource shootSound;

    public LayerMask npcLayer;
    public LayerMask ragdollLayer;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.transform.name);

        animator = GetComponent<Animator>();

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

            else if (isEquipped && canFire)
            {
                Invoke("ShootFire", 0.1f); // Small delay prevents spam-clicking breaking the reload animation
            }
            
        }

        //transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        //float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.x, 180.0f, 45.0f * Time.deltaTime);
        //transform.eulerAngles = new Vector3(angle, 0, 0);

        //var target1 = Vector3(0f,180f,0f);

        //Vector3.RotateTowards(transform.rotation.x, 0, 180, 0, 45f, 180f);



        //Debug.Log(canFire);

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

            //var badguyController = hit.transform.gameObject.GetComponent<badguyAnimationStateController>();
            //var newBadguyController = hit.

            var hitRoot = hit.transform.root;
            var hitRigidbody = hit.transform.gameObject.GetComponent<Rigidbody>();
            var badguyController = hitRoot.GetComponent<badguyAnimationStateController>();

            // If a ragdoll is hit, use blood particle
            if (hit.transform.gameObject.layer == ragdollLayer)
            {
                GameObject npcImpactGO = Instantiate(npcImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(npcImpactGO, 2f);
                var objectHit = hit.transform.gameObject;
            }

            // Else use dust particle
            else
            {
                GameObject defaultImpactGO = Instantiate(defaultImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(defaultImpactGO, 2f);
                //Debug.Log("The Raycast hit something else!");
            }

            if (badguyController != null && hitRigidbody != null)
            {
                badguyController.ActivateRagdoll(hitRigidbody);
            }


            //Debug.Log("The Root of the Transform hit was: " + hit.transform.root.name);
            //Debug.Log("GameObject hit was " + hit.transform.gameObject.name);
            //Debug.Log("The Collider that was hit is: " + hitCollider.name);
            //Debug.Log("The Root Parent of the Collider is: " + hitCollider.transform.root);
        }

        StartReload();
    }

    void StartReload()
    {
        animator.SetTrigger("reload");
        canFire = false;
    }

    void StopReload()
    {
        animator.SetTrigger("reload");
        canFire = true;
    }

    void EquipWeapon()
    {
        this.transform.parent = playerCamera.transform;
        this.transform.localPosition = equipTransform.localPosition;
        this.transform.localRotation = equipTransform.localRotation;
    }
}
