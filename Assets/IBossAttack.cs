using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossAttack
{
    void Attack();
    bool isAttacking();
    bool isFinished();
    void destoryThis();
    void setBullet(EnemyBullet eb);

}
