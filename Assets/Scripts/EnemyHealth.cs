using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 2;
    [SerializeField] Image healthBar;
    private int maxHealth;

    private void Awake() {
        maxHealth = health;
    }

    public void takeDamage(int i) {
        health -= i;

        if (healthBar != null) {
            healthBar.fillAmount = (float)health / (float)maxHealth;
        }

        if (health<=0) {
            Destroy(this.gameObject.GetComponent<CharacterController>());
            this.gameObject.AddComponent<Rigidbody>();
            this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
            Destroy(this.gameObject.GetComponent<EnemyAI>());
            Destroy(this.gameObject.GetComponent<BossAI>());
            //this.gameObject.GetComponent<IBossAttack>().destoryThis();
        }

    }

    public int getHealth() {
        return health;
    }
}
