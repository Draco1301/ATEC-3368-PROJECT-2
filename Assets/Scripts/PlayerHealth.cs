using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] int currentHealth;
    [SerializeField] Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update() {
        if (currentHealth == 0) {
            LevelController.instance.GameOver();
        }
    }

    public int getHealth() {
        return currentHealth;
    }

    public void changeHealth(int num) {
        currentHealth = Mathf.Clamp(currentHealth + num, 0,maxHealth);
        healthBar.fillAmount = currentHealth / (float) maxHealth;
    }

}
