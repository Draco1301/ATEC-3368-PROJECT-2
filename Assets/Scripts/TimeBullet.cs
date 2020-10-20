using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBullet : MonoBehaviour
{
    //Ray cast
    Vector3 rayDirection;
    Vector3 rayOrigin;
    float fireDistance;
    LayerMask mask;
    bool once = false;

    //effects
    [SerializeField] GameObject art;
    Vector3 startPos;
    Vector3 endPos;
    float localTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerTimeStop.TimeStoped) {
            Destroy(this.gameObject);
        }
        startPos = art.transform.position;
        endPos = art.transform.position + art.transform.forward * 0.5f;
    }

    private void Update() {
        if (!PlayerTimeStop.TimeStoped && once) {
            once = false;
            RaycastHit hitInfo;
            Vector3[] pos = new Vector3[2];
            pos[0] = transform.position;

            if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo, fireDistance, mask)) {

                EnemyHealth eh = hitInfo.transform.GetComponent<EnemyHealth>();
                if (eh != null) {
                    eh.takeDamage(1);
                }

                Rigidbody rb = hitInfo.transform.GetComponent<Rigidbody>();
                if (rb != null) {
                    Vector3 dir = hitInfo.transform.position - hitInfo.point;
                    rb.AddForceAtPosition(dir * 20, hitInfo.point, ForceMode.Impulse);
                }

                EnemyBullet eb = hitInfo.transform.GetComponent<EnemyBullet>();
                if (eb != null) {
                    Destroy(eb.gameObject);
                }

                Grenade g = hitInfo.transform.GetComponent<Grenade>();
                if (g != null) {
                    g.Explode();
                }


                pos[1] = hitInfo.point;
            } else {
                pos[1] = transform.position + transform.forward * fireDistance;
            }


            TrailManager.instantce.createTrail(pos);
            
            Destroy(transform.GetChild(0).gameObject);
            Destroy(this.gameObject,3);
        }

        if (PlayerTimeStop.TimeStoped) {
            art.transform.position = new Vector3(Mathf.SmoothStep(startPos.x,endPos.x,localTime), Mathf.SmoothStep(startPos.y, endPos.y, localTime), Mathf.SmoothStep(startPos.z, endPos.z, localTime));
            localTime += Time.deltaTime * 10;
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
