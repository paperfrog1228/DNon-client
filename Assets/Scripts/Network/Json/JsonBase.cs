using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 간단한 정보를 보낼 때 사용하며
/// 필수적인 이벤트 네임과 소켓id, 보낼 데이터를 문자열로 하여 보낸다.
/// </summary>
public class JsonBase{
    public string eventName="";
    public int socketID;
    public string data="";
    public JsonBase(string name) {
        eventName = name;
        socketID = NetworkManager.Instance().socketID;
    }
    public void Adddata(string msg)
    {
        data += msg;
    }
   }
