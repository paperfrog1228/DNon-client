using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonAttack : JsonBase
{
    public float x1, x2, y1, y2;
    public JsonAttack(string name) : base(name) { 
        eventName = name;
        socketID = NetworkManager.Instance().socketID;
    }
    public void SetPos(Vector2 s, Vector2 t) {
        x1 = Mathf.Round(s.x);
        y1 = Mathf.Round(s.y);
        x2 = Mathf.Round(t.x);
        y2 = Mathf.Round(t.y);
    }   
}
