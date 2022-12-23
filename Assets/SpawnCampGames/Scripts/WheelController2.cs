using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController2 : MonoBehaviour
{
    public NitroController nitroController;
    public CarController2 carController;
    public TrailRenderer[] trails;
    public TrailRenderer[] trailsNitro;
    public ParticleSystem smoke;
    

    void Update()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        if (horizontalAxis == 1 && carController.fwdSpeed > 20 && carController.isCarGrounded|| horizontalAxis == -1 && carController.fwdSpeed > 20 && carController.isCarGrounded || Input.GetKey(KeyCode.Space) && carController.fwdSpeed > 20 && carController.isCarGrounded)
        {
            foreach (var trail in trails)
            {
                trail.emitting = true;
            }

            var emission = smoke.emission;
            emission.rateOverTime = 50f;
        }
        else
        {
            foreach (var trail in trails)
            {
                trail.emitting = false;
            }
            
            var emission = smoke.emission;
            emission.rateOverTime = 0f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && nitroController.useNitro)
        {
            foreach (var trail in trailsNitro)
            {
                trail.emitting = true;
            }
        }
        else
        {
            foreach (var trail in trailsNitro)
            {
                trail.emitting = false;
            }
        }
    }
}
