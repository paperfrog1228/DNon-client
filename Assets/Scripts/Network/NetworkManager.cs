using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class NetworkManager : MonoBehaviour
{ 
    [SerializeField] string url="wws://localhost:1228";
    [SerializeField] public string socketID = "1234";
    public bool onConnect=false;
    System.Random r=new System.Random();
    WebSocketManager webSocketManager;
    JsonManager jsonManager;
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
        StartCoroutine("SendPosCoroutine",0.3f);
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
        }
    }
    #region mono
    private void Awake()
    {
        instance = this;
        socketID = r.Next(1, 100).ToString();
     
        Debug.Log("My socket id is " + socketID);
    }

    void Start()
    {
        webSocketManager = WebSocketManager.Instance();
        jsonManager = JsonManager.Instance();
        Connect();
    }
    void InitOtherPlayer(string msg_str) {
        var json = jsonManager.JsonToObject<JsonBase>(msg_str);
        OtherUserManager.Instance().InitUser(Int32.Parse(json.data));
    }
    void SendPos() {
        //Debug.Log("보낸다!");
        var json = new JsonPosition("position");
        json.SetPos(Player.Instance().gameObject.transform.position);
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
