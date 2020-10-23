using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{
    [SerializeField] GameObject grenade;
    [SerializeField] GameObject[] images;
    [SerializeField] int maxAmmo;
    [SerializeField] float refillTime;
    float refillTimer;
    int ammoCount;

    private void Start() {
        ammoCount = maxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && ammoCount > 0) {
            Rigidbody rb = Instantiate(grenade, transform.position + new Vector3(0,0.3f,0) + transform.forward, Random.rotation).GetComponent<Rigidbody>();
            if (Camera.main.transform.localRotation.x > 0) {
                rb.AddForce(transform.forward * 20, ForceMode.Impulse);
            } else { 
                rb.AddForce(Camera.main.transform.rotation * Vector3.forward * 20, ForceMode.Impulse);
            }
            ammoCount--;
            images[ammoCount].SetActive(false);
            
        }
        if (ammoCount < maxAmmo) {
            refillTimer -= Time.deltaTime;
            if (refillTimer < 0) {
                refillTimer = refillTime;
                images[ammoCount].SetActive(true);
                ammoCount++;
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(0, 0.5f, 0) + transform.forward , transform.position + Camera.main.transform.rotation * Vector3.forward * 20);
    }
}
