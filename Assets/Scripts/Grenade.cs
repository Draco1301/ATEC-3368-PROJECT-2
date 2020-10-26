using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    float timer = 5f;
    [SerializeField] float radius = 10;

    private void Start() {
        if (TimeManager.TimeStoped) {
            GetComponent<TimeAffected>().Invoke("stop",0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimeManager.TimeStoped && !TimeManager.getPITS()) {
            timer -= Time.deltaTime;
        }
        if (timer<0) {
            Explode();
        }
    }

    public void Explode() {
        Collider[] collisions = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider c in collisions) {
            Grenade gd = c.GetComponent<Grenade>();
            if (gd != null) {
                gd.Invoke("Explode",0.15f);
            }

            EnemyHealth eh = c.GetComponent<EnemyHealth>();
            if (eh != null) {
                eh.takeDamage(50);
            }
            
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(40,transform.position, radius, 5, ForceMode.Impulse);
            }

            EnemyBullet eb = c.GetComponent<EnemyBullet>();
            if (eb != null) {
                Destroy(eb.gameObject);
            }
        }

        GrenadeParticlesManager.instance.spawn(transform.position);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,radius);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.GetComponent<EnemyBullet>() != null && !TimeManager.TimeStoped && !TimeManager.getPITS()) {
            Explode();
        }
    }
}
