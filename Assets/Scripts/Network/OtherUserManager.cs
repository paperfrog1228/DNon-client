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
    public void JoinUser(int socketID)
    {
        var user = Instantiate(Resources.Load("Prefab/User")) as GameObject;
        var cUser = user.GetComponent<User>();
        cUser.SocketID = socketID;
        userList.Add(cUser);
        userDic[socketID] = cUser;
    }
    //todo : string이 좋으려나 int가 좋으려나
    public void SetUserPos(int socketID, Vector2 vec) {
        if (socketID == System.Int32.Parse(NetworkManager.Instance().socketID)) return;
        Debug.Log("상대의 위치는 : " + vec);
      //  userDic[socketID].SetPosition(vec);
    }
    public static OtherUserManager Instance() {
        return instance;

    }
}
