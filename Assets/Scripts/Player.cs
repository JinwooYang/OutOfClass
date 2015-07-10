using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public float walkSpeed, runSpeed;

    Animator animator;
    Rigidbody2D rb;

    GameObject idleImg;
    AudioSource walkSound, runSound;

    enum PlayerStatus {IDLE, WALK, RUN};
    PlayerStatus status;

	// Use this for initialization
	void Start () 
    {
        status = PlayerStatus.IDLE;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        walkSound = transform.FindChild("Player Walk Sound").GetComponent<AudioSource>();
        runSound = transform.FindChild("Player Run Sound").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Move();
	}


    void Move()
    {
        if(ArrowKeyIsDown())
        {
            status = PlayerStatus.WALK;
            if(Input.GetKey(KeyCode.LeftShift))
            {
                status = PlayerStatus.RUN;
            }
        }
        else
        {
            status = PlayerStatus.IDLE;
        }

        float distX = Input.GetAxis("Horizontal");
        float distY = Input.GetAxis("Vertical");

        float radAngle = Mathf.Atan2(distY, distX);

        switch (status)
        {
            case PlayerStatus.IDLE:
                runSound.Stop();
                walkSound.Stop();
                animator.SetTrigger("player_idle");
                animator.speed = 1.0f;

                rb.velocity = Vector2.zero;
                break;

            case PlayerStatus.WALK:
                runSound.Stop();
                if(!walkSound.isPlaying)
                {
                    walkSound.Play();
                }
                animator.SetTrigger("player_walk");
                animator.speed = 1.0f;

                rb.velocity = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle)) * walkSpeed;
                transform.rotation = Quaternion.Euler(0f, 0f, radAngle * Mathf.Rad2Deg + 90f);
                break;

            case PlayerStatus.RUN:
                walkSound.Stop();
                if(!runSound.isPlaying)
                {
                    runSound.Play();
                }
                animator.SetTrigger("player_walk");
                animator.speed = 2.0f;

                rb.velocity = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle)) * runSpeed;
                transform.rotation = Quaternion.Euler(0f, 0f, radAngle * Mathf.Rad2Deg + 90f);
                break;

            default:
                Debug.Assert(false, "Unknown Value!");
                break;
        }
    }


    bool ArrowKeyIsDown()
    {
        if(Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
