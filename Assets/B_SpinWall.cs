using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_SpinWall : MonoBehaviour, IBossAttack
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
        GameObject Pivot = new GameObject("Pivot");
        Pivot.transform.parent = transform;
        Pivot.transform.localPosition = new Vector3(0, 0, 0);
        Pivot.transform.parent = null;

        EnemyBullet eb;

        float startHieght = -5.25f;
        float startDist = 2.5f;

        for (int i = 0; i <= 3; i++) {
            for (int n = 0; n <= 19; n++) {
                while (TimeManager.TimeStoped) {
                    yield return null;
                }
                eb = Instantiate(bullet, Pivot.transform);
                eb.transform.localPosition = new Vector3(startDist + 2 * n, startHieght + 2 * i, 0);
                eb.Speed = 0;
                eb.killOnWall = false;

                eb = Instantiate(bullet, Pivot.transform);
                eb.transform.localPosition = new Vector3(-startDist - 2 * n, startHieght + 2 * i, 0);
                eb.Speed = 0;
                eb.killOnWall = false;

                eb = Instantiate(bullet, Pivot.transform);
                eb.transform.localPosition = new Vector3(0, startHieght + 2 * i, startDist + 2 * n);
                eb.Speed = 0;
                eb.killOnWall = false;

                eb = Instantiate(bullet, Pivot.transform);
                eb.transform.localPosition = new Vector3(0, startHieght + 2 * i, -startDist - 2 * n);
                eb.Speed = 0;
                eb.killOnWall = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);

        float time = 40f;
        while (time > 0) {
            while (TimeManager.TimeStoped) {
                yield return null;
            }
            time -= Time.deltaTime;
            Pivot.transform.Rotate(new Vector3(0,45 * Time.deltaTime ,0), Space.Self);
            yield return new WaitForEndOfFrame();
        }
        Destroy(Pivot);

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
