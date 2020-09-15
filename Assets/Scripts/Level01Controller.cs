using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;
    [SerializeField] GameObject PauseMenu;
    int _currentScore;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            IncreaseScore(5);
        }
    }

    public void TogglePause() {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        if (PauseMenu.activeSelf) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Cursor.visible = PauseMenu.activeSelf;
    }
    private void IncreaseScore(int v) {
        _currentScore += v;
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

    public void ExitLevel() {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (_currentScore > highScore) {
            PlayerPrefs.SetInt("HighScore", _currentScore);
        }

        SceneManager.LoadScene("MainMenu");
    }
}
