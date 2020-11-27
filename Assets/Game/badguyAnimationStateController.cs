using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badguyAnimationStateController : MonoBehaviour
{

    public Animator animator;
    public Rigidbody RIGID_BODY;
    public Rigidbody hips;
    public List<Collider> RagdollParts = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        SetRagdollParts();
        SetKinematic();
        Physics.IgnoreLayerCollision(9, 10, true); //Box Collider and the Ragdoll do no collide with eachother
        Invoke("EquipWeapon", 4);
        //Invoke("TurnOnRagdoll", 8);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EquipWeapon()
    {
        animator.SetBool("isEquipping", true);
    }

    void SetRagdollParts()
    {
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach(Collider c in colliders)
        {
            if (c.gameObject != this.gameObject)
            {
                c.isTrigger = true;
                RagdollParts.Add(c);
            }

            
        }
    }

    void SetKinematic()
    {
        Rigidbody[] bodies = this.gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            if (rb.gameObject != this.gameObject)
            {
                rb.isKinematic = true;
            }
            
            //RIGID_BODY.isKinematic = false;
        }
    }

    public void TurnOnRagdoll(RaycastHit hit)
    {
        RIGID_BODY.useGravity = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        animator.enabled = false;

        foreach(Collider c in RagdollParts)
        {
            c.isTrigger = false;
        }

        Rigidbody[] bodies = this.gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            if (rb.gameObject != this.gameObject)
            {
                rb.isKinematic = false;
            }

            //RIGID_BODY.isKinematic = false;
        }

        var closestDist = 10.0f;
        Rigidbody closestBone = null;

        foreach (Rigidbody rb in bodies)
        {
            if (rb.gameObject != this.gameObject)
            {
                float dist = Vector3.Distance(hit.point, rb.gameObject.transform.position);
                if(dist < closestDist)
                {
                    closestBone = rb;
                    closestDist = dist;
                }    
                //Debug.Log(rb.name + " " + dist);
            }
        }

        Debug.Log("The closest bone is " + closestBone.name);

        closestBone.AddForce(transform.forward * -5000);

    }

    Rigidbody GetClosestRb(Rigidbody[] bodies, RaycastHit hit)
    {
        Rigidbody bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = hit.transform.position;
        foreach (Rigidbody potentialTarget in bodies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr && potentialTarget.gameObject != this.gameObject)
            {
                Debug.Log(potentialTarget.name);
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }



}
