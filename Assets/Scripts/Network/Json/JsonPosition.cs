using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JsonPosition : JsonBase
{
    public float x, y;
    public JsonPosition(string name) : base(name) { 
        eventName = name;
    }
    public void SetPos(Vector2 vec)
    {
        x=Mathf.Round(vec.x);
        y=Mathf.Round(vec.y);
    }
 }
