using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI p1CoinText;
    public TextMeshProUGUI p2CoinText;
    public GameObject fireworksEffect;

    void Start()
    {
        Debug.Log("คะแนนที่ได้รับจาก GameManager - P1: " + GameManager.finalP1Score + " P2: " + GameManager.finalP2Score);

        p1CoinText.text = "= " + GameManager.finalP1Score;
        p2CoinText.text = "= " + GameManager.finalP2Score;

        if (fireworksEffect != null)
        {
            Instantiate(fireworksEffect, Vector3.zero, Quaternion.identity);
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame"); // ชื่อฉากเล่นเกมของคุณ
    }
}