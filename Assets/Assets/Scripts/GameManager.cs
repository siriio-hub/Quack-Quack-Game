using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreTextP1;
    public TextMeshProUGUI scoreTextP2;

    public GameObject[] heartsP1;
    public GameObject[] heartsP2; 
    public int maxHealth = 3;      
    public int currentHealthP1;
    public int currentHealthP2;

    [Header("Player Scores")]
    public int player1Score = 0;
    public int player2Score = 0;

    private int displayP1 = 0;
    private int displayP2 = 0;

    [Header("Final Score")]
    public static int finalP1Score;
    public static int finalP2Score;

    void Start()
    {
        player1Score = 0;
        player2Score = 0;
    }

    void Awake()
    {
        instance = this;

        currentHealthP1 = maxHealth;
        currentHealthP2 = maxHealth;

        UpdateHeartsUI(1);
        UpdateHeartsUI(2);

        player1Score = 0;
        player2Score = 0;
    }

    void Update()
    {
        // Player 1 
        if (displayP1 < player1Score)
        {
            displayP1++;
            scoreTextP1.text = "Score : " + displayP1;
        }

        // Player 2 
        if (displayP2 < player2Score)
        {
            displayP2++;
            scoreTextP2.text = "Score : " + displayP2;
        }
    }

    public void AddScore(int amount, int playerID)
    {
        if (playerID == 1) player1Score += amount;
        else if (playerID == 2) player2Score += amount;
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

    public void CheckGameOver(int loserID)
    {
        finalP1Score = player1Score;
        finalP2Score = player2Score;
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");
    }
}