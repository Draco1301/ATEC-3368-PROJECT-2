using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_NineShot : MonoBehaviour, IBossAttack
{
    [SerializeField] EnemyBullet bullet;
    private bool isAttack = false;
    private bool isFin = false;
    Coroutine attack;

    public void Attack() {
        isAttack = true;
        transform.position = new Vector3(25f, 11.75f, -25f);
        transform.GetComponent<BossAI>().moveSpeed = 0;
        attack = StartCoroutine(Spawnbullets());
    }

    private IEnumerator Spawnbullets() {
        for (int i=0; i<9;i++) {
            while (TimeManager.TimeStoped) {
                yield return null;
            }

            EnemyBullet eb;
            Quaternion rot;
            eb = Instantiate(bullet, transform.position + new Vector3(0, 0.75f, 0) + transform.forward, Quaternion.Euler(0,0,0)) ;
            eb.transform.LookAt(PlayerMovement.instance.transform);
            rot = eb.transform.rotation;
            eb.Speed = 20;

            for (int n=-2; n<= 2 ;n++) {
                while (TimeManager.TimeStoped) {
                    yield return null;
                }
                for (int d = -2; d <= 2; d++) {
                    while (TimeManager.TimeStoped) {
                        yield return null;
                    }
                    if (n != 0 || d != 0) {
                        eb = Instantiate(bullet, transform.position + new Vector3(0, 0.75f, 0) + transform.forward + transform.up * n + transform.right * d, Quaternion.Euler(0, 0, 0));
                        eb.transform.rotation = rot;
                        eb.Speed = 20;
                    }
                }
            }

            yield return new WaitForSeconds(1.5f);
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
}
