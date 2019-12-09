using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;

    public GameObject flipper;

    private Player player;
    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        if (Player != null)
        {
            player = Player.GetComponent<Player>();
        }
        if (Player == null)
        {
            Debug.Log("Cannot find 'Player' script");
        }
        facingRight = true;
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers and pingpong the movement.
        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));
    }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        if (facingRight == false && hozMovement > 0)
        {
            //Player.Flip(flipper);
        }
        else if (facingRight == true && hozMovement < 0)
        {
            //Player.Flip(flipper);
        }
    }
}