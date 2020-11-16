using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// JSON-> 유니티 역직렬화 시 해당하는 변수 명이 반드시 존재해야하므로
/// 이벤트의 이름을 식별받기 위한
/// 모든 JSON 클래스 안의 변수를 담은 Common 스크립트입니다.
/// </summary>
public class JsonCommon : MonoBehaviour
{
    public string ninkname;
    public string type;
    public string eventName = "";
    public int socketID;
    public float x, y;
    public string data = "";
}
