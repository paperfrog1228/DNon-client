﻿using Sirenix.OdinInspector;
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
    private int hp;
    protected Animator animator;
    [ShowInInspector]
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            hpbar.SetHpBar(hp);   
        }
    }
    [SerializeField]protected HpBar hpbar;
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
        hpbar=transform.parent.GetChild(1).GetComponent<HpBar>();
        Hp = 100;
    }
    #region Transform&Animation
    protected void SetNicknamePosition() {
        nickname.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
    }
    public void SetPosition(Vector2 vec) {
        if (userTransfrom == null) return;
        userTransfrom.position=new Vector3(vec.x, vec.y, -10);
    }
    /// <summary>
    /// localscale을 통해 좌우 반전 시킴.
    /// </summary>
    /// <param name="dir"> 0 == left, 1== right</param>
    protected void SetDirection(int dir) {
        var tmp = transform.localScale;
        if(dir==0)
        transform.localScale = new Vector3(tmp.x, tmp.y, tmp.z);
        else
        transform.localScale = new Vector3(-tmp.x, tmp.y, tmp.z);
    }
    #endregion
}
