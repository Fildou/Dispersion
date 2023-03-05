using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanics : MonoBehaviour
{
    public Animator animator;
    public Transform grabDetect;
    public Transform holdObject;
    public float rayDist;
    public float throwForce;
    public bool isHolding = false;
    public bool canCombineSquares = true;

    public GameObject blueSquarePrefab;
    public GameObject yellowSquarePrefab;
    public GameObject redSquarePrefab;
    public GameObject purpleSquarePrefab;
    public GameObject greenSquarePrefab;
    public GameObject orangeSquarePrefab;
    private GameObject blueGauntlet;
    [SerializeField]
    private bool blueIsEnabled = false;
    [SerializeField]
    private bool yellowIsEnabled = false;

    void Awake()
    {
        if(SceneController.GetScene() > 2)
        {
            blueIsEnabled = true;
            animator.SetBool("Glove1", blueIsEnabled);
        }
        if(SceneController.GetScene() > 6)
        {
            yellowIsEnabled = true;
            animator.SetBool("Glove2", yellowIsEnabled);
        }
        
    }

    void Update()
    {
        // check if object is in range to grab
        if (!isHolding && Input.GetKeyDown(KeyCode.LeftShift))
        {
            RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
            if (grabCheck.collider != null && grabCheck.collider.tag == "Object" && !grabCheck.collider.name.Contains("RedSquare"))
            {
                grabCheck.collider.gameObject.transform.parent = holdObject;
                grabCheck.collider.gameObject.transform.position = holdObject.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Reset the movement of the grabbed object
                isHolding = true;
            }
        }
        // check if object is in range to grab
        else if (!isHolding && Input.GetKeyDown(KeyCode.K) && blueIsEnabled)
        {
            RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
            if (grabCheck.collider != null && grabCheck.collider.tag == "Object")
            {
                // replace square based on enabled gauntlet
                if (Input.GetKeyDown(KeyCode.K) && grabCheck.collider.GetComponent<PurpleSquare>())
                {
                    DivideSquare(grabCheck, purpleSquarePrefab, blueSquarePrefab, redSquarePrefab);
                }
                else if (Input.GetKeyDown(KeyCode.K) && grabCheck.collider.GetComponent<GreenSquare>())
                {
                    DivideSquare(grabCheck, greenSquarePrefab, blueSquarePrefab, yellowSquarePrefab);
                }
            }
        }
        else if (!isHolding && Input.GetKeyDown(KeyCode.J) && yellowIsEnabled)
        {
            RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
            if (grabCheck.collider != null && grabCheck.collider.tag == "Object")
            {
                // replace square based on enabled gauntlet
                if (grabCheck.collider.GetComponent<OrangeSquare>())
                {
                    DivideSquare(grabCheck, orangeSquarePrefab, yellowSquarePrefab, redSquarePrefab);
                }
                else if (grabCheck.collider.GetComponent<GreenSquare>())
                {
                    DivideSquare(grabCheck, greenSquarePrefab, yellowSquarePrefab, blueSquarePrefab);
                }
            }
        }
        // drop held object
        else if (isHolding && Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameObject heldObject = holdObject.GetChild(0).gameObject;
            heldObject.transform.parent = null;
            Rigidbody2D rb = heldObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            isHolding = false;
        }

        // throw held object
        else if (isHolding && Input.GetKeyDown(KeyCode.E))
        {
            GameObject heldObject = holdObject.GetChild(0).gameObject;
            heldObject.transform.parent = null;
            Rigidbody2D rb = heldObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            Vector2 throwDirection = new Vector2(transform.localScale.x > 0 ? 1 : -1, 0.3f);
            rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            isHolding = false;
            animator.SetTrigger("Throw");
        }
        animator.SetBool("Hold", isHolding);
    }

    // Divide square with gauntlet
    void DivideSquare(RaycastHit2D grabCheck, GameObject removeSquarePrefab, GameObject addSquarePrefab, GameObject DivideSquarePrefab)
    {
        GameObject removeSquare = grabCheck.collider.gameObject;
        if (removeSquare != null)
        {
            // create new square and replace old square
            var newAddSquare = Instantiate(addSquarePrefab, grabCheck.collider.transform.position, Quaternion.identity, null);
            Destroy(grabCheck.collider.gameObject);
            var newDivideSquare = Instantiate(DivideSquarePrefab, grabCheck.collider.transform.position, Quaternion.identity, null);
            Destroy(removeSquare);

            // move new square to player's hand
            newAddSquare.transform.parent = holdObject;
            newAddSquare.transform.position = holdObject.position;
            newAddSquare.GetComponent<Rigidbody2D>().isKinematic = true;
            newAddSquare.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isHolding = true;
            StartCoroutine(ResetCanCombine());

            // apply a small force to push player away from the square
            Rigidbody2D playerRb = GetComponent<Rigidbody2D>();
            Rigidbody2D divideSquareRb = newDivideSquare.GetComponent<Rigidbody2D>();
            Vector2 direction = (playerRb.transform.position - divideSquareRb.transform.position).normalized;
            playerRb.AddForce(direction * 4, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning(removeSquarePrefab.name + " component not found on grabbed object");
        }
    }

    // delay
    private IEnumerator ResetCanCombine()
    {
        canCombineSquares = false;
        yield return new WaitForSecondsRealtime(4);
        canCombineSquares = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!blueIsEnabled && collision.collider.tag == "Gauntlet_blue")
        {
            blueIsEnabled = true;
            animator.SetBool("Glove1", blueIsEnabled);
            Destroy(collision.collider.gameObject);
        }
        else if (!yellowIsEnabled && collision.collider.tag == "Gauntlet_yellow")
        {
            yellowIsEnabled = true;
            animator.SetBool("Glove2", yellowIsEnabled);
            Destroy(collision.collider.gameObject);
        }

    }
}