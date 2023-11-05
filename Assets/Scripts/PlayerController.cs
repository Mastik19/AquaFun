using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
   
    Vector3 direction;

    public float speedZ;

    public int desiredLane;
    public float distanceLane;
    public float gravity;
    public float jumpForce;

    public int maxSpeed;

    



     Animator anim;

    private bool isSliding;
    public bool isJumpingOver;

    private void Awake()
    {
        desiredLane = 1;
        maxSpeed = 30;
        speedZ = 7;
    }
    void Start()
    {
        
        isJumpingOver = false;
        isSliding = false;
        anim = GetComponentInChildren<Animator>();
        
        distanceLane = 1.2f;
        controller = GetComponent<CharacterController>();
        direction.z = speedZ;
        gravity = -20;
        jumpForce = 7;

        

       
    }

    private void Update()
    {
        
        if(!PlayerManager.isGameStarted)
        {
            return;
        }

        if(PlayerManager.isGameOver)
        {
            return;
        }


        anim.SetBool("isGameStarted", true);

       
        if(transform.position.y <= 1  )
        {
            
            StartCoroutine(FallDown());
           
        }


        if(speedZ < maxSpeed)
        {
            speedZ += 0.2f * Time.deltaTime;
        }


        if(SwipeManager.swipeLeft )
        {
            desiredLane--;
            if(desiredLane == -1)
            {
                desiredLane = 0;
            }
          

        }

        if(SwipeManager.swipeRight)
        {
            desiredLane++;
            if(desiredLane == 3)
            {
                desiredLane = 2;
            }
        }


        if(controller.isGrounded)
        {
            if(SwipeManager.swipeUp)
            {
                direction.y = jumpForce;
                anim.SetBool("isJumping", true);

            }


        }
        else
        {
            direction.y += gravity * Time.deltaTime;
            anim.SetBool("isJumping", false);

        }


        if(SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());

        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane ==0)
        {
            targetPosition += Vector3.left * distanceLane;

        }
        else if(desiredLane ==2)
        {

            targetPosition += Vector3.right * distanceLane;
        }

        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

       // transform.position = targetPosition;




        controller.Move(direction * Time.deltaTime);


    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            PlayerManager.isGameOver = true;
            
            
        }
        
    }

    


    public IEnumerator Slide()
    {
        isSliding = true;
        anim.SetBool("isRolling", true);
        controller.center = new Vector3(0, 0.35f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isRolling", false);
        controller.center = new Vector3(0, 0.85f, 0);
        controller.height = 1.8f;
        isSliding = false;


    }

    public IEnumerator JumpOver()
    {
        
        isJumpingOver = true;
        anim.SetBool("isJumpingOver", true);

        yield return new WaitForSeconds(1);

        anim.SetBool("isJumpingOver", false);
        isJumpingOver = false;
        

    }
    public IEnumerator FallDown()
    {
        anim.SetTrigger("Fall");
        FindObjectOfType<AudioManager>().PlaySound("PlayerFallingDown");
        CameraMove.yPos = transform.position.y;
        yield return new WaitForSeconds(0.3f);
        PlayerManager.isGameOver = true;

        
    }


}
