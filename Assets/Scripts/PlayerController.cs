using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D Player;
    public float jumpPower = 8f;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isOnGround;
    private float inX = 0f;
    public bool canSwapLeft = false;
    public bool canSwapRight = false;
    //public TextMeshProUGUI Score;
    //public TextMeshProUGUI Timer;
    //public TextMeshProUGUI Swaps;
    //public TextMeshProUGUI AttemptsText;
    private static int attemptNumber = 1;
    public double baseScore = 100;
    public int generalTime = 60;
    private double swapTime = 0;

    private void Start()
    {
        //AttemptsText.text = "Attempts: " + attemptNumber;
    }

    // Update is called once per frame
    void Update()
    {
        inX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if(Time.timeScale <= 0) { return; }
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (Input.GetButton("Jump") && isOnGround)
        {
            Player.velocity = new Vector2(Player.velocity.x, jumpPower);
        }


        Vector2 moveX = new Vector2(inX * movementSpeed, Player.velocity.y);
        Player.velocity = moveX;
    }


    public void Reset()
    {
        attemptNumber += 1;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void calculateScore()
    {
        //Calculate score
        //int TimeTaken = Int32.Parse(Timer.text.Substring(6).Trim());
        //int SwapsUsed = Int32.Parse(Swaps.text.Substring(7).Trim());
        //Calculate Score
        //double totalScore = baseScore * generalTime / (TimeTaken + (5 * SwapsUsed));
        //Score.text = "Score: " + Math.Round(totalScore);
        //Reset Attempts
        attemptNumber = 1;
    }
}
