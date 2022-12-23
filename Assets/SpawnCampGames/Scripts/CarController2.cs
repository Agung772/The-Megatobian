using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController2 : MonoBehaviour
{
    public Rigidbody sphereRB;
    public Rigidbody carRB;

    public float maxFwdSpeed = 200f;
    public float fwdSpeed;
    
    public float fwdAccel;
    public float stoppingAccel;
    public float powerRem;
    
    public float revSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;
    public LayerMask deathLayer;

    private float moveInput;
    private float turnInput;
    public bool isCarGrounded;
    
    private float normalDrag;
    public float modifiedDrag;
    
    public float alignToGroundTime;

    public Transform leftFrontWheel, rightFrontWheel;
    public Transform leftBackWheel, rightBackWheel;
    float speedAnimasiRoda = 1000;

    public bool groundDeath, isDeath;
    public Vector3 savePosisi;


    void Start()
    {
        sphereRB.transform.parent = null;
        carRB.transform.parent = null;

        normalDrag = sphereRB.drag;

        savePosisi = transform.position;
    }
    
    void Update()
    {
        //Input
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");
        
        //Rem
        if (Input.GetKey(KeyCode.Space))
        {
            if ((int)fwdSpeed > 0) 
            {
                fwdSpeed -= Time.deltaTime * powerRem;
            }
            else if ((int)fwdSpeed < 0)
            {
                fwdSpeed += Time.deltaTime * powerRem;
            }
            else if ((int)fwdSpeed == 0)
            {
                fwdSpeed = 0;
            }
        }
       
        else if (moveInput > 0)
        {
            if (fwdSpeed < maxFwdSpeed)
            {
                fwdSpeed += Time.deltaTime * fwdAccel;
            }
            else
            {
                fwdSpeed = maxFwdSpeed;
            }
        }
        else if (moveInput < 0)
        {
            if (fwdSpeed > -50)
            {
                fwdSpeed -= Time.deltaTime * stoppingAccel;
            }
        }
        else
        {
            if ((int)fwdSpeed > 0)
            {
                fwdSpeed -= Time.deltaTime * stoppingAccel;
            }
            else if ((int)fwdSpeed < 0)
            {
                fwdSpeed += Time.deltaTime * stoppingAccel;
            }
            else if ((int)fwdSpeed == 0)
            {
                fwdSpeed = 0;
            }
        }

        
        float newRot = turnInput * turnSpeed * Time.deltaTime;
        
        if (isCarGrounded)
            transform.Rotate(0, newRot, 0, Space.World);

        
        transform.position = sphereRB.transform.position;

        // Raycast
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        //Rotate Car
        Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);
        
        
        moveInput *= moveInput > 0 ? fwdSpeed : revSpeed;
        
        
        sphereRB.drag = isCarGrounded ? normalDrag : modifiedDrag;


        //Saveposisi

        if (!groundDeath)
        {
            groundDeath = Physics.Raycast(transform.position, -transform.up, out hit, 5f, deathLayer);
        }
        if (groundDeath && !isDeath)
        {
            isDeath = true;
            fwdSpeed = 0;
            HpController hpController = GetComponent<HpController>();
            hpController.DamageHP();
            hpController.DamageHP();
            hpController.DamageHP();

            StartCoroutine(Coroutine());
            IEnumerator Coroutine()
            {
                yield return new WaitForSeconds(3);
                sphereRB.transform.position = savePosisi + new Vector3(0, 5, 0);
                GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
                camera.transform.position = sphereRB.transform.position;
                groundDeath = false;
                isDeath = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            fwdSpeed = 0;
            sphereRB.transform.position = savePosisi + new Vector3(0, 1, 0);
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.transform.position = sphereRB.transform.position;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            savePosisi = other.transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (isCarGrounded)
        {
            //sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration); //Movement Default
            sphereRB.AddForce(transform.forward * fwdSpeed, ForceMode.Acceleration); //Movement
        }

        else
        {
            sphereRB.AddForce(transform.up * -200f); //Gravity
        }

        
        carRB.MoveRotation(transform.rotation);

        AnimasiRoda();
    }

    void AnimasiRoda()
    {
        if (fwdSpeed > 0)
        {
            leftBackWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
            rightBackWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
            leftFrontWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
            rightFrontWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
        }
        else if (fwdSpeed < 0)
        {
            leftBackWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
            rightBackWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
            leftFrontWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
            rightFrontWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
        }

        if (turnInput > 0)
        {
            leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, 25, leftFrontWheel.localRotation.eulerAngles.z);
            rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, 25, rightFrontWheel.localRotation.eulerAngles.z);
            if ((int)fwdSpeed == 0)
            {
                leftBackWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
                rightBackWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
                leftFrontWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
                rightFrontWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
            }
        }
        else if (turnInput < 0)
        {
            leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, -25, leftFrontWheel.localRotation.eulerAngles.z);
            rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, -25, rightFrontWheel.localRotation.eulerAngles.z);

            if ((int)fwdSpeed == 0)
            {
                leftBackWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
                rightBackWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
                leftFrontWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
                rightFrontWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
            }

        }
        else if (turnInput == 0)
        {
            leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, 0, leftFrontWheel.localRotation.eulerAngles.z);
            rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, 0, rightFrontWheel.localRotation.eulerAngles.z);
        }
    }
}