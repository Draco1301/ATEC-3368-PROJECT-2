using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IBossAttack
{
    void Attack();
    bool isAttacking();
    bool isFinished();
    void destoryThis();
    void setBullet(EnemyBullet eb);
    void stopAttack();
    void setOther(GameObject g);
    void setBossText(Text text);

}
