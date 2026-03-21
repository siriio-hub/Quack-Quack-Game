using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public void GoHome()
    {
        SceneManager.LoadScene("StartGame"); // ชื่อ Scene หน้าแรก
    }
}