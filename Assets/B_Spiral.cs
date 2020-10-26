using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B_Spiral : MonoBehaviour, IBossAttack
{
    [SerializeField] EnemyBullet bullet;
    private bool isAttack = false;
    private bool isFin = false;
    Coroutine attack;
    Text text;

    public void Attack() {
        isAttack = true;
        transform.position = new Vector3(25f, 11.75f, -25f);
        transform.GetComponent<BossAI>().moveSpeed = 0;
        attack = StartCoroutine(Spawnbullets());
    }

    public IEnumerator Spawnbullets() {
        EnemyBullet eb;
        for (int i=0; i<360 ;i++) {
            while (TimeManager.TimeStoped) {
                yield return null;
            }
            for (int n = 0; n <= 3; n++) {
                float m = (2f * i + 90 * n);
                eb = Instantiate(bullet, transform.position + new Vector3(0, 0.75f, 0) + new Vector3(Mathf.Cos(m * Mathf.Deg2Rad), 0, Mathf.Sin(m * Mathf.Deg2Rad)), Quaternion.Euler(0, 0, 0));
                eb.transform.LookAt(new Vector3(transform.position.x, eb.transform.position.y, transform.position.z));
                eb.transform.Rotate(new Vector3(0, 180, 0));
                eb.transform.Rotate(new Vector3(20, 0, 0), Space.Self);
                eb.Speed = 10f;

                float p = (-2f * i + 90 * n + 45);
                eb = Instantiate(bullet, transform.position + new Vector3(0, 0.75f, 0) + new Vector3(Mathf.Cos(p * Mathf.Deg2Rad), 0, Mathf.Sin(p * Mathf.Deg2Rad)), Quaternion.Euler(0, 0, 0));
                eb.transform.LookAt(new Vector3(transform.position.x, eb.transform.position.y, transform.position.z));
                eb.transform.Rotate(new Vector3(0, 180, 0));
                eb.transform.Rotate(new Vector3(30, 0, 0), Space.Self);
                eb.Speed = 10f;
            }

            if (i % 45 == 0) {
                while (TimeManager.TimeStoped) {
                    yield return null;
                }
                for (int d = 0; d <= 359; d++) {
                    eb = Instantiate(bullet, transform.position + new Vector3(0, 0.75f, 0) + new Vector3(Mathf.Cos(d * Mathf.Deg2Rad), 0, Mathf.Sin(d * Mathf.Deg2Rad)), Quaternion.Euler(0, 0, 0));
                    eb.transform.LookAt(new Vector3(transform.position.x, eb.transform.position.y, transform.position.z));
                    eb.transform.Rotate(new Vector3(0, 180, 0));
                    eb.transform.Rotate(new Vector3(10, 0, 0), Space.Self);
                    eb.Speed = 15f;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
        isFin = true;
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

    public void stopAttack() {
        StopCoroutine(attack);
    }

    public void setOther(GameObject g) {
        throw new NotImplementedException();
    }
    public void setBossText(Text text) {
        this.text = text;
    }
}
