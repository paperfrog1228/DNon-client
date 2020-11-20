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
            case "receiveAttackChemical":
            ReceiveAttackChemical(msg_str);
                break;
            case "exitOther":
                ExitOther(msg_str);
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
    public void ExitGame() {
        var json = new JsonBase("exitUser");
        webSocketManager.SendMsg(json);
    }
    public void Join(Player player,string type) {
        this.player = player;
        JsonUser data = new JsonUser("join");
        data.SetNickname("empty");//todo: 지훈쿤의 프론트페이지에서 받아와야한다.
        if (APIClient.GetClient().PlayerInfo != null)
        {
            var tmp = APIClient.GetClient().PlayerInfo;
            player.SetSocketID(tmp.playerId);
            player.SetNickname(tmp.playerName);
            data.SetNickname(tmp.playerName);
        }
        Debug.Log(APIClient.GetClient().PlayerInfo);
        data.SetType(type);
        webSocketManager.SendMsg(data);
        StartCoroutine("SendStateCoroutine", frame);
    }
    public void AttackChemical(Vector2 start, Vector2 target) {
        var json = new JsonAttack("chemicalAttack");
        json.SetPos(start, target);
        webSocketManager.SendMsg(json);
    }
    #endregion
    #region Stub
    void ExitOther(string js) {
        var json = jsonManager.JsonToObject<JsonBase>(js);
        OtherUserManager.Instance().ExitUser(json.socketID) ;
    }
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
    public void ReceiveAttackChemical(string js) {
        var json = jsonManager.JsonToObject<JsonAttack>(js);
        ObjectManager.Instance().AttackChemical(json);
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
    private void OnApplicationQuit()
    {
        ExitGame();
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
