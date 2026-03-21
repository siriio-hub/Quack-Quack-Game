using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GoToSetting()
    {
        SceneManager.LoadScene("SettingGame");
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("StartGame");
    }
}