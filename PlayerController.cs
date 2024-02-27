using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;

    private float lastMoveX= 0f;
    private float lastMoveY= 0f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();

        float inputX = movement.x;
        float inputY = movement.y;

        myAnimator.SetFloat("x", inputX);
        myAnimator.SetFloat("y", inputY);

    //giữ nguyên giá trị của y và z, thay đổi x

        //di chuyển sang phải
        if (inputX > 0)
        {
            transform.localScale = new Vector3(-1,1,1); 
        }
        //di chuyển sang trái
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }

        float horizontal= Input.GetAxisRaw("Horizontal");
        float vertical= Input.GetAxisRaw("Vertical");

        if(horizontal!= 0|| vertical!= 0)
        {
            //Player đang di chuyển
            myAnimator.SetBool("IsMoving", true);
            lastMoveX= horizontal;
            lastMoveY= vertical;
        }
        else
        {
            //Player đã dừng lại
            myAnimator.SetBool("IsMoving", false);
            myAnimator.SetFloat("lastMoveX", lastMoveX);
            myAnimator.SetFloat("lastMoveY", lastMoveY);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));

        if (movement != Vector2.zero)
        {
            myAnimator.SetBool("IsMoving", true);
        }
        else
        {
            myAnimator.SetBool("IsMoving", false);
        }
    }
}
