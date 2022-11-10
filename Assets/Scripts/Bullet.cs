using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform pos;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private int damage = 20;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = GetComponent<Transform>();
        rb.AddForce(pos.right * bulletForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            PhotonNetwork.Destroy(this.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthScript>().ModifyHealth(damage);
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

}
