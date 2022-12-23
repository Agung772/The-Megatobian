using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public bool death;
    public float maxHpEnemy, hpEnemy;
    public Image barHP;
    public Vector3 savePosisi;

    public NavMeshAgent navMeshAgent;
    public Transform parentWaypoint;
    public Transform[] waypoint;
    int numberWaypoint;

    public Transform leftFrontWheel, rightFrontWheel;
    public Transform leftBackWheel, rightBackWheel;

    private void Start()
    {
        waypoint = new Transform[parentWaypoint.transform.childCount];
        for (int i = 0; i < waypoint.Length; i++)
        {
            waypoint[i] = parentWaypoint.transform.GetChild(i);
        }

        Destination();

        hpEnemy = maxHpEnemy;
        barHP.fillAmount = hpEnemy / maxHpEnemy;
    }

    private void Update()
    {
        if (!death)
        {
            if (navMeshAgent.remainingDistance < 15)
            {
                numberWaypoint++;
                Destination();
            }
        }

        leftBackWheel.Rotate(Vector3.right, -1000 * Time.deltaTime);
        rightBackWheel.Rotate(Vector3.right, -1000 * Time.deltaTime);
        leftFrontWheel.Rotate(Vector3.right, -1000 * Time.deltaTime);
        rightFrontWheel.Rotate(Vector3.right, -1000 * Time.deltaTime);

    }

    void Destination()
    {
        if (numberWaypoint == waypoint.Length)
        {
            numberWaypoint = 0;
        }

        Vector3 randomX = waypoint[numberWaypoint].position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        navMeshAgent.SetDestination(randomX);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !death)
        {
            hpEnemy -= 1;
            barHP.fillAmount = hpEnemy / maxHpEnemy;
            if (hpEnemy <= 0)
            {
                savePosisi = transform.position;
                death = true;
                navMeshAgent.enabled = false;
                Ripep();
            }
        }
    }

    void Ripep()
    {
        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(5);
            transform.position = savePosisi;
            hpEnemy = maxHpEnemy;
            barHP.fillAmount = hpEnemy / maxHpEnemy;
            death = false;
            navMeshAgent.enabled = true;
        }
    }


}
