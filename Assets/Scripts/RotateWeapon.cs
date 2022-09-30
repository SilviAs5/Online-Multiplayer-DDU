using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    public Rigidbody2D rb;
    private Camera cam;

    Vector2 mousePos;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }


    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = angle;
    }
}
