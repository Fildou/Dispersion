using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform startPoint;
    private BoxCollider2D resetZone;
    private GameObject player;
    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField] 
    private GameObject pauseMenu;
    void Awake()
    {
        startPoint = this.transform.GetChild(1);
        resetZone = this.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(resetZone.IsTouchingLayers(playerLayer))
        {
            resetPlayer();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            resetPlayer();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void resetPlayer()
    {
        SceneController.Restart();
    }


}
