using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSquare : MonoBehaviour
{
    [SerializeField]
    private GameObject purpleSquare;
    [SerializeField]
    private GameObject orangeSquare;
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name.Contains("BlueSquare"))
        {
            if (player.GetComponent<Mechanics>().canCombineSquares == false) return;
            Instantiate(purpleSquare, this.transform.position, Quaternion.identity);
            player.GetComponent<Mechanics>().isHolding = false;
            Destroy(collision.collider.gameObject);
            Destroy(this.gameObject);
        }
        if(collision.collider.name.Contains("YellowSquare"))
        {
            if (player.GetComponent<Mechanics>().canCombineSquares == false) return;
            Instantiate(orangeSquare, this.transform.position, Quaternion.identity);
            player.GetComponent<Mechanics>().isHolding = false;
            Destroy(collision.collider.gameObject);
            Destroy(this.gameObject);
        }
        if(collision.collider.name.Equals("Player"))
        {
            SceneController.Restart();
        }
    }
}