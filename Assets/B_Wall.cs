using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Wall : MonoBehaviour, IBossAttack
{
    [SerializeField] EnemyBullet bullet;
    private bool isAttack = false;
    private bool isFin = false;
    public void Attack() {
        isAttack = true;
        StartCoroutine(SpawnBullets());
        
    }

    private IEnumerator SpawnBullets() {
        Vector3 startPos = new Vector3(1.5f, 6.5f, -0.55f);
        EnemyBullet[,] eb = new EnemyBullet[23,6];
        for (int i=0;i<6;i++) {
            for (int n = 0; n < 23; n++) {
                eb[n, i] = Instantiate(bullet, startPos + new Vector3(n *2,i *2, 0), Quaternion.Euler(0, 180, 0));
                eb[n, i].Speed = 0;
                while (PlayerTimeStop.TimeStoped) {
                    yield return null;
                }
            }
            while (PlayerTimeStop.TimeStoped) {
                yield return null;
            }
            yield return new WaitForSeconds(1);
        }

        for (int i = 0; i < 6; i++) {
            for (int n = 0; n < 23; n++) {
                eb[n, i].Speed = 5;
                while (PlayerTimeStop.TimeStoped) {
                    yield return null;
                }
            }
            while (PlayerTimeStop.TimeStoped) {
                yield return null;
            }
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(11);
        isAttack = false;
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
}
