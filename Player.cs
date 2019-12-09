using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;
    public int OneValue;

    public AudioClip soundClipOne;
    public AudioClip soundClipTwo;
    public AudioSource musicSource;

    public GameObject flipper;

    private bool facingRight;

    private bool noContact;

    private GameController GameController;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        noContact = false;
        facingRight = true;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            GameController = gameControllerObject.GetComponent<GameController>();
        }
        if (GameController == null)
        {
            Debug.Log("Cannot find 'gameController' script");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.4f, 0.5f, 1f);
            noContact = true;
        }
        if (Input.GetKeyUp(KeyCode.S) && (noContact == true))
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0.7f, 0.97f, 1f);
            noContact = false;
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        //Debug.Log(noContact);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        // float vertMovement = Input.GetAxis("Vertical");
        rb2d.AddForce(new Vector2(hozMovement * speed, 0));
        if (facingRight == false && hozMovement > 0)
        {
            Flip(flipper);
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip(flipper);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (noContact == false)
        {
            if (collision.collider.tag == "Coin")
            {
                GameController.AddScore(OneValue);
                Destroy(collision.collider.gameObject);
            }
            else if (collision.collider.tag == "enemy")
            {
                //Subtract one to the current value of our lives variable.
                musicSource.clip = soundClipTwo;
                musicSource.Play();
                GameController.SubtractLives(OneValue);
                //... then DESTROY the other object we just collided
                Destroy(collision.collider.gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.collider.tag == "ground") && (noContact == false))
        {
            anim.SetBool("inAir", false);
            if (Input.GetKey(KeyCode.W))
            {
                rb2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                musicSource.clip = soundClipOne;
                musicSource.Play();
                anim.SetBool("inAir", true);
            }
        }
    }
    public void Flip(GameObject newflipper)
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}