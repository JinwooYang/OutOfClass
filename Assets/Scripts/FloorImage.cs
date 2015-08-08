using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloorImage : MonoBehaviour
{
    public Image fadeImage;

    Animator fadeImageAnim;

    void Awake()
    {
        fadeImageAnim = fadeImage.gameObject.GetComponent<Animator>();
    }

    void TimePause()
    {
        //Time.timeScale = 0f;
    }

    void TimeResume()
    {
        //Time.timeScale = 1f;
    }

    void ScreenFadeIn()
    {
        fadeImageAnim.Play("ScreenFadeIn");
    }

    void ScreenFadeOut()
    {
        fadeImageAnim.Play("ScreenFadeOut");
    }

}
