using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HybridWebSocket;
using Sirenix;
using Sirenix.OdinInspector;

public class WebSocketDemo : MonoBehaviour {
    private static WebSocketDemo instance;
    [SerializeField] string url="ws://localhost:1228";
    WebSocket ws;
    bool connected = false;
    [SerializeField]
    public string socketID = "1234";
    private void Awake()
    {
        instance = this;
    }
    void Start () {
        ws = WebSocketFactory.CreateInstance(url);
        ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");
            Debug.Log("WS state: " + ws.GetState().ToString());
        };

        ws.OnMessage += (byte[] msg) =>
        {
            Debug.Log("WS received message: " + Encoding.UTF8.GetString(msg));
            string msg_str = Encoding.UTF8.GetString(msg);
            var json = JsonToObject<JsonBase>(msg_str);
            switch (json.eventName) {
                case "connected":
                    Connected(msg_str);
                    break;
            }
         };

        ws.OnError += (string errMsg) =>
        {
            Debug.Log("WS error: " + errMsg);
        };


        ws.OnClose += (WebSocketCloseCode code) =>
        {
            Debug.Log("WS closed with code: " + code.ToString());
        };
        ws.Connect();

    }
    private void SendPos() {
        var json = new JsonPosition("position");
        json.SetPos(Player.Instance().gameObject.transform.position);
        ws.Send(GetByte(json));
    }
    private void Update()
    {
        if (!connected) return;
        SendPos();
    }
    void Connected(string js) {
        connected = true;
        var json = JsonToObject<JsonBase>(js);
        Debug.Log(json.data);
        JsonBase data = new JsonBase("socketID");
        ws.Send(GetByte(data));
    }
    
	string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }
    T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
    byte[] GetByte(string s) {
        return Encoding.UTF8.GetBytes(s);
    }
    byte[] GetByte(JsonBase j) { 
        return Encoding.UTF8.GetBytes(ObjectToJson(j));
    }
    public static WebSocketDemo Instance() {
        return instance;
    }
}
