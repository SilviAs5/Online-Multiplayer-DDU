using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public float moveSpeed, jumpForce;

    public Transform groundPoint;
    public LayerMask ground;

    private bool isGrounded;

    public Animator anim;

    private float inputX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);


        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, ground);
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }
}
