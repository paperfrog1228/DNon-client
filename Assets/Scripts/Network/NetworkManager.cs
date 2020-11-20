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
    [SerializeField,Range(0,0.5f)] private float frame;
    public bool onConnect=false;
    System.Random r=new System.Random();
    WebSocketManager webSocketManager;
    JsonManager jsonManager;
    Player player;

    public void Connect() {
        webSocketManager.Connect(url);
    }
    private void DecodeMsg(byte[] msg) 
    { 
        string msg_str = Encoding.UTF8.GetString(msg);
        var json = jsonManager.JsonToObject<JsonBase>(msg_str);
        switch (json.eventName) {
        case "connected":
            Connected(msg_str);
        break;
        case "receiveOtherState":
            ReceiveOtherState(msg_str);
        break;
        case "initOtherPlayer":
            InitOtherPlayer(msg_str);
        break;
        case "notifyNewPlayer":
            InitOtherPlayer(msg_str);
        break;
              }
    }

    #region Proxy&Stub
    #region Proxy
    JsonState jsonState;
    void SendState() {
        jsonState.SetPos(player.gameObject.transform.position);
        jsonState.SetHp(player.Hp);
        jsonState.SetDir((int)player.transform.GetChild(0).localScale.x);
        jsonState.SetState((int)player.state);
        webSocketManager.SendMsg(jsonState);
    }
    public void Join(Player player,string type) {

        this.player = player;
        if(APIClient.GetClient().PlayerInfo!=null)
        player.SetNickname(APIClient.GetClient().PlayerInfo.playerName);
        player.SetSocketID(socketID);
        JsonUser data = new JsonUser("join");
        data.SetNickname("킹갓형석");//todo: 지훈쿤의 프론트페이지에서 받아와야한다.
        data.SetType(type);
        webSocketManager.SendMsg(data);
        StartCoroutine("SendStateCoroutine", frame);
    }
    #endregion
    #region Stub
    void ReceiveOtherState(string js) {
        var json = jsonManager.JsonToObject<JsonState>(js);
        OtherUserManager.Instance().SetUserState(json);
    }
    void InitOtherPlayer(string msg_str) {
        var json = jsonManager.JsonToObject<JsonUser>(msg_str);
        OtherUserManager.Instance().InitUser(json);
    }
    public void Connected(string js) {

        Debug.Log("Connect Allow.");
        onConnect = true;
        UIManager.Instance().SetFalseLoadingPanel();
    }
    #endregion
    #endregion
    #region mono
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (APIClient.GetClient().PlayerInfo != null)
        {
            var tmp = APIClient.GetClient().PlayerInfo;
            Debug.Log("My socket id is " + tmp.playerId + "" + tmp.playerName);
            socketID = tmp.playerId;
        }
        else {
            var rand =new System.Random();
            socketID = rand.Next(0, 100);
        }
        webSocketManager = WebSocketManager.Instance();
        jsonManager = JsonManager.Instance();
        jsonState = new JsonState("sendState");
        Connect();
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
    IEnumerator SendStateCoroutine(float time){
        while (true)
        {
            SendState();
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
