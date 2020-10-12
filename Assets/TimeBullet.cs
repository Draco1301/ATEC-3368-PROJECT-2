using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBullet : MonoBehaviour
{
    Vector3 rayDirection;
    Vector3 rayOrigin;
    float fireDistance;
    LayerMask mask;
    bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerTimeStop.TimeStop) {
            Destroy(this.gameObject);
        }

    }

    private void Update() {
        if (!PlayerTimeStop.TimeStop && once) {
            once = false;
            RaycastHit hitInfo;
            Vector3[] pos = new Vector3[2];
            pos[0] = transform.position;

            if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo, fireDistance, mask)) {
                Debug.Log("hit: " + hitInfo.transform.gameObject.name);

                EnemyHealth eh = hitInfo.transform.GetComponent<EnemyHealth>();
                if (eh != null) {
                    eh.takeDamage(1);
                }

                Rigidbody rb = hitInfo.transform.GetComponent<Rigidbody>();
                if (rb != null) {
                    Vector3 dir = hitInfo.transform.position - hitInfo.point;
                    rb.AddForceAtPosition(dir * 20, hitInfo.point, ForceMode.Impulse);
                }


                pos[1] = hitInfo.point;
            } else {
                pos[1] = transform.position + transform.forward * fireDistance;
            }


            TrailManager.instantce.createTrail(pos);
            
            Destroy(transform.GetChild(0).gameObject);
            Destroy(this.gameObject,3);
        }
    }

    public void setVar(Vector3 rD, Vector3 rO, float dist, LayerMask m) {
        rayDirection = rD;
        rayOrigin = rO;
        fireDistance = dist;
        mask = m;
        once = true;

    } 
}
