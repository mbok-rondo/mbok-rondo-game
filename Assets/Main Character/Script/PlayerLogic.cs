using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    private Rigidbody rb;
    public Transform PlayerOrientation;
    public float walkspeed, runspeed;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    public Animator anim;
    bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Supaya nggak muter random kalau kena benturan
    }

    // Update is called once per frame
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
        if (!grounded) return;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runspeed : walkspeed;

        Vector3 forceToAdd = moveDirection.normalized * currentSpeed;
        rb.velocity = new Vector3(forceToAdd.x, rb.velocity.y, forceToAdd.z);
    }

    private void HandleAnimation()
    {
        bool isMoving = moveDirection.magnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

        anim.SetBool("Run", isRunning);
        anim.SetBool("Walk", isMoving && !isRunning);
    }

    // TIDAK DIUBAH SESUAI PERMINTAAN
    public void groundedchanger()
    {
        grounded = true;
    }
}
