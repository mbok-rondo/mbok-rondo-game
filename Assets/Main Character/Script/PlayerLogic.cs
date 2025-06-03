using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public SoundEmitter soundEmitter;

    [Header("Player Setting")]
    private Rigidbody rb;
    public Transform PlayerOrientation;
    public float walkspeed, runspeed, fallspeed, airMultiplier;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    public Animator anim;
    bool grounded = true;

    [Header("Player SFX")]
    public AudioClip StepAudio;
    public AudioClip RunAudio;
    AudioSource PlayerAudio;

    void Start()
    {
        soundEmitter = GetComponent<SoundEmitter>();
        rb = GetComponent<Rigidbody>();
        PlayerAudio = GetComponentInChildren<AudioSource>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        GetInput();
        HandleAnimation();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = PlayerOrientation.forward * verticalInput + PlayerOrientation.right * horizontalInput;
    }

    private void MovePlayer()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runspeed : walkspeed;

        if (grounded)
        {
            Vector3 forceToAdd = moveDirection.normalized * currentSpeed;
            rb.velocity = new Vector3(forceToAdd.x, rb.velocity.y, forceToAdd.z);
        }
        else
        {
            rb.AddForce(Vector3.down * fallspeed, ForceMode.Acceleration);
            Vector3 airMove = moveDirection.normalized * currentSpeed * airMultiplier;
            rb.AddForce(new Vector3(airMove.x, 0, airMove.z), ForceMode.Acceleration);
        }
    }

    private void HandleAnimation()
    {
        bool isMoving = moveDirection.magnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

        if (isRunning)
        {
            anim.SetBool("Run", true);
            anim.SetBool("Walk", false);
            soundEmitter?.EmitSound(20f, true); // suara lari
            run();
        }
        else if (isMoving)
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", true);
            soundEmitter?.EmitSound(10f, true); // suara jalan
            step();
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
        }
    }

    private void step()
    {
        if (StepAudio != null && !PlayerAudio.isPlaying)
        {
            PlayerAudio.clip = StepAudio;
            PlayerAudio.Play();
        }
    }

    private void run()
    {
        if (RunAudio != null && !PlayerAudio.isPlaying)
        {
            PlayerAudio.clip = RunAudio;
            PlayerAudio.Play();
        }
    }

    public void groundedchanger()
    {
        grounded = true;
    }
}
