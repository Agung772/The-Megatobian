using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriveModif : MonoBehaviour
{
    public float speed, speedMax, speedMin;
    public float turnSpeed;
    public float gravityMultiplier;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Accelerate();
        Turn();
        Fall();
    }

    void Accelerate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            speed += Time.deltaTime * 10;
            speedMin = -1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speed -= Time.deltaTime * 10;
            speedMin = -1;
        }
        else
        {
            speed -= Time.deltaTime * 5;
            speedMin = 0;
        }

        Vector3 forceToAdd = transform.forward;
        forceToAdd.y = 0;
        rb.AddForce(forceToAdd * speed * 10);

        speed = Mathf.Clamp(speed, speedMin, speedMax);

        Vector3 locVel = transform.InverseTransformDirection(rb.velocity);
        locVel = new Vector3(0, locVel.y, locVel.z);
        rb.velocity = new Vector3(transform.TransformDirection(locVel).x, rb.velocity.y, transform.TransformDirection(locVel).z);
    }

    void Turn()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(-Vector3.up * turnSpeed * 10);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(Vector3.up * turnSpeed * 10);
        }
    }

    void Fall()
    {
        rb.AddForce(Vector3.down * gravityMultiplier * 10);
    }
}