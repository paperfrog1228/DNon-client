using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JsonBase{
    public string eventName="";
    public string socketID;
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
