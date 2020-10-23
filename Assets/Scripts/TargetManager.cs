using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    bool isComplete;
    [SerializeField] Target[] targets;

    // Update is called once per frame
    void Update()
    {
        isComplete = CheckForComplete();
        if (isComplete) {
            foreach (Target t in targets) {
                t.isComplete = true;
            }
            TutorialController.instantce.roomCleared = true;
        }
    }

    bool CheckForComplete() {
        foreach (Target t in targets) {
            if (!t.isOn)
                return false;
        }
        return true;
    }
}
