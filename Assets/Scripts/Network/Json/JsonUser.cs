using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 플레이어 정보 전달용
/// </summary>
public class JsonUser : JsonBase
{
    public string nickname;
    public string type;
    public float x;
    public float y;
    public void SetPos(Vector2 vec) {
        x = (int)vec.x;
        y = (int)vec.y;
    }
    public void SetNickname(string str) {
        nickname = str;
    }
    public void SetType(string str) {
        type = str;
    }
    public JsonUser(string name) : base(name) { 
        eventName = name;
        socketID = NetworkManager.Instance().socketID;
    }
}
