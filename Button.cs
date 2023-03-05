using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private GameObject gate;
    [SerializeField]
    private LayerMask squareLayer;
    private BoxCollider2D boxCollider;
    private Animator gateAnimator;

    void Awake()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
        gateAnimator = gate.GetComponent<Animator>();
    }

    void Update()
    {
        if(boxCollider.IsTouchingLayers(squareLayer))
        {
            Debug.Log("Door opened");
            gateAnimator.SetBool("Open", true);
        }
    }
}
