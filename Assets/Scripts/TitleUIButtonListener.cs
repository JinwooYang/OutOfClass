using UnityEngine;
using System.Collections;

public class TitleUIButtonListener : MonoBehaviour
{
    Animator anim;
    bool showHowToImage = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (showHowToImage)
            {
                anim.Play("HideHowToPlayImage");
            }
        }
    }

    public void StartBtnClick()
    {
        Application.LoadLevel("PlayScene");
    }

    public void HowToPlayButtonClick()
    {
        anim.Play("ShowHowToPlayImage");
        showHowToImage = true;
    }

    public void ExitBtnClick()
    {
        Application.Quit();
    }
}