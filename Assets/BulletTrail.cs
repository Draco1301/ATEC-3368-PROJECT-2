using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    LineRenderer lr;
    Vector3[] pos = new Vector3[2];
    float time = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.GetPositions(pos);
    }

    private void Update() {
        lr.SetPosition(0, new Vector3(Mathf.Lerp(pos[0].x, pos[1].x, time), Mathf.Lerp(pos[0].y, pos[1].y, time), Mathf.Lerp(pos[0].z, pos[1].z, time)));
        if (!PlayerTimeStop.TimeStop) {
            time += Time.deltaTime *10;
            if (time >= 1) {
                Destroy(this.gameObject);
            }
        }
    }

}
