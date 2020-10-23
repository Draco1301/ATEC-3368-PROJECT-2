using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorThree : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        transform.GetComponentInParent<MeshCollider>().enabled = true;
        TutorialController.instantce.passedRoomTwo = true;
    }
}
