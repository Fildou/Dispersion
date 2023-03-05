using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSquare : MonoBehaviour
{
    private Rigidbody2D rbSquare;
    private Rigidbody2D rbPlayer;
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    private float jumpBoost = 5f;
    [SerializeField]
    private LayerMask playerLayer;

    void Awake()
    {
        rbSquare = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isPlayerCol())
        {
            StartCoroutine(JumpDelay());
        }
    }

    private IEnumerator JumpDelay()
    {
        yield return new WaitForFixedUpdate();
        rbPlayer.AddForce(Vector2.up * jumpBoost, ForceMode2D.Impulse);
    }


    private bool isPlayerCol()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, 0.2f, playerLayer);
        return raycast.collider != null;
    }
}
