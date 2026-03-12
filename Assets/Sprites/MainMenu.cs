using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("");  //ใส่ซีนครับผม
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
