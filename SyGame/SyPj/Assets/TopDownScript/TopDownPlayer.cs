using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayer : MonoBehaviour
{
    public static int speed =5;
    public GameManagerScript gameManager;
    float h;
    float v;
    Rigidbody2D rigid;
    bool isHorizonMove;
    GameObject scanObject;
    Vector2 dirVec;
    Animator anim;

    PlayerActionsExample playerInput;

    bool isJunmping;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = new PlayerActionsExample();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {


        if (playerInput == null)
            playerInput = new PlayerActionsExample();
        Vector2 movement = playerInput.Player.Move.ReadValue<Vector2>();

        h = gameManager.isAction ? 0 : movement.x;
        v = gameManager.isAction ? 0 : movement.y;

        if (Mathf.Abs(h) == 1)
        {
            isHorizonMove = true;
        }
        else
        {
            isHorizonMove = false;
        }
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetInteger("vAxisRaw", (int)v);
        }


        //레이 관련
        if (v == 1)
        {
            dirVec = Vector2.up;
        }
        else if (v == -1)
        {
            dirVec = Vector2.down;
        }
        else if (h == -1)
        {
            dirVec = Vector2.left;
        }
        else if (h == 1)
        {
            dirVec = Vector2.right;
        }



        /*     h = Input.GetAxisRaw("Horizontal");
             v = Input.GetAxisRaw("Vertical");


             bool hDown = Input.GetButtonDown("Horizontal");
             bool vDown = Input.GetButtonDown("Vertical");
             bool hUp = Input.GetButtonUp("Horizontal");
             bool vUp = Input.GetButtonUp("Vertical");


             if (hDown || vUp)
                 isHorizonMove = true;
             else if (vDown || hUp)

                 isHorizonMove = false;*/

        // Debug.Log(playerInput.Player.Jump.triggered + " : " + playerInput.Player.Jump.IsPressed());
        bool jumpPress = playerInput.Player.Jump.triggered;//|| playerInput.Player.Jump.IsPressed();
        if (jumpPress && scanObject != null && jumpPress )
        {
          //  isJunmping = jumpPress;
            gameManager.Action(scanObject);
        }
    }
    void FixedUpdate()
    {
      //   Debug.Log("x" + h + " y" + v);
        Vector2 moveVec = isHorizonMove ? new Vector2(h, v) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

        Debug.DrawLine(rigid.position, dirVec * 0.7f, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("TopObject"));
        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
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
