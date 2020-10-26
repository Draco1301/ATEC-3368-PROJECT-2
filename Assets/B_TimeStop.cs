using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_TimeStop : MonoBehaviour, IBossAttack {
    [SerializeField] EnemyBullet bullet;
    private bool isAttack = false;
    private bool isFin = false;
    Coroutine attack;

    public void Attack() {
        isAttack = true;
        attack = StartCoroutine(Spawnbullets());
    }
    private IEnumerator Spawnbullets() {
        EnemyBullet eb;
        EnemyBullet[] ebs = new EnemyBullet[50];
        float yRot;
        Vector3 pos;
        
        transform.GetComponent<BossAI>().enabled = false;

        TimeManager.setPITS(true);
        

        for (int i = 0; i < 50 ;i++) {
            yRot = Random.Range(0,360);
            pos = new Vector3(Random.Range(2, 48), Random.Range(6, 10), Random.Range(-48, -2));
            
            transform.rotation = Quaternion.Euler(0, yRot, 0);
            transform.position = pos - transform.forward * 1.2f;
            
            eb = Instantiate(bullet);
            eb.transform.position = pos;
            eb.transform.rotation = Quaternion.Euler(0, yRot, 0);
            eb.Speed = 0;
            ebs[i] = eb;

            yield return new WaitForSeconds(0.1f);
        }
        transform.position = new Vector3(25f, 11.75f, -25f);

        yield return new WaitForSeconds(1.1f);

        foreach (EnemyBullet e in ebs) {
            e.Speed = 20;
        }
        TimeManager.setPITS(false); 

        while (TimeManager.TimeStoped) {
            yield return null;
        }

        transform.GetComponent<BossAI>().enabled = true;
        yield return new WaitForSeconds(10);
        isFin = true;
    }
    public bool isAttacking() {
        return isAttack;
    }

    public bool isFinished() {
        return isFin;
    }

    public void setBullet(EnemyBullet eb) {
        bullet = eb;
    }
    public void destoryThis() {
        Destroy(this);
    }
    public void stopAttack() {
        StopCoroutine(attack);
    }
}
