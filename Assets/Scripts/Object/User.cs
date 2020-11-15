using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public int SocketID;
    public void SetPosition(Vector2 vec) {
        this.gameObject.transform.position=new Vector3(vec.x, vec.y, -10);
    }
}
