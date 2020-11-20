using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JsonState : JsonBase
{
    public int state;
    public float x, y;
    public int dir;
    public int hp;
    public JsonState(string name) : base(name) { 
        eventName = name;
    }
    public void SetPos(Vector2 vec)
    {
        x=Mathf.Round(vec.x);
        y=Mathf.Round(vec.y);
    }
    public void SetDir(int d) { dir = d; }
    public void SetState(int state) { this.state = state; }
    public void SetHp(int hp) { this.hp = hp; }
 }
