using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private Vector2 pos;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        pos = GetComponent<Transform>().position;
    }

    private void Update()
    {
        
    }
}
