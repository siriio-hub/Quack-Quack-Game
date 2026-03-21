using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    public void GoToSetting()
    {
        SceneManager.LoadScene("SettingGame"); // ใส่ชื่อ Scene Setting ของเธอ
    }
}