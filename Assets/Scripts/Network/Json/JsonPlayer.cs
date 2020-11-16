using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 플레이어 정보 전달용
/// </summary>
public class JsonPlayer : JsonBase
{
    public string nickname;
    public string type; // 후에 타입은 int로 변경될 수도 있음.
    public void SetNickname(string str) {
        nickname = str;
    }
    public void SetType(string str) {
        type = str;
    }
    public JsonPlayer(string name) : base(name) { 
        eventName = name;
        socketID = NetworkManager.Instance().socketID;
    }
}
