using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badguyAnimationStateController : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("EquipWeapon", 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EquipWeapon()
    {
        animator.SetBool("isEquipping", true);
    }

}
