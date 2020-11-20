using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using NativeWebSocket;
using Sirenix;
using Sirenix.OdinInspector;
using System;
public class WebSocketManager{
    WebSocket ws;

    public Queue<byte[]> MessageQueue = new Queue<byte[]>();
    public async void Connect (string url) {
        ws = WebSocketFactory.CreateInstance(url);
        ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");
        };
        ws.OnMessage += (byte[] msg) =>
        {
        //  Debug.Log("WS received message: " + Encoding.UTF8.GetString(msg));
            MessageQueue.Enqueue(msg);
         };

        ws.OnError += (string errMsg) =>
        {
            Debug.Log("WS error: " + errMsg);
        };


        ws.OnClose += (WebSocketCloseCode code) =>
        {
            NetworkManager.Instance().ExitGame();
            Debug.Log("WS closed with code: " + code.ToString());
        };
       // InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);
        await ws.Connect();

    }
    public void Dispatch() {
        #if !UNITY_WEBGL || UNITY_EDITOR
        ws.DispatchMessageQueue();
        #endif
    }

    async void SendWebSocketMessage(byte[] msg)
    {
        if (ws.State == WebSocketState.Open)
        {

            await ws.Send(msg);

    }
    }
    public void SendMsg(JsonBase json)
    {
        SendWebSocketMessage(JsonManager.Instance().GetByte(json));
    }
    public void SendMsg(string str)
    {
        ws.Send(JsonManager.Instance().GetByte(str));
    }
    
    #region singleton
    private static WebSocketManager instance;
    public static WebSocketManager Instance() { 
        if (instance == null)
                instance = new WebSocketManager();
            return instance;
    }
    #endregion
}
