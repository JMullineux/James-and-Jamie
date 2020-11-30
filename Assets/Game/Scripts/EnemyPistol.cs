using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPistol : MonoBehaviour
{
    public GameObject equipSlot; // The empty game object for the equipped weapon

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip()
    {
        this.transform.parent = equipSlot.transform;
        this.transform.position = equipSlot.transform.position;
        this.transform.rotation = equipSlot.transform.rotation;
    }

    void Fire()
    {
        
    }
}
