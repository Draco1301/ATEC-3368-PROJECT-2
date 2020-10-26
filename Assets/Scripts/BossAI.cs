using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    //components
    PlayerMovement pm;
    EnemyHealth health;
    [SerializeField] EnemyAI enemyAI;
    [SerializeField] EnemyBullet enemyBullet;
    [SerializeField] GameObject killWall;
    [SerializeField] Text text;


    //attack
    [SerializeField] public float moveSpeed;
    IBossAttack BossAttack;
    private int attackNum;
    private bool checkPoint_1 = false;
    private bool checkPoint_2 = false;
    private bool regen_1 = false;
    private bool regen_2 = false;

    //Move
    CharacterController cc;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    private bool grounded;
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    [SerializeField] Transform gunPivot;

    private void Start() {
        pm = PlayerMovement.instance;
        cc = GetComponent<CharacterController>();
        health = GetComponent<EnemyHealth>();
        BossAttack = GetComponent<IBossAttack>();
        BossAttack.setBossText(text);
    }


    void Update() {

        if (health.getHealth() <= 0) {
            return;
        }

        if (health.getPercent() <= .66 ) {
            checkPoint_1 = true;
        }
        if (health.getPercent() <= .33) {
            checkPoint_1 = true;
        }

        if (!TimeManager.TimeStoped) {
            Move();


            if (BossAttack != null && !BossAttack.isAttacking() && !BossAttack.isFinished()) {
                BossAttack.Attack();
            }
            if (BossAttack != null && BossAttack.isFinished()) {
                BossAttack.destoryThis();
                attackNum = BossAttackRandomizer(attackNum);
                switch (attackNum) {
                    case 0:
                        BossAttack = gameObject.AddComponent<B_Enemies>();
                        BossAttack.setOther(enemyAI.gameObject);
                        break;
                    case 1:
                        BossAttack = gameObject.AddComponent<B_NineShot>();
                        break;
                    case 2:
                        BossAttack = gameObject.AddComponent<B_SpinWall>();
                        BossAttack.setOther(killWall);
                        break;
                    case 3:
                        BossAttack = gameObject.AddComponent<B_Spiral>();
                        break;
                    case 4:
                        BossAttack = gameObject.AddComponent<B_TimeStop>();
                        break;
                    case 5:
                        BossAttack = gameObject.AddComponent<B_Wall>();
                        break;
                    default:
                        BossAttack = gameObject.AddComponent<B_NineShot>();
                        break;
                }
                BossAttack.setBullet(enemyBullet);
                BossAttack.setBossText(text);
            }

            ApplyGravity();
        }
    }

    private void ApplyGravity() {
        //Ground Checking
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (grounded && velocity.y < 0) {
            velocity.y = -2;
        }
        cc.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
    }

    private void Move() {
        Vector3 move = pm.transform.position - transform.position;
        move.y = 0;
        move = move.normalized;
        cc.Move(move * moveSpeed * Time.deltaTime);
        transform.LookAt(new Vector3(pm.transform.position.x, transform.position.y, pm.transform.position.z));
        gunPivot.LookAt(PlayerMovement.instance.transform);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
    }

    private int BossAttackRandomizer(int x) {
        if (checkPoint_1 &&  !regen_1) {
            regen_1 = true;
            return 0;
        }
        if (checkPoint_2 && !regen_2) {
            regen_2 = true;
            return 0;
        }

        int r;
        do { 
            r = Random.Range(1, 6);
        } while (r == x);

        return r;
    }

    private void OnDestroy() {
        if (BossAttack != null && BossAttack.isAttacking() && !BossAttack.isFinished()) {
            BossAttack.stopAttack();
        }
    }
}
