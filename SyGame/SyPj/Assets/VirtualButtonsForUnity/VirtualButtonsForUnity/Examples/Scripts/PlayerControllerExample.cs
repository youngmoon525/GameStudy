using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class PlayerControllerExample : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    protected PlayerActionsExample playerInput;
    private Rigidbody2D playerVelocity;
    private bool groundedPlayer;
    private void Awake()
    {
        playerInput = new PlayerActionsExample();
        playerVelocity =  GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        

        Vector2 movement = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        if(playerVelocity.velocity.y > 1)
        {
            groundedPlayer = true;
        }
        else
        {
            groundedPlayer = false;
        }

        if (move != Vector3.zero)
        {
            if (move.x > 0)//Right
                playerVelocity.velocity = new Vector2(4, playerVelocity.velocity.y);
            else if (move.x < 0)//Left
                playerVelocity.velocity = new Vector2(4 * (-1), playerVelocity.velocity.y);
            //gameObject.transform.forward = move;
        }

       //  bool jumpPress = playerInput.Player.Jump.IsPressed();
        bool jumpPress = playerInput.Player.Jump.triggered || playerInput.Player.Jump.IsPressed();
        if (jumpPress && !groundedPlayer)
        {
            playerVelocity.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
           // animator.SetBool("isJumping", true);
           // playSound(audioJump);
            //playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

       // playerVelocity.y += gravityValue * Time.deltaTime;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

}
