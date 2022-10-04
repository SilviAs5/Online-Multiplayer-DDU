using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateWeapon : MonoBehaviour
{
    Rigidbody2D rb;
    Camera cam;

    Vector2 mousePos;

    PhotonView view;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        view = GetComponentInParent<PhotonView>();
    }


    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            Vector2 lookDir = mousePos - rb.position;

             float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

             rb.rotation = angle;
        }
        
    }
}
