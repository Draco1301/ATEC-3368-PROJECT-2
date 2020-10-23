using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Spiral : MonoBehaviour, IBossAttack
{
    [SerializeField] EnemyBullet bullet;
    private bool isAttack = false;
    private bool isFin = false;

    public void Attack() {
        isAttack = true;
        StartCoroutine(Spawnbullets());
    }

    private IEnumerator Spawnbullets() {
        EnemyBullet eb;
        for (int i=0; i<360 ;i++) {
            while (PlayerTimeStop.TimeStoped) {
                yield return null;
            }
            for (int n = 0; n <= 3; n++) {
                float m = (2f * i + 90 * n);
                eb = Instantiate(bullet, transform.position + new Vector3(0, 0.75f, 0) + new Vector3(Mathf.Cos(m * Mathf.Deg2Rad), 0, Mathf.Sin(m * Mathf.Deg2Rad)), Quaternion.Euler(0, 0, 0));
                eb.transform.LookAt(new Vector3(transform.position.x, eb.transform.position.y, transform.position.z));
                eb.transform.Rotate(new Vector3(0, 180, 0));
                eb.transform.Rotate(new Vector3(10, 0, 0), Space.Self);

                float p = (-2f * i + 90 * n + 45);
                eb = Instantiate(bullet, transform.position + new Vector3(0, 0.75f, 0) + new Vector3(Mathf.Cos(p * Mathf.Deg2Rad), 0, Mathf.Sin(p * Mathf.Deg2Rad)), Quaternion.Euler(0, 0, 0));
                eb.transform.LookAt(new Vector3(transform.position.x, eb.transform.position.y, transform.position.z));
                eb.transform.Rotate(new Vector3(0, 180, 0));
                eb.transform.Rotate(new Vector3(20, 0, 0), Space.Self);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public bool isAttacking() {
        return isAttack;
    }

    public bool isFinished() {
        return isFin;
    }

    public void destoryThis() {
        Destroy(this);
    }

    public void setBullet(EnemyBullet eb) {
        bullet = eb;
    }
}
