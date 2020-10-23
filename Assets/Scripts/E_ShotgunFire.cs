using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ShotgunFire : MonoBehaviour, IEnemyAttack
{
    [SerializeField] GameObject bullet;

    public void Attack() {
        GameObject g;
        g = Instantiate(bullet, transform.position + transform.rotation * Vector3.forward * 2, transform.rotation);
        g.transform.LookAt(PlayerMovement.instance.transform);
        
        g = Instantiate(bullet, transform.position + transform.rotation * new Vector3(0.707f, 0, 0.707f) * 2, transform.rotation * Quaternion.Euler(0, 45, 0));
        g.transform.LookAt(PlayerMovement.instance.transform);
        g.transform.localRotation = g.transform.localRotation * Quaternion.Euler(0, 45, 0);

        g = Instantiate(bullet, transform.position + transform.rotation * new Vector3(-0.707f, 0, 0.707f) * 2, transform.rotation * Quaternion.Euler(0, -45, 0));
        g.transform.LookAt(PlayerMovement.instance.transform);
        g.transform.localRotation = g.transform.localRotation * Quaternion.Euler(0, -45, 0);
    }
}
