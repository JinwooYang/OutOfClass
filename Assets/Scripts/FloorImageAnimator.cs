using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloorImageAnimator : MonoBehaviour
{
    public Animator[] floorImageAnim = new Animator[4];
    public Animator fadeImage;

    public Animator curFloorImageAnim
    {
        get;
        private set;
    }

    public void ShowFloorImage(int floorNum)
    {
        curFloorImageAnim = floorImageAnim[floorNum - 1];

        for (int animIdx = 0; animIdx < floorImageAnim.Length; ++animIdx)
        {
            floorImageAnim[animIdx].Play("Wait");
        }

        curFloorImageAnim.Play("ShowFloorImage");
    }
}
