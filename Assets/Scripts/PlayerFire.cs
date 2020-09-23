using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] AudioSource gunSound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shoot") && !LevelController.instance.isPaused) {
            muzzleFlash.Play();
            gunSound.Play();
        }
    }
}
