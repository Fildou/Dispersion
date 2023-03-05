using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSquare : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float maxDistance = 5f;
    [SerializeField]
    private float elSpeed = 2f;
    [SerializeField]
    private GameObject greenSquare;
    private Vector3 origPos = Vector3.zero;
    private GameObject player;



    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if(rb.bodyType != RigidbodyType2D.Kinematic)
        {
            if(isGrounded() && isPlayerCol())
            {
                origPos = rb.transform.position;
            }

            if(isPlayerCol())
            {
                rb.bodyType = RigidbodyType2D.Static;
                if(transform.position.y < origPos.y + maxDistance)
                {
                    rb.transform.position += new Vector3(0, elSpeed);
                }
            }
            else
            {
                if(transform.position.y >= origPos.y)
                {
                    rb.transform.position -= new Vector3(0, elSpeed);
                }
            }
        }
    }

    private bool isPlayerCol()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, .2f, playerLayer);
        return raycast.collider != null;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        return raycast.collider != null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name.Contains("YellowSquare"))
        {
            if (player.GetComponent<Mechanics>().canCombineSquares == false) return;
            Instantiate(greenSquare, this.transform.position, Quaternion.identity);
            player.GetComponent<Mechanics>().isHolding = false;
            Destroy(collision.collider.gameObject);
            Destroy(this.gameObject);
        }
    }
}