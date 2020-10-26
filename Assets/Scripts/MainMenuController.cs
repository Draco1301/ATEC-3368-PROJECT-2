using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioClip _startingSong;
    [SerializeField] Text _highScoreTextView;

    // Start is called before the first frame update
    void Start()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime");
        if (bestTime < 10) {
            ResetScore();
        }


        _highScoreTextView.text = $"{ Mathf.Floor(bestTime / 60)}:" + (bestTime % 60).ToString("00.00");

        if (_startingSong != null) {
            AudioManager.Instance.PlaySong(_startingSong);
        }    
    }

    public void ResetScore() {
        PlayerPrefs.SetFloat("BestTime", 3600);
        _highScoreTextView.text = $"{ Mathf.Floor(3600 / 60)}:" + (3600 % 60).ToString("00.00");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
