using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSquare : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField]
    private LayerMask playerLayer;
    private GameObject player;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 2f);
    private GameObject[] teleports;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(isPlayerCol() && canTeleport() && Input.GetKeyDown(KeyCode.S))
        {
            player.transform.position = GetOtherTeleport().transform.position + offset;
        }
    }

    private bool canTeleport()
    {
        teleports = GameObject.FindGameObjectsWithTag("Teleport");
        if(teleports.Length > 2)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private GameObject GetOtherTeleport()
    {
        GameObject parent = null;

        foreach(GameObject teleport in teleports)
        {
            if(teleport.transform.parent.position != this.transform.position)
            {
                parent = teleport.transform.parent.gameObject;
            }
        }
        return parent;
    }

    private bool isPlayerCol()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, .2f, playerLayer);
        return raycast.collider != null;
    }
}