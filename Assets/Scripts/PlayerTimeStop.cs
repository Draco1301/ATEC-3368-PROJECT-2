using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTimeStop : MonoBehaviour
{
    public static bool TimeStoped = false;
    [SerializeField] float timeLimit;
    [SerializeField] float rechargeTime;
    [SerializeField] Renderer ClockMesh;
    [SerializeField] Texture2D[] greenText;
    [SerializeField] Texture2D[] playSymbol = new Texture2D[2];
    float charge = 1;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) && charge >= 1) || (charge < 0) || (Input.GetKeyDown(KeyCode.E) && TimeStoped)) {
            if (TimeStoped) {
                charge = 0;
            } else {
                charge = 1;
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
            ClockMesh.material.mainTexture = greenText[(int)(charge* timeLimit)];
        } else {
            charge += Time.deltaTime / rechargeTime;
            if (charge >= 1) {
                ClockMesh.material.mainTexture = playSymbol[1];
            } else {
                ClockMesh.material.mainTexture = playSymbol[0];
            }
        }

    }
}
