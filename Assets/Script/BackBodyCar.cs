using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBodyCar : MonoBehaviour
{
    public HpController hpController;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            hpController.DamageHP();
            print("JSNFGAUJFBNUAJ");
        }
    }
}
