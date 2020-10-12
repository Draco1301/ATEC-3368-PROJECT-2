using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    public static TrailManager instantce;
    [SerializeField] LineRenderer lr;
    [SerializeField] int limit = 20;

    private void Awake() {
        if (instantce == null) {
            instantce = this;
        } else {
            Destroy(this.gameObject);
        }
    }


    public void createTrail(Vector3[] pos) {
        LineRenderer line = Instantiate(lr.gameObject,transform).GetComponent<LineRenderer>();
        line.SetPositions(pos);

    }

}
