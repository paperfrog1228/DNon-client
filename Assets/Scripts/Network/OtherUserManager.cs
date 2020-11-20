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
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform otherUserTransform;
    private void Awake()
    {
        instance = this;
    }
    public void InitUser(JsonUser json)
    {
        if (json.socketID == NetworkManager.Instance().socketID) return;
        var user = InstantiateUser(json.type);
        user.GetComponent<Player>().DestroyThis();
        user.transform.parent = otherUserTransform;
        var cUser = user.GetComponent<User>();
        cUser.SetSocketID(json.socketID);
        cUser.SetNickname(json.nickname);
        userList.Add(cUser);
        userDic[json.socketID] = cUser;
    }
    public void SetUserPos(int socketID, Vector2 vec) {
        if (socketID == NetworkManager.Instance().socketID) return;
        if (!userDic.ContainsKey(socketID)) return;
        userDic[socketID].SetPosition(vec);
    }
   
    private GameObject InstantiateUser(string type) {
        GameObject user = null;
        switch (type)
        {
            case "Chemical":
                user = Instantiate(Resources.Load("Prefab/Chemical")) as GameObject;
                break;
        }
        user.transform.position = new Vector3(0,0,-10);
        return user;
    }
    public void SetPlayer(string type) {
        var player = InstantiateUser(type);
        player.transform.GetChild(0).tag = "Player";
        player.GetComponent<User>().DestroyThis();
        player.transform.parent= playerTransform;
        var cPlayer = player.GetComponent<Player>();
        CameraMover.Instance().SetPlayer(player);
        NetworkManager.Instance().Join(cPlayer,type);
    }
    public static OtherUserManager Instance()
    {
        return instance;

    }
}
