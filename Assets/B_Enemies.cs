using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B_Enemies : MonoBehaviour, IBossAttack
{
    [SerializeField] EnemyBullet bullet;
    private bool isFin = false;
    private bool isAttack = false;
    Coroutine attack;

    [SerializeField] EnemyAI enemy;
    EnemyAI[] enemies = new EnemyAI[12];
    EnemyHealth eh;

    float time = 0;

    Text text;

    public void Attack() {
        isAttack = true;

        eh = transform.GetComponent<EnemyHealth>();
        text.text = "I need a break";
        for (int i = 0; i < 2; i++) {
            for (int n = 0; n < 2; n++) {
                enemies[0 + 3 * n + 6 * i] = Instantiate(enemy, new Vector3(10f + 30f * n, 6.75f, -10f + 5 - 30 * i), Quaternion.Euler(0, 0, 0));
                enemies[0 + 3 * n + 6 * i].transform.gameObject.AddComponent<E_BasicAttack>();
                enemies[0 + 3 * n + 6 * i].transform.gameObject.GetComponent<IEnemyAttack>().setBullet(bullet.gameObject);

                enemies[1 + 3 * n + 6 * i] = Instantiate(enemy, new Vector3(10f - 4.33f + 30f * n, 6.75f, -10f - 4.33f - 30 * i), Quaternion.Euler(0, 0, 0));
                enemies[1 + 3 * n + 6 * i].transform.gameObject.AddComponent<E_HeavenShot>();
                enemies[0 + 3 * n + 6 * i].transform.gameObject.GetComponent<IEnemyAttack>().setBullet(bullet.gameObject);

                enemies[2 + 3 * n + 6 * i] = Instantiate(enemy, new Vector3(10f + 4.33f + 30f * n, 6.75f, -10f - 4.33f - 30 * i), Quaternion.Euler(0, 0, 0));
                enemies[2 + 3 * n + 6 * i].transform.gameObject.AddComponent<E_ShotgunFire>();
                enemies[0 + 3 * n + 6 * i].transform.gameObject.GetComponent<IEnemyAttack>().setBullet(bullet.gameObject);
            }
        }
    }

    private void Update() {
        if (isAttack) {
            transform.GetComponent<CharacterController>().enabled = false;
            transform.position = new Vector3(-10,3,0);
            if (time < 0) {
                time = 1;
                eh.takeDamage(-3);
            }
            if (!TimeManager.TimeStoped) {
                time -= Time.deltaTime;
            }

            foreach (EnemyAI e in enemies) {
                if (e != null) {
                    return;
                }
            }

            transform.position = new Vector3(25, 11.75f, -25);
            transform.GetComponent<CharacterController>().enabled = true;
            StartCoroutine( finish());
        }
    }


    public IEnumerator finish() {
        text.text = "Alright I'm back";
        yield return new WaitForSeconds(5f);
        text.text = "";
        isFin = true;
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

    public void destoryThis() {
        Destroy(this);
    }

    public void stopAttack() {

    }

    public void setOther(GameObject g) {
        enemy = g.GetComponent<EnemyAI>();
    }

    public void setBossText(Text text) {
        this.text = text;
    }
}
