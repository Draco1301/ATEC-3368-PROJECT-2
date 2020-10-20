using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{
    [SerializeField] GameObject grenade;
    [SerializeField] int maxAmmo = 3;
    int ammoCount = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && ammoCount > 0) {
            Rigidbody rb = Instantiate(grenade, transform.position + new Vector3(0,0.3f,0) + transform.forward, Random.rotation).GetComponent<Rigidbody>();
            if (Camera.main.transform.rotation.x > 0) {
                rb.AddForce(transform.forward * 20, ForceMode.Impulse);
            } else { 
                rb.AddForce(Camera.main.transform.rotation * Vector3.forward * 20, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(0, 0.5f, 0) + transform.forward , transform.position + Quaternion.Euler(Camera.main.transform.rotation.x,0,0) * (transform.forward * 20));
    }
}
