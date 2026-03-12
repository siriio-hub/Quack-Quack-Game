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

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Player 1 Score UI
        if (displayP1 < scoreP1)
        {
            displayP1++;
            scoreTextP1.text = "P1 Score : " + displayP1;
        }

        // Player 2 Score UI
        if (displayP2 < scoreP2)
        {
            displayP2++;
            scoreTextP2.text = "P2 Score : " + displayP2;
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
}