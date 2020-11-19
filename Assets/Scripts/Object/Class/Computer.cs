using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class Computer : Player{
    [ShowInInspector]private GameObject keyboard;
    private Vector3 targetPos;
    [Range(0, 5), SerializeField] private float range;
    private bool busy = false;
    private void Start()
    {
        base.Start();
        keyboard = transform.GetChild(1).gameObject;
        targetPos = keyboard.transform.position - new Vector3(0,range,0);
    }
    private void Update()
    {
        base.Update();
        if (Input.GetMouseButtonUp(0)) {
            Attack();
        }
        if (busy)
            keyboard.transform.position = Vector3.Slerp(keyboard.transform.position, targetPos, 0.05f);
    }
    private void Attack()
    {
        busy = true;
    }
 }
