using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeParticlesManager : MonoBehaviour
{
    public static GrenadeParticlesManager instance;
    public GameObject ps;

    private void Start() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public void spawn(Vector3 pos) {
        Instantiate(ps, pos, Quaternion.Euler(0,0,0), this.transform);
    }
}
