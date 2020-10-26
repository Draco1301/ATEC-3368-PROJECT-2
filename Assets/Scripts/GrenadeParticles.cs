using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeParticles : MonoBehaviour
{
    float timer = 2;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem[] ps = transform.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in ps) {
            p.Play();
        }
    }

    private void Update() {
        if (!TimeManager.TimeStoped) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                Destroy(this.gameObject);
            }
        }
    }
}
