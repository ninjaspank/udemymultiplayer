using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DealDamageOnContact : MonoBehaviour
{
    [SerializeField] private int damage = 15;

    private ulong ownerClientID;

    public void SetOwner(ulong ownerClientID)
    {
        this.ownerClientID = ownerClientID;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.attachedRigidbody == null) {return;}

        if (col.attachedRigidbody.TryGetComponent<NetworkObject>(out NetworkObject netObj))
        {
            if (netObj.OwnerClientId == ownerClientID)
            {
                return;
            }
        }

        if (col.attachedRigidbody.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }
    }
}
