using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (player == null) return;
        Vector3 pos=player.transform.position;
        gameObject.transform.position=new Vector3(pos.x,pos.y,pos.z-10f);
    }
    public void SetPlayer(GameObject arg) {
        player = arg;
    }
    #region singleton
    static CameraMover instance;
    public static CameraMover Instance() { 
            return instance;
    }
    #endregion
}
