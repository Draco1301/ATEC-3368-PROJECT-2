using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeStop : MonoBehaviour
{
    public static bool TimeStoped = false;
    [SerializeField] float timeLimit;
    [SerializeField] float rechargeTime;
    float charge = 1;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) && charge >= 1) || (charge < 0) || (Input.GetKeyDown(KeyCode.E) && TimeStoped)) {
            if (TimeStoped) {
                charge = 0;
            }
            
            TimeStoped = !TimeStoped;
            TimeAffected[] objects = FindObjectsOfType<TimeAffected>();
            if (TimeStoped) {
                foreach (TimeAffected t in objects) {
                    t.stop();
                }
            } else {
                foreach (TimeAffected t in objects) {
                    t.resume();
                }
            }
        }

        if (TimeStoped) {
            charge -= Time.deltaTime / timeLimit;
        } else {
            charge += Time.deltaTime / rechargeTime;
        }

    }
}
