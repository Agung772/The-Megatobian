using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NitroController : MonoBehaviour
{
    public CarController2 carController;
    public float maxNitro, nitro;
    public bool useNitro;
    public Image barNitro;

    private void Start()
    {
        nitro = maxNitro;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && nitro >= maxNitro / 10)
        {
            useNitro = true;

            carController.maxFwdSpeed = 260;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || nitro <= 0 )
        {
            useNitro = false;


            carController.maxFwdSpeed = 200;
        }

        UseNitro();

        barNitro.fillAmount = nitro / maxNitro;
        nitro = Mathf.Clamp(nitro, 0, maxNitro);
    }

    void UseNitro()
    {
        if (useNitro)
        {
            nitro -= Time.deltaTime * 5;
        }
        else
        {
            nitro += Time.deltaTime;
        }
    }
}
