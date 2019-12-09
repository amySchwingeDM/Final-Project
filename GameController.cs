using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public AudioClip musicClipOne;
    public AudioClip soundClipOne;
    public AudioSource musicSource;
    public Text endText;
    public Text score;
    public Text lives;
    public Text tutorialtext;
    public GameObject player;
    private Rigidbody2D rb2d;

    private int scoreValue = 0;
    private int livesValue = 3;
    private bool endGame;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            rb2d = player.GetComponent<Rigidbody2D>();
        }
        if (player == null)
        {
            Debug.Log("Cannot find 'player' script");
        }
        endGame = false;
        musicSource.clip = musicClipOne;
        musicSource.Play();
        endText.text = "";
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        StartCoroutine(reloadTimer(120));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            tutorialtext.text = "";
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    IEnumerator reloadTimer(float reloadTimeInSeconds)
    {
        float counter = 0;

        while ((counter < reloadTimeInSeconds) && (endGame == false))
        {
            counter += Time.deltaTime;
            int intCounter = (int)counter;
            endText.text = "Timer: " + intCounter.ToString();
            yield return null;
        }
        if (counter >= reloadTimeInSeconds)
        {
            endText.text = "Time's up! Game created by Amy Schwinge";
        }
        player.SetActive(false);
    }
    public void AddScore(int newOneValue)
    {
        scoreValue += newOneValue;
        score.text = "Score: " + scoreValue.ToString();
        EndGame();
    }
    public void SubtractLives(int newOneValue)
    {
        livesValue -= newOneValue;
        lives.text = "Lives: " + livesValue.ToString();
        EndGame();
    }
    void EndGame()
    {
        if (scoreValue == 4)
        {
            player.transform.position = new Vector2(100f, 1.5f);
            rb2d.velocity = new Vector2(0f, 0f);
            livesValue = 3;
            lives.text = "Lives: " + livesValue.ToString();
        }
        else if (scoreValue >= 8)
        {
            endGame = true;
            endText.color = new Color(255, 255, 0, 255);
            endText.text = "You win! Game created by Amy Schwinge";
        }
        else if (livesValue == 0)
        {
            endGame = true;
            endText.color = new Color(255, 0, 0, 255);
            endText.text = "You lose! Game created by Amy Schwinge";
            player.SetActive(false);
        }
    }  
}