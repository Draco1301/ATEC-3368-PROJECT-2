using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeStop : MonoBehaviour
{
    public static bool TimeStop = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            TimeStop = !TimeStop;
            TimeAffected[] objects = FindObjectsOfType<TimeAffected>();
            if (TimeStop) {
                foreach (TimeAffected t in objects) {
                    t.stop();
                }
            } else {
                foreach (TimeAffected t in objects) {
                    t.resume();
                }
            }
        }
    }
}
