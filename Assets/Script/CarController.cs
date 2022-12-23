using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Animator animator;
    public new Rigidbody rigidbody;

    public float forwardAccel, reverseAccel, maxSpeed, turnStrength, gravityForce = 10, dragOnGround = 3;

    float speedInput, turnInput;

    public bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = 0.5f;
    public Transform groundRayPoint;

    public Transform leftFrontWheel, rightFrontWheel;
    public Transform leftBackWheel, rightBackWheel;
    public float maxWheelTurn = 25, speedAnimasiRoda = 200;

    float leftBackX;
    private void Start()
    {
        rigidbody.transform.parent = null;
    }
    void Update()
    {
        speedInput = 0;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000;

            //Animasi Ban
            leftBackWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
            rightBackWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
            leftFrontWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
            rightFrontWheel.Rotate(Vector3.right, -speedAnimasiRoda * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000;

            //Animasi Ban
            leftBackWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
            rightBackWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
            leftFrontWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
            rightFrontWheel.Rotate(Vector3.right, speedAnimasiRoda * Time.deltaTime);
        }


        turnInput = Input.GetAxis("Horizontal");

        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnInput * turnStrength * Time.deltaTime, 0));
        }

        if (turnInput > 0)
        {
            leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, 25, leftFrontWheel.localRotation.eulerAngles.z);
            rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, 25, rightFrontWheel.localRotation.eulerAngles.z);

            //Animasi Ban
            if (Input.GetAxis("Vertical") == 0)
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

            //Animasi Ban
            if (Input.GetAxis("Vertical") == 0)
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
        //Roda belok default
        //leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
        //rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, rightFrontWheel.localRotation.eulerAngles.z);



        transform.position = rigidbody.transform.position;
    }

    private void FixedUpdate()
    {
        //grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, transform.up, out hit, groundRayLength, whatIsGround))
        {
            //grounded = true;

            //transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        }

        if (grounded)
        {
            rigidbody.drag = dragOnGround;

            if (Mathf.Abs(speedInput) > 0)
            {
                rigidbody.AddForce(transform.forward * speedInput);
            }


        }
        else
        {
            rigidbody.drag = 0.1f;
            rigidbody.AddForce(Vector3.up * -gravityForce * 100);
        }


    }
}
