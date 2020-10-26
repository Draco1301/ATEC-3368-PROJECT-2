using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    //Ray Cast
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float fireDistance;
    [SerializeField] LayerMask mask;

    //Time Stop Bullet
    [SerializeField] TimeBullet tBullet;
    [SerializeField] int ammoMax;
    int ammo;

    //effects
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] AudioSource gunSound;

    //timer
    [SerializeField] float fireRate;
    float fireTimer;

    [SerializeField] float reloadTime;
    [SerializeField] GameObject gun;
    [SerializeField] Text ammoText;
    private bool isReloading;
    private float reloadProg;

    // Update is called once per frame
    void Update()
    {
        if (!TimeManager.getPITS()) {
            if (Input.GetButtonDown("Shoot") && fireTimer <= 0 && !LevelController.instance.isPaused && !isReloading) {

                if (ammo > 0) {
                    if (!TimeManager.TimeStoped) {
                        Shoot();
                    } else {
                        TimeStopShoot();
                    }
                    ammo--;
                    ammoText.text = ammo + "/" + ammoMax;
                    muzzleFlash.Play();
                    gunSound.Play();
                } else {
                    isReloading = true;
                    reloadProg = reloadTime;
                }
                fireTimer = fireRate;
            }

            fireTimer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.R) && !isReloading) {
                isReloading = true;
                reloadProg = reloadTime;
            }
            if (isReloading) {
                reload((reloadTime - reloadProg) / reloadTime);
                reloadProg -= Time.deltaTime;
                if (reloadProg <= 0) {
                    isReloading = false;
                    ammo = ammoMax;
                    ammoText.text = ammo + "/" + ammoMax;
                }
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
            BossAI BAI = hitInfo.transform.GetComponent<BossAI>();
            if (BAI != null && eh != null) {
                float dist = (Vector3.Distance(transform.position, BAI.transform.position));
                if (dist < 5f) {
                    eh.takeDamage(10);
                } else if (dist < 12.5f) {
                    eh.takeDamage(5);
                } else if (dist < 20f) {
                    eh.takeDamage(3);
                } else {
                    eh.takeDamage(1);
                }

            } else if (eh != null){
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

            Grenade g = hitInfo.transform.GetComponent<Grenade>();
            if (g != null) {
                g.Explode();
            }

            Target t = hitInfo.transform.GetComponent<Target>();
            if (t != null) {
                t.ShotAt();
            }


        } else {
            pos[1] = muzzleFlash.transform.position + muzzleFlash.transform.forward * fireDistance;
        }

        TrailManager.instantce.createTrail(pos);

    }

    private void reload(float t) {
        float temp = Mathf.SmoothStep(0,360,t);
        gun.transform.localRotation = Quaternion.Euler(temp,0,0);
    }
}
