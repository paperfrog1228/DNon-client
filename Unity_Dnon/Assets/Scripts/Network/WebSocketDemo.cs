using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HybridWebSocket;
using Sirenix;
using Sirenix.OdinInspector;

public class WebSocketDemo : MonoBehaviour {
    [SerializeField] string url="ws://localhost:1228";
    WebSocket ws;
	void Start () {
        ws = WebSocketFactory.CreateInstance(url);
        ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");
            Debug.Log("WS state: " + ws.GetState().ToString());
            
           
        };
        // Add OnMessage event listener
        ws.OnMessage += (byte[] msg) =>
        {
            Debug.Log("WS received message: " + Encoding.UTF8.GetString(msg));

            ws.Close();
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
    [Button]
    public void Test(){
        JsonBase data = new JsonBase("test");
        data.AddMessage("에잇 씨팔!!");
        ws.Send(Encoding.UTF8.GetBytes(ObjectToJson(data)));
      }
	void Update () {
		//ws.Send(Encoding.UTF8.GetBytes("test"));
	}
    string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }
    T JsonToOject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
}
