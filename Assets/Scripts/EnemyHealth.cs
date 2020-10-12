using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 2;

    public void takeDamage(int i) {
        health -= i;
        if (health<=0) {
            Destroy(this.gameObject.GetComponent<CharacterController>());
            this.gameObject.AddComponent<Rigidbody>();
            this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
            Destroy(this.gameObject.GetComponent<EnemyAI>());
            Destroy(this.gameObject.GetComponent<EnemyHealth>());
        }
    }
}
