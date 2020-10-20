using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAffected : MonoBehaviour
{
    Rigidbody rb;
    EnemyAI eAI;
    ParticleSystem ps;
    Vector3 saveVelocity;
    Vector3 saveAngularVelocity;

    // Start is called before the first frame update
    public void stop() {
        rb = GetComponent<Rigidbody>();
        eAI = GetComponent<EnemyAI>();
        ps = GetComponent<ParticleSystem>();

        if (rb != null) {
            saveVelocity = rb.velocity;
            saveAngularVelocity = rb.angularVelocity;
            rb.isKinematic = true;

            rb.velocity = Vector3.zero;
        }

        if (eAI != null) {
            eAI.enabled = false;
        }

        if (ps != null) {
            ps.Pause();
        }
    }

    public void resume() {
        rb = GetComponent<Rigidbody>();
        eAI = GetComponent<EnemyAI>();
        ps = GetComponent<ParticleSystem>();

        if (rb != null) {
            rb.isKinematic = false;
            rb.velocity = saveVelocity;
            rb.angularVelocity = saveAngularVelocity;
        }
        if (eAI != null) {
            eAI.enabled = true;
        }
        if (ps != null) {
            ps.Play();
        }
    }


}
