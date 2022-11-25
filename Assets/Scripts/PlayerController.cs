using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    #region Variables
    //SerializeFields
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;
    [SerializeField] private GameObject ui;

    private CapsuleCollider2D cc;

    public Transform groundPoint;
    public Transform firePoint;

    public LayerMask ground;
    public GameObject playerSprite;
    public GameObject bullet;

    public Animator anim;

    //Booleans
    private bool isGrounded;
    private bool isOnSlope;
    private bool isJumping;
    private bool canjump;

    //floats
    public float moveSpeed, jumpForce;
    public float groundPointSize;

    private float inputX;
    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private float slopeSideAngle;

    //Vectors
    private Vector2 pointerInput;
    private Vector3 mousePos;
    private Vector2 playerScale;
    private Vector2 colliderSize;
    private Vector2 slopeNormalPerp;

    private WeaponParent weaponParent;
    private FlipSprite flipSprite;

    PhotonView view;
    #endregion

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        playerScale = playerSprite.transform.localScale;
        cc = GetComponent<CapsuleCollider2D>();
        colliderSize = cc.size;
        if (!view.IsMine)
        {
            Destroy(ui);
        }
        weaponParent = GetComponentInChildren<WeaponParent>();
        flipSprite = GetComponentInChildren<FlipSprite>();
    }

    private void FixedUpdate()
    {
        Multiplayer();
        if (view.IsMine)
        {
            Move();        
            Mouse();
            CheckGround();
            SlopeCheck();
        }
    }
    private void Multiplayer()
    {
        if (!view.IsMine)
        {
            rb.sharedMaterial = fullFriction;
        }
    }

    #region Slope
    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, ground);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, ground);

        if (slopeHitFront)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, ground);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (isOnSlope && inputX == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }
    #endregion

    #region Move
    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }

    private void Move()
    {
        //Move the player
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(inputX * moveSpeed));
        if (isGrounded && !isOnSlope && !isJumping)
        {
            rb.velocity.Set(moveSpeed * inputX, 0.0f);
        }
        else if (isGrounded && isOnSlope && !isJumping)
        {
            rb.velocity.Set(moveSpeed * slopeNormalPerp.x * -inputX, moveSpeed * slopeNormalPerp.y * -inputX);
        }
        else if (!isGrounded)
        {
            rb.velocity.Set(moveSpeed * inputX, rb.velocity.y);
        }
    }
    #endregion

    #region Jump
    public void Jump(InputAction.CallbackContext context)
    {
            if (context.performed && canjump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
                canjump = false;
            }
            if (context.canceled && !canjump) //if jump button is released in the air
            {
                rb.gravityScale = 10;
            }
    }

    private void CheckGround()
    {
        //Check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundPointSize, ground);
        if (isGrounded) //when the player lands on the ground again
        {
            rb.gravityScale = 5;
        }
        //if (!isGrounded)
        //{
        //    yield return new WaitForSeconds(0.2f);
        //    canjump = false;
        //}
        if (rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }
        if (isGrounded && !isJumping)
        {
            canjump = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundPoint.position, groundPointSize);
    }

    #endregion

    #region Mouse
    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
    }

    private void Mouse()
    {
        //Mouse position
        pointerInput = Camera.main.ScreenToWorldPoint(mousePos);
        //Mouse position is sent to the WeaponParent script and FlipSprite Script
        weaponParent.PointerPosition = pointerInput;
        flipSprite.PointerPosition = pointerInput;
    }
    #endregion

    #region Shooting
    public void Fire(InputAction.CallbackContext context)
    {   
        if (context.performed && view.IsMine)
        {
            //Instantiate(bullet, new Vector2(firePoint.transform.position.x, firePoint.transform.position.y), firePoint.transform.rotation);
            PhotonNetwork.Instantiate(bullet.name, firePoint.transform.position, firePoint.transform.rotation);
        }
    }
    #endregion
}