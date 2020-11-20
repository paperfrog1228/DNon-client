﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 공격등 추가적인 오브젝트 관리.
/// </summary>
public class ObjectManager : MonoBehaviour
{
    public void AttackChemical(JsonAttack json) {
        Debug.Log("실행돠라고");
        if (json.socketID == NetworkManager.Instance().socketID) return;
        var flask = Instantiate(Resources.Load("Prefab/flask")) as GameObject;
        flask.transform.position = new Vector3(json.x1,json.x2,-10);
        flask.GetComponent<Flask>().targetPos = new Vector3(json.y1, json.y2, -10);
        flask.SetActive(true);
    }
    private void Awake()
    {
        instance = this;
    }

    static ObjectManager instance;
    public static ObjectManager Instance()
    {
        return instance;

    }
}
