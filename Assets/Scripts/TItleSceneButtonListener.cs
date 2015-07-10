using UnityEngine;
using System.Collections;

public class TItleSceneButtonListener : MonoBehaviour 
{
    public HowToPlayImage howToPlayImage;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}


    public void StartBtnClick()
    {
        Application.LoadLevel("PlayScene");
    }

    public void HowToPlayButtonClick()
    {
        Instantiate(howToPlayImage);
    }

    public void ExitBtnClick()
    {
        Application.Quit();
    }
}
