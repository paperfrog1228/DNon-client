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
        var cUser = user.GetComponent<User>();
        user.transform.parent = otherUserTransform;
        cUser.SetSocketID(json.socketID);
        cUser.SetNickname(json.nickname);
        cUser.gameObject.transform.position = new Vector3(json.x, json.y, -10);
        userList.Add(cUser);
        userDic[json.socketID] = cUser;
    }
    public void SetUserState(JsonState js) {
        if (js.socketID == NetworkManager.Instance().socketID) return;
        if (!userDic.ContainsKey(js.socketID)) return;
        var user = userDic[js.socketID];
        user.SetPosition(new Vector2(js.x,js.y));
        user.SetState(js.state);
        user.SetHp(js.hp);
        user.SetDir(js.dir);
    }
  
    //여기서 팩토리 패턴을 쓰면 더 효율적으로 쓰는데 상속을 쓴데 어쩌냐! //enum cc
    private GameObject InstantiateUser(string type) {

    }
    public void ExitUser(int socketID) {
        if (socketID == NetworkManager.Instance().socketID) return;
        for (int i = 0; i < userList.Count; i++) {
            if (socketID == userList[i].SocketID)
            {
                userList.RemoveAt(i);
                break;
            }

        }
        userDic.Remove(socketID);
    
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
