using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpSugar : MonoBehaviour
{

    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            Pickup();
        }
    }

    void Pickup()
    {
        Debug.Log("Pickup");
        Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        StaminUp.Instance.PowerStamina(100);
        
    }
}
