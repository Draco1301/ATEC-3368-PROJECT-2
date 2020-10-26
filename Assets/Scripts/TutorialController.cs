using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public static TutorialController instantce;
    [SerializeField] Text tutorialText;
    public bool passedRoomOne;
    public bool roomCleared;
    public bool passedRoomTwo;
    bool enemiesKilled;
    [SerializeField] GameObject door2;
    [SerializeField] EnemyAI[] ais;

    private void Awake() {
        if (instantce == null) {
            instantce = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update() {

        enemiesKilled = checkEnemies();

        if (enemiesKilled) {
            tutorialText.text = "Tutorial Completed Get Ready";
            LevelController.instance.GameOver();
        } else if (passedRoomTwo) {
            tutorialText.text = "You also throw grenades with right click\nShooting a grenade will blow it up";
        } else if (roomCleared) {
            tutorialText.text = "Good job ( ◞･౪･)";
            door2.SetActive(false);
        } else if (passedRoomOne) {
            tutorialText.text = "You can FIRE with LEFT CLICK.\nYou can also STOP TIME with E.\nLight up all targets to proceed.\nIt takes awhile for time stop to recharge";
        } else {
            tutorialText.text = "WASD\t\tMove\nSpacebar\tJump\nMouse\t\tAim";
        }
    }

    bool checkEnemies() {
        foreach (EnemyAI e in ais) {
            if (e != null) {
                return false;
            }
        }
        return true;
    }
}
