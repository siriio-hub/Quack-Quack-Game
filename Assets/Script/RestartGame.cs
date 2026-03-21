using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        GameManager.finalP1Score = 0;
        GameManager.finalP2Score = 0;
        
        SceneManager.LoadScene("GamePlay");
    }
}