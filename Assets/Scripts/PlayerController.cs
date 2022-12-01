using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.PlayerLoop.PreLateUpdate;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public int playerSide;
    public Rigidbody2D Player;
    public float jumpPower = 8f;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isOnGround;
    private float inX = 0f;
    GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        inX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if(Time.timeScale <= 0) { return; }
        if (gameManager.getSideID() != playerSide) { return; }
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (Input.GetButton("Jump") && isOnGround)
        {
            Player.velocity = new Vector2(Player.velocity.x, jumpPower);
        }

        Vector2 moveX = new Vector2(inX * movementSpeed, Player.velocity.y);
        Player.velocity = moveX;
    }
}
