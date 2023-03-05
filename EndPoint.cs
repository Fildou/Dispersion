using System.Collections;using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerMask;
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(boxCollider.IsTouchingLayers(playerMask))
        {
            Debug.Log(1);
            SceneController.NextLevel();
        }
    }
}
