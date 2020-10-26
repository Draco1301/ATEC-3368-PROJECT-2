using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B_Intro : MonoBehaviour, IBossAttack
{
    Text text;
    bool isAttack = false;
    bool isfin = false;


    public void Attack() {
        isAttack = true;
        StartCoroutine(Message());
    }

    public IEnumerator Message() {
        text.text = "Beat me as fast as you can";
        yield return new WaitForSeconds(3f);
        text.text = "remember the closer you are the more damage you do";
        yield return new WaitForSeconds(3f);
        text.text = "";
        isfin = true;

    }

    public void destoryThis() {
        Destroy(this);
    }

    public bool isAttacking() {
        return isAttack;
    }

    public bool isFinished() {
        return isfin;
    }

    public void setBossText(Text text) {
        this.text = text;
    }

    public void setBullet(EnemyBullet eb) {
        return;
    }

    public void setOther(GameObject g) {
        return;
    }

    public void stopAttack() {
        StopCoroutine(Message());
    }
}
