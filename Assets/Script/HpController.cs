using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    public float maxHpPlayer, hpPlayer;
    public Image barHp;

    private void Start()
    {
        hpPlayer = maxHpPlayer;
        barHp.fillAmount = hpPlayer / maxHpPlayer;
    }
    public void DamageHP()
    {
        hpPlayer--;
        barHp.fillAmount = hpPlayer / maxHpPlayer;

        if (hpPlayer <= 0)
        {
            GameManager.instance.Defeat();
        }
    }
}
