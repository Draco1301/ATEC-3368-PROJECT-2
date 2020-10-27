using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTimeManager : MonoBehaviour
{
    float _currentTime;
    [SerializeField] EnemyHealth eh;
    [SerializeField] Text timeText;
    [SerializeField] Text winText;
    private void Start() {
        Debug.Log(PlayerPrefs.GetFloat("BestTime"));
    }
    // Update is called once per frame
    void Update()
    {
        if (eh.getHealth() <= 0) {
            float bestTime = PlayerPrefs.GetFloat("BestTime");
            if (_currentTime < bestTime) {
                PlayerPrefs.SetFloat("BestTime", _currentTime);
            }
            winText.text = "You Win";
            GetComponent<LevelController>().GameOver();
            
        } else {
            _currentTime += Time.deltaTime;
            timeText.text = $"{ Mathf.Floor(_currentTime / 60)}:" + (_currentTime % 60).ToString("00.00");
        }
    }
}
