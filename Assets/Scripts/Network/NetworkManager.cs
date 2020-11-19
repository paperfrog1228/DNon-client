using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class NetworkManager : MonoBehaviour
{ 
    [SerializeField] string url="wws://localhost:1228";
    [SerializeField] public int socketID = 9999;
    public bool onConnect=false;
    System.Random r=new System.Random();
    WebSocketManager webSocketManager;
    JsonManager jsonManager;
    Player player;
    public void Connect() {
        webSocketManager.Connect(url);
    }
    void OtherPosition(string js) {
        var json = jsonManager.JsonToObject<JsonPosition>(js);
        OtherUserManager.Instance().SetUserPos(json.socketID, new Vector2(json.x, json.y));
    }
    public void Connected(string js) {
        Debug.Log("Connect Allow.");
        onConnect = true;
        UIManager.Instance().SetFalseLoadingPanel();
    }
    public void Join(Player player,string type) {
        this.player = player;
        player.SetSocketID(socketID);
        JsonUser data = new JsonUser("join");
        data.SetNickname("킹갓형석");//todo: 지훈쿤의 프론트페이지에서 받아와야한다.
        data.SetType(type);
        webSocketManager.SendMsg(data);
        StartCoroutine("SendPosCoroutine", 0.3f);
    }
    private void DecodeMsg(byte[] msg) 
    { 
        string msg_str = Encoding.UTF8.GetString(msg);
        var json = jsonManager.JsonToObject<JsonBase>(msg_str);
        switch (json.eventName) {
        case "connected":
            Connected(msg_str);
        break;
        case "otherPosition":
            OtherPosition(msg_str);
        break;
        case "initOtherPlayer":
            InitOtherPlayer(msg_str);
        break;
        case "notifyNewPlayer":
            InitOtherPlayer(msg_str);
        break;
        }
    }
    #region mono
    private void Awake()
    {
        instance = this;
        socketID = r.Next(1, 100);
        Debug.Log("My socket id is " + socketID);
    }

    void Start()
    {
        webSocketManager = WebSocketManager.Instance();
        jsonManager = JsonManager.Instance();
        Connect();
    }
    void InitOtherPlayer(string msg_str) {
        var json = jsonManager.JsonToObject<JsonUser>(msg_str);
        OtherUserManager.Instance().InitUser(json);
    }
    void SendPos() {
        var json = new JsonPosition("position");
        json.SetPos(player.gameObject.transform.position);
        webSocketManager.SendMsg(json);
    }
    void Update()
    {
        webSocketManager.Dispatch();
        if (webSocketManager.MessageQueue.Count > 0)
        {   
            while(webSocketManager.MessageQueue.Count>0)
            DecodeMsg(webSocketManager.MessageQueue.Dequeue());
        }

    }
    IEnumerator SendPosCoroutine(float time){
        while (true)
        {
            SendPos();
            yield return new WaitForSeconds(time);
        }
    }
    #endregion
    #region singleton
    private static NetworkManager instance;
    public static NetworkManager Instance() { 
            return instance;
    }
    #endregion
}
