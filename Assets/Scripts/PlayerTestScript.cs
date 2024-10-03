using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTestScript : MonoBehaviour
{

    [Header("Script Comms")]

    public Objective ObjectiveScript;
    public AudioManager AudioManagerScript;
    public uiManager uiManagerScript;
    public Ladder LadderScript;
    


    [Header("Game Information")]
    //Game State
    public bool IsPaused = false;
    public int collectableCount = 0;
    public int GoldValue = 1;
    public int DiamondValue = 5;


    [Header("Player Information")]

    public CharacterController PlayerController;
    public GameObject Player;
 

    public float NormalSpeed;
    public float WalkSpeed = 50f;
    public float SprintSpeed = 65f;
    public float JumpHeight = 2f;
    public float Gravity = -9.81f;

    private float SpeedReductionTime = 5f;  // Time over which to reduce the speed

 
    private float StopMovementDelay = 5f; //Time that speed is set to zero in StopMove function

    public bool Sprinting;
    public bool Jumped;



    public float TurnSmoothing = 0.1f;
    public float TurnSmoothVelocity;
    public Vector3 Direction;

    //Grounding Info

    public LayerMask GroundMask; // Layer mask to specify what layers are considered ground
    public bool IsGrounded; // Check if the player is on the ground
    public GameObject GroundCheckObj;
   

    private Vector3 Velocity; // Current velocity of the player
    public float GroundCheckDistance = 0.2f; // Distance to check if the player is grounded


    //Animations
    private Animator PlayerAnimator;
    public bool InputDisabled;
    private string LandingAnimState = "Hard Landing";  // The name of the animation state


    //Audio

    public AudioSource PlayerAudioSource;

    [Header("Camera Information")]

    public Transform CameraTransform; //assign main camera to this, not PlayerCam



    //Pickups and Carrying

    private GameObject SpawnedObj;
    private GameObject ObjectToPickUp;
    private GameObject SpawnObject;
    private GameObject RefPoint;
    private GameObject SourceObj;

    [Header("Pickup Information")]
    


    [SerializeField] private float SpawnOffsetX = 4f;
    [SerializeField] private float SpawnOffsetY = 0f;
    [SerializeField] private float SpawnOffsetZ = 4f;

    [SerializeField] private bool CarryingObject;


    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        
        AnimatePlayer();
        DropObject();
        GroundCheck();
        Jump();




    }

    void FixedUpdate()
    {
        Move();
        Jump();
        Sprint();
        
    }

    private void OnDrawGizmos()
    {
        DebugCollider();

    }

    #region Colliders
    

    private void DebugCollider()
    {
        CharacterController characterController = Player.GetComponent<CharacterController>();
        if (characterController != null)
        {
            // Draw a wireframe of the collider bounds
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(characterController.bounds.center, characterController.bounds.size);
        }
    }

    

    #endregion

    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EndPoint")
        {
            ObjectiveScript.ObjectiveIndicator.SetActive(false);
            ObjectiveScript.ObjectiveReached();
            AudioManagerScript.ObjectiveReached(); //play completion sound
        }

        if (other.gameObject.tag == "Gold")
        {
            Destroy(other.gameObject);
            collectableCount += GoldValue;
            AudioManagerScript.CollectableObtained();
        }
        else if (other.gameObject.tag == "Diamond")
        {
            Destroy(other.gameObject);
            collectableCount += DiamondValue;
            AudioManagerScript.CollectableObtained();
        }


    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6 && CarryingObject == false) //if object is on layer 6, aka PickupLayer, then do stuff
        {
            Debug.Log("Object on PickupLayer detected");

            //show interaction keybind to pick up object 
            //pick up object
            //deactivate trigger on pickup


            SpawnObject = other.GetComponent<Pickup>().ObjectToSpawn;
            RefPoint = other.gameObject.GetComponent<Pickup>().PickupRefPoint;
            SourceObj = other.gameObject.GetComponent<Pickup>().PickupSourceObj;
            PickUpObject(SpawnObject, RefPoint, SourceObj); //passes Pickup spawn object and ref point, spawns given object in player's hand at ref point

        }

        

        
    }

   

    #endregion



    //#region Movement
    private void GroundCheck()
    {
        
            IsGrounded = Physics.CheckSphere(GroundCheckObj.transform.position, GroundCheckDistance, GroundMask);
        

    }
    

    #region Movement


    private void Move()
        {
            Cursor.lockState = CursorLockMode.Locked;


            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Direction = new Vector3 (horizontal, 0f, vertical).normalized;


            if (!InputDisabled)
            {

                if (Direction.magnitude >= 0.1f)
                {
                    float TargetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + CameraTransform.eulerAngles.y; //returns an angle amt to turn player clockwise from player forward direction
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref TurnSmoothVelocity, TurnSmoothing);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    Vector3 MoveDirection = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward;
                    PlayerController.Move(MoveDirection.normalized * NormalSpeed * Time.fixedDeltaTime);


                }

            }

            

            // Apply vertical velocity (includes gravity)
            PlayerController.Move(Velocity * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        // Check if the player is grounded before jumping
        if (!InputDisabled && IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            
            // Calculate the jump velocity based on the desired jump height
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);  // Using the physics formula: v = ?(2 * height * gravity)
        }

        Jumped = true;

        // Apply gravity to the player
        Velocity.y += Gravity * Time.deltaTime;
        
        // Apply the velocity (gravity or jump) to the CharacterController
        PlayerController.Move(Velocity * Time.deltaTime);

        if (IsGrounded)
        {
            Jumped = false;
        }
        
    }

    private void ApplyGravity()
        {
            // If grounded, don't apply downward acceleration
            if (!IsGrounded)
            {
                Velocity.y += Gravity * Time.deltaTime; // Apply gravity to the velocity's Y component
            }
        }

        private void Sprint()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //Debug.Log("Sprinting");
                NormalSpeed = SprintSpeed; //changes player speed to sprinting
                Sprinting = true;
            }
            else
            {
                NormalSpeed = WalkSpeed; //sets player speed back to walking
                Sprinting = false;
            }
        }

    #endregion

    #region Player Animation

    private void AnimatePlayer()
    {

        //Debug.Log("Grounded? " + IsGrounded);
        //Idle
        if (Direction == Vector3.zero)
        {
            PlayerAnimator.SetFloat("Speed", 0); //sets animator's PlayerSpeed parameter to zero, playing idle anim
            //Debug.Log("Idle");

        }
        //Walk
        else if (!Sprinting)
        {
            PlayerAnimator.SetFloat("Speed", 0.4f);
            //Debug.Log("Walk");

        }


        //Sprint
         if (Sprinting)
        {
            PlayerAnimator.SetFloat("Speed", 1);
            //Debug.Log("Sprint");
        }

         //Falling
         if (Jumped)
        {
            
            PlayerAnimator.SetBool("Jumped", true); //Idle falling anim plays
            //Debug.Log("grounded: " + IsGrounded);
            //CheckAnimState();

        }
         else if (!Jumped && !IsGrounded)
        {
            PlayerAnimator.SetBool("Falling", true); //Idle falling anim plays
            //Debug.Log("grounded: " + IsGrounded);

        }
        if (IsGrounded)
        {
            
            PlayerAnimator.SetBool("Falling", false); //hard landing anim plays
            //CheckAnimState();
        }

        

    }

    private void CheckAnimState()
    {
        AnimatorStateInfo stateInfo = PlayerAnimator.GetCurrentAnimatorStateInfo(0); // Get the current state info for layer 0

        // If any animation is currently playing and has not fully completed
        if (stateInfo.normalizedTime < 1f && !InputDisabled)
        {
            InputDisabled = true;  // Disable input while an animation is playing
        }
        // If no animation is playing or the current animation has finished playing
        else if (stateInfo.normalizedTime >= 1f && InputDisabled)
        {
            InputDisabled = false;  // Re-enable input after the animation finishes
            PlayerAnimator.SetBool("AnimComplete", true);
        }
    }



    #endregion


    #region Object Interaction
    public void PickUpObject(GameObject ObjectToSpawn, GameObject RefPoint, GameObject SourceObj) //typeOf GameObject to indicate what is being passed through
    {
        if (CarryingObject == false &&
            ObjectToSpawn != null &&
            RefPoint != null &&
            SourceObj != null &&
            Input.GetKeyDown(KeyCode.E)) //if the params are filled and E is pressed, 
        {
            SpawnedObj = Instantiate(ObjectToSpawn, RefPoint.transform.position, RefPoint.transform.rotation); //spawn passed obj at passed ref point's position and rotation
            SpawnedObj.transform.SetParent(RefPoint.transform); //set spawned object's parent as RefPoint
            CarryingObject = true;
            SourceObj.SetActive(false);
            
        }

        else if (CarryingObject == true)
        {
            Debug.Log("Already Holding Object");

        }
        else if (ObjectToSpawn == null)
        {
            Debug.Log("Null Spawn Object");

        }
        else if (RefPoint == null)
        {
            Debug.Log("Null Spawn RefPoint");

        }
        else
        {
            Debug.Log("Missing ObjectToSpawn/RefPoint");
        }

    }

    public void DropObject()
    {
        if (CarryingObject == true && SourceObj != null && Input.GetKeyDown(KeyCode.Q))
        {
            SourceObj.SetActive(true); //set active near player
            SourceObj.transform.position = Player.transform.position + new Vector3(SpawnOffsetX, SpawnOffsetY, SpawnOffsetZ); //move hidden Pickup to player with offsets
            CarryingObject = false;

            //Deactivate carried object in player's hand
            Destroy(SpawnedObj);


        }
        else if (SourceObj == null) 
        {
            //Debug.Log("No SourceObj");

        }
        else if (CarryingObject == false)
        {
            //Debug.Log("Carrying is false");

        }

        else
        {
            Debug.Log("No Input / Unknown Error");

        }
    }

    #endregion

}
