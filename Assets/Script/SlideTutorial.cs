using UnityEngine;
using System.Collections;

public class SlideTutorial : MonoBehaviour
{
    public RectTransform tutorialImage; // ลาก Image มาใส่ตรงนี้

    public Vector2 hiddenPos = new Vector2(-100, -1000); // ตำแหน่งซ่อน (ล่างจอ)
    public Vector2 shownPos = new Vector2(-100, 0);      // ตำแหน่งแสดง (กลางจอ)

    public float speed = 5f;

    void Start()
    {
        // เริ่มต้นให้ซ่อนอยู่
        tutorialImage.anchoredPosition = hiddenPos;
    }

    public void ShowTutorial()
    {
        StopAllCoroutines();
        StartCoroutine(Slide(hiddenPos, shownPos));
    }

    public void HideTutorial()
    {
        StopAllCoroutines();
        StartCoroutine(Slide(shownPos, hiddenPos));
    }

    IEnumerator Slide(Vector2 start, Vector2 end)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * speed;
            tutorialImage.anchoredPosition = Vector2.Lerp(start, end, time);
            yield return null;
        }
    }
}