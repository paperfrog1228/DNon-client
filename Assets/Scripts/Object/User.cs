using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User : MonoBehaviour
{
    [ShowInInspector] private GameObject nickname;
    private Transform userTransfrom;
    public int SocketID;
    protected void Start()
    {
        nickname=Instantiate(Resources.Load("Prefab/Nickname")) as GameObject;
        nickname.transform.parent = UIManager.Instance().NicknamePanel.transform;
        userTransfrom = this.gameObject.transform;
    }
    protected void SetNicknamePosition() {
        nickname.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
    }
    public void SetPosition(Vector2 vec) {
        userTransfrom.position=new Vector3(vec.x, vec.y, -10);
    }
}
