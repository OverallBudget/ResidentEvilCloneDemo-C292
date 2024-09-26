using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float mouseSensitivity = 60f;
    private bool isGrounded;
    private bool doubleJump;
    private Rigidbody rb;

    [SerializeField] Transform fpsCamera;
    [SerializeField] float verticalLookLimit;
    private float xRotation;

    [SerializeField] Transform firePoint;

    private Magazine currentMag;

    public Magazine CurrentMag { get => currentMag; set => currentMag = value; }

    //private Magazine currentMag2;

    //public Magazine CurrentMag2 { get { return currentMag; } set { currentMag = value; } }



    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAround();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //Debug.Log("Shoot");
        //    //shoot(1);
        //}

        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = 100f;
            Debug.DrawRay(fpsCamera.position, fpsCamera.forward * distance, Color.green, 2f);
            if(Physics.Raycast(fpsCamera.position, fpsCamera.forward, out RaycastHit hit, distance))
            {
                if(hit.transform.TryGetComponent(out Magazine magazine))
                {
                    Debug.Log("Magazine");
                    magazine.OnPickup(this);
                    Debug.Log(currentMag);
                }
            }
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 moveVelocity = move * moveSpeed;
        moveVelocity.y = rb.velocity.y;
        rb.velocity = moveVelocity; 
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);

        fpsCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Jump()
    {
        if (isGrounded == true)
        {
            rb.velocity = Vector3.up * jumpForce;
            isGrounded = false;
        }
        else
        {
            if (doubleJump)
            {
                rb.velocity = Vector3.up * jumpForce;
                doubleJump = false;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            doubleJump = true;
        }
    }

    //private void shoot(float damage)
    //{
        
    //    if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100))
    //    {
    //        Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
    //        if (hit.transform.CompareTag("Zombie"))
    //        {
    //            hit.transform.GetComponent<Zombie>().TakeDamage(damage);
    //        }
    //    }
    //}
}
