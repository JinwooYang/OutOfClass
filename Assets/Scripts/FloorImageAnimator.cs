using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloorImageAnimator : MonoBehaviour
{
    public Animator[] floorImageAnim = new Animator[4];
    public Animator fadeImage;

    public void ShowFloorImage(int floorNum)
    {
        for (int animIdx = 0; animIdx < floorImageAnim.Length; ++animIdx)
        {
            floorImageAnim[animIdx].Play("Wait");
        }

        floorImageAnim[floorNum - 1].Play("ShowFloorImage");
    }

    //void Awake()
    //{
    //}

}
