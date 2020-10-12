using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    PlayerMovement pm;
    EnemyHealth health;

    //attack
    [SerializeField] float awareRadius;
    [SerializeField] float limitRadius;
    [SerializeField] float moveSpeed;
    IEnemyAttack EnemyAttack;
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
    
    private void Start() {
        pm = PlayerMovement.instance;
        cc = GetComponent<CharacterController>();
        health = GetComponent<EnemyHealth>();
        EnemyAttack = GetComponent<IEnemyAttack>();
    }


    void Update() {

        if (awareRadius >= Mathf.Sqrt(Mathf.Pow(pm.gameObject.transform.position.x - transform.position.x, 2) + Mathf.Pow(pm.gameObject.transform.position.z - transform.position.z, 2))) {
            Attack();
        }
        timeSinceAttack += Time.deltaTime;

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

    void Attack() {
        if (limitRadius < Mathf.Sqrt(Mathf.Pow(pm.gameObject.transform.position.x - transform.position.x, 2) + Mathf.Pow(pm.gameObject.transform.position.z - transform.position.z, 2))) {
            Vector3 move = pm.transform.position - transform.position;
            move.y = 0;
            move = move.normalized;
            cc.Move(move * moveSpeed * Time.deltaTime);
        }
        transform.LookAt(new Vector3(pm.transform.position.x, transform.position.y, pm.transform.position.z));

        if (timeSinceAttack > timeBetweenAttacks) {
            timeSinceAttack = 0;
            EnemyAttack.Attack();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, awareRadius);
        Gizmos.DrawWireSphere(transform.position, limitRadius);
    }
}
