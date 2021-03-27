using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    //Game Object Ref
    [Header("GameObjects Reference")]
    [SerializeField] GameObject playerBody;
    [SerializeField] Transform objPoint;
    [Space]
    [SerializeField] GameManager gm;
    [Space]

    [Header("Player Variables")]
    [SerializeField] float jumpForce = 1f;
    [SerializeField] float moveSpeed = 2;

    //Strings
    private readonly string verticalAxis = "Vertical";
    private readonly string horizontalAxis = "Horizontal";

    private readonly string puzzle1 = "puzzle1";
    private readonly string puzzle2 = "puzzle2";
    private readonly string puzzle3 = "puzzle3";

    //Variables

    GameObject currentObj;
    string currentObjString;

    Vector3 moveRotate;

    bool canInteract;
    GameObject interactableObject;
    bool canMove = true;
    bool canJump = false;
    bool anestesiaPuzzle = false;

    Rigidbody rb;

    private void Start()
    {
        moveRotate = Vector3.zero;

        rb = this.GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        Interaction();
    }

    private void FixedUpdate()
    {
        if (canMove)
            Movement();
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform") || other.CompareTag("Wall"))
            canJump = true;
        else
        {
            canInteract = true;
            interactableObject = other.gameObject;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        canInteract = false;

        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform"))
            canJump = false;
    }

    private void Movement()
    {
        float moveAxis = Input.GetAxis(verticalAxis);
        float turnAxis = Input.GetAxis(horizontalAxis);

        Vector3 move = transform.right * turnAxis + transform.forward * moveAxis;

        if (moveAxis != 0 || turnAxis != 0)
        {
            moveRotate = transform.right * turnAxis + transform.forward * moveAxis;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            playerBody.transform.rotation = Quaternion.LookRotation(moveRotate * Time.deltaTime);
            rb.MovePosition(transform.position + (move * moveSpeed * Time.deltaTime));
        }
    }

    private void Interaction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (interactableObject != null && canInteract)
            {
                Debug.Log(interactableObject.name);


                if (interactableObject.CompareTag(puzzle1))
                {
                    interactableObject.GetComponent<RockPlatform>().ChangeBall();
                }
                else if (interactableObject.CompareTag(puzzle2))
                {

                }
                else if (interactableObject.CompareTag(puzzle3))
                {

                }
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(currentObj);
        }

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jumping");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void MovementState(bool state)
    {
        canMove = state;
    }
}
