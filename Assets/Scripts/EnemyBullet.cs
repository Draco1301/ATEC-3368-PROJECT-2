﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rb;
    public bool killOnWall = true;
    public float Speed { get => speed; set => speed = value; }

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision) {
        Rigidbody rb2 = collision.gameObject.GetComponent<Rigidbody>();
        if (rb2 != null) {
            Vector3 dir = collision.gameObject.transform.position - collision.GetContact(0).point;
            rb.AddForceAtPosition(dir * 20, collision.GetContact(0).point, ForceMode.Impulse);

        }

        PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
        if (ph != null) {
            ph.changeHealth(-1);
        }

        if (killOnWall) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        PlayerHealth ph = other.gameObject.GetComponent<PlayerHealth>();
        if (ph != null) {
            ph.changeHealth(-1);
        }
    }
}
