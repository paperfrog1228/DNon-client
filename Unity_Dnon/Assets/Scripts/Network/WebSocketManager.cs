using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HybridWebSocket;
using Sirenix;
using Sirenix.OdinInspector;
using System;
public class WebSocketManager{
    WebSocket ws;
    public Queue<byte[]> MessageQueue = new Queue<byte[]>();
    public void Connect (string url) {
        ws = WebSocketFactory.CreateInstance(url);
        ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");
            Debug.Log("WS state: " + ws.GetState().ToString());
        };
        ws.OnMessage += (byte[] msg) =>
        {
            MessageQueue.Enqueue(msg);
            Debug.Log("WS received message: " + Encoding.UTF8.GetString(msg));
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

    public void SendMsg(JsonBase json)
    {
        ws.Send(JsonManager.Instance().GetByte(json));
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
