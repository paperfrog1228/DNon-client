using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;   // RestClient
using System;

[Serializable]
public class Channel
{
    public int channelId;
    public int maximum;
}

public class APIClient : MonoBehaviour
{
    private string serverURL = "http://54.159.199.82:5000";

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void GetChannels()
    {
        RestClient.Get(serverURL + "/ch").Then(res =>
        {
            Debug.Log(res.Text);
        });
    }
}
