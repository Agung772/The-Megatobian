using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public HpController car;

    private void Awake()
    {
        car = GameObject.FindGameObjectWithTag("Car").GetComponent<HpController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            car.DamageHP();
        }
    }
}
