using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_BasicAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField] GameObject bullet;

    public void Attack() {
        GameObject g = Instantiate(bullet, transform.position + transform.rotation * Vector3.forward * 2, transform.rotation);
        g.transform.LookAt(PlayerMovement.instance.transform);
    }

    public void setBullet(GameObject g) {
        bullet = g;
    }
}
