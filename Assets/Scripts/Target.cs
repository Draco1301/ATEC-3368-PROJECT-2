using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isOn;
    public bool isComplete;
    float resetTime = 0.5f;
    float time;
    [SerializeField] Material on;
    [SerializeField] Material off;

    // Update is called once per frame
    void Update()
    {
        if (!isComplete && isOn) {
            time -= Time.deltaTime;
            if (time < 0) {
                isOn = false;
                GetComponent<MeshRenderer>().material = off;
            }
        }        
    }

    public void ShotAt() {
        time = resetTime;
        isOn = true;
        GetComponent<MeshRenderer>().material = on;
    }
}
