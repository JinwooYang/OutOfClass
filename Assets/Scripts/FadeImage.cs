using UnityEngine;
using System.Collections;

public class FadeImage : MonoBehaviour
{
    Animator animator;
    public bool showFadeImageDone
    {
        get;
        private set;
    }

    public void ShowFadeImage()
    {
        showFadeImageDone = false;
        animator.SetTrigger("ShowFadeImage");
    }

    void EndOfShowFadeImage()
    {
        showFadeImageDone = true;
    }

    void Awake()
    {
        showFadeImageDone = false;
        animator = GetComponent<Animator>();
    }
}
