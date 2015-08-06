using UnityEngine;
using System.Collections;

public class Blind : MonoBehaviour 
{
    SpriteRenderer sprRenderer;
    BoxCollider2D boxCollider;

    bool playerInCollider = false;

    void Show()
    {
        Color tempColor = sprRenderer.color;
        if (tempColor.a > 0f)
        {
            tempColor.a -= 0.02f;
            sprRenderer.color = tempColor;
        }
    }

    void Hide()
    {
        Color tempColor = sprRenderer.color;
        if (tempColor.a < 1f)
        {
            tempColor.a += 0.02f;
            sprRenderer.color = tempColor;
        }
    }

	void Awake () 
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
	}
	
    void Update()
    {
        if(playerInCollider)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInCollider = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCollider = false;
        }
    }
}
