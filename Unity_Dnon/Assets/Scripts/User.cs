using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{   
    [ShowInInspector] protected int socketID;
    public int SocketID { get { return socketID; } set { socketID = value; } }
    public void SetPosition(Vector2 vec) {
        this.gameObject.transform.position = vec;
    }
}
