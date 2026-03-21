using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Exit Game"); // เอาไว้ดูใน Unity

        Application.Quit(); // ออกจากเกมจริง
    }
}