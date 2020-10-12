using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //Ray Cast
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float fireDistance;
    [SerializeField] LayerMask mask;

    //Time Stop Bullet
    [SerializeField] TimeBullet tBullet;

    //effects
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] AudioSource gunSound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shoot") && !LevelController.instance.isPaused) {
            muzzleFlash.Play();
            gunSound.Play();

            if (!PlayerTimeStop.TimeStop) {
                Shoot();
            } else {
                TimeStopShoot();
            }
        }
    }

    private void TimeStopShoot() {
        TimeBullet tb = Instantiate(tBullet.gameObject, cameraController.transform.position + cameraController.transform.rotation * new Vector3(0.38f, -0.26f, 1.135f), cameraController.transform.rotation).GetComponent<TimeBullet>();
        tb.setVar(cameraController.transform.forward, rayOrigin.position, fireDistance, mask);
    }

    private void Shoot() {
        //Calc direction to shoot ray
        Vector3 direction = cameraController.transform.forward;
        
        //debug ray
        Debug.DrawRay(rayOrigin.position, direction * fireDistance, Color.red, 2f);

        //bullet trail
        Vector3[] pos = new Vector3[2];
        pos[0] = muzzleFlash.transform.position;

        //ray cast
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin.position, direction, out hitInfo, fireDistance, mask)) {
            pos[1] = hitInfo.point;

            EnemyHealth eh = hitInfo.transform.GetComponent<EnemyHealth>();
            if(eh != null){
                eh.takeDamage(1);
            }

            Rigidbody rb = hitInfo.transform.GetComponent<Rigidbody>();
            if (rb != null) {
                Vector3 dir = hitInfo.transform.position - hitInfo.point;
                rb.AddForceAtPosition(dir * 20, hitInfo.point, ForceMode.Impulse);
            }

            EnemyBullet eb = hitInfo.transform.GetComponent<EnemyBullet>();
            if (eb != null) {
                Destroy(eb.gameObject);
            }



        } else {
            pos[1] = transform.position + transform.forward * fireDistance;
        }

        TrailManager.instantce.createTrail(pos);

    }
}
