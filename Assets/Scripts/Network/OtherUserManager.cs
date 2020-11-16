using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
/// <summary>
/// 다른 플레이어들 관리하는 스크립트
/// </summary>
public class OtherUserManager : MonoBehaviour
{
    static OtherUserManager instance;
    [ShowInInspector]
    private List<User> userList = new List<User>();
    [ShowInInspector]
    private Dictionary<int, User> userDic = new Dictionary<int, User>();
    private void Awake()
    {
        instance = this;
    }
    [Button]
    public void InitUser(int socketID)
    {
        if (socketID == NetworkManager.Instance().socketID) return;
        Debug.Log(socketID+"입장.");
        var user = Instantiate(Resources.Load("Prefab/User")) as GameObject;
        var cUser = user.GetComponent<User>();
        cUser.SocketID = socketID;
        userList.Add(cUser);
        userDic[socketID] = cUser;
    }
    public void SetUserPos(int socketID, Vector2 vec) {
        if (socketID == NetworkManager.Instance().socketID) return;
        //Debug.Log(socketID+"번 상대의 위치는 : " + vec);
        userDic[socketID].SetPosition(vec);
    }
    public static OtherUserManager Instance() {
        return instance;

    }
}
