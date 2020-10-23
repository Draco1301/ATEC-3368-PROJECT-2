using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    PlayerMovement pm;
    EnemyHealth health;
    [SerializeField] EnemyBullet eb;


    //attack
    [SerializeField] float moveSpeed;
    IBossAttack BossAttack;
    [SerializeField] float timeBetweenAttacks;
    private float timeSinceAttack;

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
    }


    void Update() {

        Move();

        if (BossAttack != null && !BossAttack.isAttacking() && !BossAttack.isFinished()) {
            BossAttack.Attack();
        }
        if (BossAttack != null && BossAttack.isFinished()) {
            BossAttack.destoryThis();
            BossAttack = gameObject.AddComponent<B_NineShot>();
            BossAttack.setBullet(eb);
        }

        ApplyGravity();
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
}
