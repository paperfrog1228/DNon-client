using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class APIClientTester : MonoBehaviour
{
    public void APITester()
    {
        APIClient.GetClient().GetChannelList().Then(channels =>
        {
            foreach (ChannelVO channel in channels)
            {
                Debug.Log(channel.channelId);
            }
        });
    }
}