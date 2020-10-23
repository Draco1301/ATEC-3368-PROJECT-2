using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;
    [SerializeField] GameObject PauseMenu;
    int _currentScore;

    [SerializeField] GameObject GameOverPanel;
    bool gameOver;

    public static LevelController instance;
    public bool isPaused = true;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            IncreaseScore(5);
        }

        if (isPaused) {
            Time.timeScale = 0;
        } else { 
            Time.timeScale = 1;
        }
    }

    public void TogglePause() {
        if (gameOver) {
            return;
        }

        PauseMenu.SetActive(!PauseMenu.activeSelf);
        if (PauseMenu.activeSelf) {
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            isPaused = false;
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

    public void RestartLevel() {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (_currentScore > highScore) {
            PlayerPrefs.SetInt("HighScore", _currentScore);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOver = true;
        isPaused = true;

        GameOverPanel.SetActive(true);
    }
}
