using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOne : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        transform.GetComponentInParent<MeshCollider>().enabled = true;
        TutorialController.instantce.passedRoomOne = true;
    }
}
