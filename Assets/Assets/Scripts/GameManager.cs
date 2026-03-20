using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int scoreP1 = 0;
    public int scoreP2 = 0;

    int displayP1 = 0;
    int displayP2 = 0;

    public TextMeshProUGUI scoreTextP1;
    public TextMeshProUGUI scoreTextP2;

    public GameObject[] heartsP1;
    public GameObject[] heartsP2; 
    public int maxHealth = 3;      
    public int currentHealthP1;
    public int currentHealthP2;

    void Awake()
    {
        instance = this;

        currentHealthP1 = maxHealth;
        currentHealthP2 = maxHealth;

        UpdateHeartsUI(1);
        UpdateHeartsUI(2);
    }

    void Update()
    {
        // Player 1 Score UI
        if (displayP1 < scoreP1)
        {
            displayP1++;
            scoreTextP1.text = "Score : " + displayP1;
        }

        // Player 2 Score UI
        if (displayP2 < scoreP2)
        {
            displayP2++;
            scoreTextP2.text = "Score : " + displayP2;
        }
    }

    public void AddScore(int amount, int playerID)
    {
        if (playerID == 1)
        {
            scoreP1 += amount;
        }
        else if (playerID == 2)
        {
            scoreP2 += amount;
        }
    }

    public void TakeDamage(int amount, int playerID)
    {
        if (playerID == 1)
        {
            currentHealthP1 -= amount;
            if (currentHealthP1 < 0) currentHealthP1 = 0;
            UpdateHeartsUI(1);
        }
        else if (playerID == 2)
        {
            currentHealthP2 -= amount;
            if (currentHealthP2 < 0) currentHealthP2 = 0;
            UpdateHeartsUI(2);
        }
    }
    public void AddHealth(int amount, int playerID)
    {
        if (playerID == 1)
        {
            currentHealthP1 = Mathf.Min(currentHealthP1 + amount, maxHealth);
            UpdateHeartsUI(1);
        }
        else
        {
            currentHealthP2 = Mathf.Min(currentHealthP2 + amount, maxHealth);
            UpdateHeartsUI(2);
        }
    }

    void UpdateHeartsUI(int playerID)
    {
        if (playerID == 1)
        {
            for (int i = 0; i < heartsP1.Length; i++)
            {
                heartsP1[i].SetActive(i < currentHealthP1);
            }
        }
        else if (playerID == 2)
        {
            for (int i = 0; i < heartsP2.Length; i++)
            {
                heartsP2[i].SetActive(i < currentHealthP2);
            }
        }
    }
    public int GetCurrentHealth(int playerID)
    {
        if (playerID == 1) return currentHealthP1;
        else return currentHealthP2;
    }
}