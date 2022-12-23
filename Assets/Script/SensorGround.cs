using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorGround : MonoBehaviour
{
    public CarController carController;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            carController.grounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            carController.grounded = false;
        }
    }
}
