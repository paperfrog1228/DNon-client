using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User : MonoBehaviour
{
    [BoxGroup,ShowInInspector] private GameObject nickname;
    [ShowInInspector]private Transform userTransfrom;
    [BoxGroup]public int SocketID;
    [BoxGroup] private string nicknameStr;
    public void DestroyThis() {
        Destroy(this);
    }
    public void SetSocketID(int id) {
        SocketID = id;
    }
    public void SetNickname(string str) {
        nicknameStr = str;
    }
    protected void Start()
    {
        nickname=Instantiate(Resources.Load("Prefab/Nickname")) as GameObject;
        nickname.transform.parent = UIManager.Instance().NicknamePanel.transform;
        nickname.GetComponent<Text>().text = nicknameStr;
        userTransfrom = this.gameObject.transform;
    }
    protected void SetNicknamePosition() {
        nickname.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
    }
    public void SetPosition(Vector2 vec) {
        if (userTransfrom == null) return;
        userTransfrom.position=new Vector3(vec.x, vec.y, -10);
    }
}
