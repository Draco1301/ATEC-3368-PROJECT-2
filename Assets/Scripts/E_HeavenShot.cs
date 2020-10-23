using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_HeavenShot : MonoBehaviour, IEnemyAttack
{
    [SerializeField] GameObject bullet;

    public void Attack() {
        StartCoroutine(SpawnBullets());
    }

    private IEnumerator SpawnBullets() {

        GameObject b = Instantiate(bullet, transform.position + new Vector3(-3, 0, 0), transform.rotation * Quaternion.Euler(-90, 0, 0) * Quaternion.Euler(0, -90, 0));
        StartCoroutine(target(b.GetComponent<EnemyBullet>()));
        yield return new WaitForSeconds(0.2f);

        b = Instantiate(bullet, transform.position + new Vector3(-2.121f, 2.121f, 0), transform.rotation * Quaternion.Euler(-90, 0, 0) * Quaternion.Euler(0, -45, 0));
        StartCoroutine(target(b.GetComponent<EnemyBullet>()));
        yield return new WaitForSeconds(0.2f);

        b = Instantiate(bullet, transform.position + new Vector3(0, 3, 0), transform.rotation * Quaternion.Euler(-90, 0, 0));
        StartCoroutine(target(b.GetComponent<EnemyBullet>()));
        yield return new WaitForSeconds(0.2f);

        b = Instantiate(bullet, transform.position + new Vector3(2.121f, 2.121f, 0), transform.rotation * Quaternion.Euler(-90, 0, 0) * Quaternion.Euler(0, 45, 0));
        StartCoroutine(target(b.GetComponent<EnemyBullet>()));
        yield return new WaitForSeconds(0.2f);

        b = Instantiate(bullet, transform.position + new Vector3(3, 0, 0), transform.rotation * Quaternion.Euler(-90, 0, 0) * Quaternion.Euler(0, 90, 0));
        StartCoroutine(target(b.GetComponent<EnemyBullet>()));
        yield return new WaitForSeconds(0.2f);
    }

    public IEnumerator target(EnemyBullet eb) {
        eb.Speed /= 10; 
        yield return new WaitForSeconds(2f);
        eb.transform.LookAt(PlayerMovement.instance.transform);
        eb.Speed *= 10;
    }

}
