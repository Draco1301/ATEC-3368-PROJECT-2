using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        collision.gameObject.GetComponent<PlayerHealth>()?.changeHealth(-1);
        Debug.Log("Collision",this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<PlayerHealth>()?.changeHealth(-1);
        Debug.Log("Collision", this.gameObject);
    }
}
