using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffects : MonoBehaviour
{
    bool cd;
    public GameObject hitEffectPrefab;

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(hitEffectPrefab, other.GetContact(0).point, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finis") && !cd)
        {
            cd = true;
            GameManager.instance.Finis();

            StartCoroutine(Coroutine());
            IEnumerator Coroutine()
            {
                yield return new WaitForSeconds(10);
                cd = false;
            }
        }
    }
}
