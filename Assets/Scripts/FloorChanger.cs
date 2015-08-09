using UnityEngine;
using System.Collections;

public class FloorChanger : MonoBehaviour
{
    public FadeImage fadeImage;

	public void ChangeFloor (Stair curStair)
    {
        StartCoroutine(ChangeFloorCoroutine(curStair));
	}

    IEnumerator ChangeFloorCoroutine(Stair curStair)
    {
        Time.timeScale = 0f;
        fadeImage.ShowFadeImage();

        while (!fadeImage.showFadeImageDone)
        {
            yield return null;
        }

        Time.timeScale = 1f;
        curStair.GoToExitStair();
    }
}
