using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Enemies : MonoBehaviour, IBossAttack
{
    [SerializeField] EnemyBullet bullet;
    private bool isFin = false;
    private bool isAttack = false;
    Coroutine attack;

    EnemyAI enemy;
    EnemyAI[] enemies = new EnemyAI[12];
    EnemyHealth eh;

    public void Attack() {
        isAttack = true;

        eh = transform.GetComponent<EnemyHealth>();

        EnemyAI e;
        for (int i = 0; i < 2; i++) {
            for (int n = 0; n < 2; n++) {
                e = Instantiate(enemy, new Vector3(10f + 30f * n, 6.75f, -10f + 5 - 30 * i), Quaternion.Euler(0, 0, 0));
                e = enemies[0 + 3 * n + 6 * i];
                e.transform.gameObject.AddComponent<E_BasicAttack>();

                e = Instantiate(enemy, new Vector3(10f - 4.33f + 30f * n, 6.75f - 4.33f, -10f - 4.33f - 30 * i), Quaternion.Euler(0, 0, 0));
                e = enemies[1 + 3 * n + 6 * i];
                e.transform.gameObject.AddComponent<E_HeavenShot>();

                e = Instantiate(enemy, new Vector3(10f + 4.33f + 30f * n, 6.75f, -10f - 4.33f - 30 * i), Quaternion.Euler(0, 0, 0));
                e = enemies[2 + 3 * n + 6 * i];
                e.transform.gameObject.AddComponent<E_ShotgunFire>();
            }
        }
    }

    private void Update() {
        if (isAttack) {
            //Heal Boss
            //eh.takeDamage(-1);
            foreach (EnemyAI e in enemies) {
                if (e != null) {
                    return;
                }
            }
            isFin = true;
        }
    }

    public bool isAttacking() {
        return isAttack;
    }

    public bool isFinished() {
        return isFin;
    }

    //This script doesn't need this
    public void setBullet(EnemyBullet eb) {
        bullet = eb;
    }

    public void setEnemy(EnemyAI eb) {
        enemy = eb;
    }

    public void destoryThis() {
        Destroy(this);
    }

    public void stopAttack() {
        StopCoroutine(attack);
    }
}
