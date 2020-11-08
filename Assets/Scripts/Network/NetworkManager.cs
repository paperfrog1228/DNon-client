using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class NetworkManager : MonoBehaviour
{ 
    [SerializeField] string url="ws://localhost:1228";
    [SerializeField] public string socketID = "1234";
    public bool onConnect=false;
    System.Random r=new System.Random();
    WebSocketManager webSocketManager;
    JsonManager jsonManager;
    JsonBase requestJson;
    public void Test() { 
        JsonBase data = new JsonBase("socketID");
        webSocketManager.SendMsg(data);
      }
    public void Connect() {
        webSocketManager.Connect(url);
    }

    void OtherPosition(string js) {
        var json = jsonManager.JsonToObject<JsonPosition>(js);
        OtherUserManager.Instance().SetUserPos(Int32.Parse(json.socketID), new Vector2(json.x, json.y));
    }
    public void Connected(string js) {
        Debug.Log("Connect Allow.");
        onConnect = true;
        var json = jsonManager.JsonToObject<JsonBase>(js);
        JsonBase data = new JsonBase("socketID");
        webSocketManager.SendMsg(data);
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
        }
    }
    #region mono
    private void Awake()
    {
        instance = this;
        requestJson = new JsonBase("request");
        socketID = r.Next(1, 100).ToString();
     
        Debug.Log("My socket id is " + socketID);
    }
    private IEnumerator RequestCorutine()
    {
        WaitForSeconds waitSec = new WaitForSeconds(1);

                   

            yield return waitSec;
    }


    void Start()
    {
        webSocketManager = WebSocketManager.Instance();
        jsonManager = JsonManager.Instance();
        Connect();
    }

    void SendPos() {
        var json = new JsonPosition("position");
        json.SetPos(Player.Instance().gameObject.transform.position);
        webSocketManager.SendMsg(json);
    }
    void Update()
    {
        if (webSocketManager.MessageQueue.Count > 0) 
           DecodeMsg(webSocketManager.MessageQueue.Dequeue());
           }
    #endregion
    #region singleton
    private static NetworkManager instance;
    public static NetworkManager Instance() { 
            return instance;
    }
    #endregion
}
