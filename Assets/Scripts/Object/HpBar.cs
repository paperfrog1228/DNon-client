using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] private float maxHp=100;
    [SerializeField] private float currentHp=100;
    [SerializeField]private Transform hpFillTransform;
    void Start()
    {
        hpFillTransform = transform.GetChild(0);
    }
    public void SetMaxHp(int hp) {
        maxHp = hp;
    }
    public void SetHpBar(int hp)
    {
        currentHp = hp;
        if (currentHp > maxHp) currentHp = maxHp;
        if (currentHp <0) currentHp = 0;
        if (maxHp == 0) return;
        hpFillTransform.localScale=new Vector3(currentHp / maxHp, hpFillTransform.localScale.y,hpFillTransform.localScale.z);
    }
}
