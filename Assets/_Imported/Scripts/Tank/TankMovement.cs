using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField]
    private int playerNumber = 1;         
    [SerializeField]
    private float speed = 12f;            
    [SerializeField]
    private float turnSpeed = 180f;       
    [SerializeField]
    private AudioSource movementAudio;    
    [SerializeField]
    private AudioClip engineIdling;       
    [SerializeField]
    private AudioClip engineDriving;      
    [SerializeField]
    private float pitchRange = 0.2f;

    private string movementAxisName;
    private string turnAxisName;
    private Rigidbody rb;
    private float movementInputValue;
    private float turnInputValue;
    private float originalPitch;


    private const float EPS = 0.1f;

    public void SetPlayerNumber(int num)
    {
        this.playerNumber = num;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        movementInputValue = 0f;
        turnInputValue = 0f;
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
    }


    private void Start()
    {
        movementAxisName = "Vertical" + playerNumber;
        turnAxisName = "Horizontal" + playerNumber;

        originalPitch = movementAudio.pitch;
    }

    private void Update()
    {
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);

        EngineAudio();
    }


    private void EngineAudio()
    {
        if (Mathf.Abs(movementInputValue) < EPS && Mathf.Abs(turnInputValue) < EPS)
        {
            if (movementAudio.clip == engineDriving)
            {
                movementAudio.clip = engineIdling;
                movementAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }
        else
        {
            if (movementAudio.clip == engineIdling)
            {
                movementAudio.clip = engineDriving;
                movementAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }
    } // EngineAudio() //


    private void FixedUpdate()
    {
        Move();
        Turn();
    }


    private void Move()
    {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }


    private void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        Quaternion rotation = transform.rotation * turnRotation;

        rb.MoveRotation(rotation);

    }

} // end of class ///

