using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public float moveSpeed, jumpForce;

    public Transform groundPoint;
    public LayerMask ground;

    private bool isGrounded;

    public Animator anim;

    private float inputX;

    private Vector2 pointerInput;
    private Vector3 mousePos;

    private WeaponParent weaponParent;
    private FlipSprite flipSprite;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Awake()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
        flipSprite = GetComponentInChildren<FlipSprite>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            //Move the player
            rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

            //Check if player is on the ground
            isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, ground);

            //Mouse position
            pointerInput = Camera.main.ScreenToWorldPoint(mousePos);
            //Mouse position is sent to the WeaponParent script and FlipSprite Script
            weaponParent.PointerPosition = pointerInput;
            flipSprite.PointerPosition = pointerInput;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }

    #region Jump

    public void Jump(InputAction.CallbackContext context)
    {
            if (context.performed && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            if (context.canceled && isGrounded == false) //if jump button is released in the air
            {
                rb.gravityScale = 10;
            }
            if (isGrounded) //when the player lands on the ground again
            {
                rb.gravityScale = 5;
            }
    }
    
    #endregion

    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
    }

    public void Shoot(InputAction.CallbackContext context)
    {

    }


}
 