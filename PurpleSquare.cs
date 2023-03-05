using UnityEngine;

public class PurpleSquare : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private LayerMask wallLayer;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            Vector2 stickPosition = contact.point + contact.normal * 0.5f;
            transform.position = stickPosition;
        }
    }
}
