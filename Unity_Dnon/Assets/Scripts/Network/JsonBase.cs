using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JsonBase : MonoBehaviour
{
    public string eventName;
    public string message;
    public JsonBase(string name) {
        eventName = name;
    }
    public void AddMessage(string msg)
    {
        message += msg;
    }
   }
