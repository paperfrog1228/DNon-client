using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// JSON 역직렬화 시 해당하는 변수 없으면 에러 나기 때문에 모든 JSON 파일의 변수를 담고 있는 Base 클래스입니다.
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
