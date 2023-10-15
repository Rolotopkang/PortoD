using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [Header("基本设置")]
    [Header("移动速度")]
    public float moveSpeed = 5f;
    [Header("跳跃力")]
    public float jumpForce = 10f;
    [Header("摩擦力")]
    public float friction = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject holdingPoint;
    
    [Header("高级设置")]
    [Header("跳跃容忍")]
    public float coyoteTime = 0.1f;
    [Header("空中控制系数")]
    [Range(0,1)]
    public float airControl = 0.4f;
    [Header("Ground加速上线")] 
    public float maxSpeedAccG = 10f;
    [Header("Air加速上线")] 
    public float maxSpeedAccAir = 10f;
    [Header("速度上限")]
    public float maxSpeed = 30f;
    [Header("加速曲线")]
    public AnimationCurve accelerationCurve; // 用于控制加速度的曲线


    [Header("蔚蓝模式")]
    public bool isCelesteMode = false;
    public float wallJumpForce = 5f;
    public float wallSlideSpeed = 2f;
    public float wallJumpDirection = 1f;


    public Box currentHoldingBox;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canJump;
    private bool isWallSliding;
    private float groundCheckRadius = 0.1f;
    private float coyoteTimer;
    private bool isFacingRight = false;
    private float currentAcceleration;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //限速
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        
        if (Mathf.Abs(rb.velocity.x) > maxSpeedAccG && Input.GetAxis("Horizontal")!=0 && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x* maxSpeedAccG, rb.velocity.y);
        }
        
        if (Mathf.Abs(rb.velocity.x) > maxSpeedAccAir && Input.GetAxis("Horizontal")!=0 && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x* maxSpeedAccAir, rb.velocity.y);
        }
    }

    private void Update()
    {
        // 检测角色是否与地面接触
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 计时器用于增加跳跃的容忍时间
        if (isGrounded)
        {
            coyoteTimer = Time.time + coyoteTime;
        }

        // 获取水平输入
        float moveX = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 0.04f;
        }
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Time.timeScale = 1f;
        }
        
        if (moveX > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && isFacingRight)
        {
            Flip();
        }
        
        // 计算加速度，通过曲线来控制
        currentAcceleration = accelerationCurve.Evaluate(Mathf.Abs(moveX));
        
        //移动
        rb.AddForce(Vector2.right * moveX * moveSpeed * (isGrounded? 1 : airControl));
        

        if (moveX == 0 && isGrounded)
        {
            Vector2 velocity = rb.velocity;
            velocity.x -= velocity.x * friction * Time.deltaTime;
            rb.velocity = velocity;
        }

        // 角色跳跃
        if ((isGrounded || Time.time < coyoteTimer) && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // // 检测角色是否在墙上
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * wallJumpDirection, 0.5f, groundLayer);
        // isWallSliding = hit.collider != null && !isGrounded && rb.velocity.y < 0;
        //
        // if (isCelesteMode)
        // {
        //     // 墙上滑动
        //     if (isWallSliding)
        //     {
        //         rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        //     }
        //
        //     // 墙上跳跃
        //     if (isWallSliding && Input.GetButtonDown("Jump"))
        //     {
        //         rb.velocity = new Vector2(-wallJumpDirection * wallJumpForce, jumpForce);
        //     }
        // }
    }

    public void TryPickUp(Box box)
    {
        if (currentHoldingBox)
        {
            return;
        }
        currentHoldingBox = box;
        box.PickUp(true);
        box.transform.SetParent(holdingPoint.transform);
        box.transform.localPosition = Vector3.zero;
    }

    public void TryRlease()
    {
        currentHoldingBox.transform.SetParent(null);
        currentHoldingBox.PickUp(false);
        currentHoldingBox = null;
    }

    private void Flip()
    {
        // 切换人物朝向
        isFacingRight = !isFacingRight;

        // 创建一个新的欧拉角，将y轴旋转值设置为180度或0度，取决于朝向
        Vector3 newRotation = transform.rotation.eulerAngles;
        newRotation.y = isFacingRight ? 180 : 0;
        // 使用插值来平滑翻转过程
        transform.rotation = Quaternion.Euler(newRotation);
    }

    public bool isHolding => currentHoldingBox;
}
