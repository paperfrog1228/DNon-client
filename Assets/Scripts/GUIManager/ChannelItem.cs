using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ChannelItem : MonoBehaviour
{
    [SerializeField] Text channelName;
    [SerializeField] Text battlefieldName;
    [SerializeField] Text playerNumber;

    private ChannelVO channelData;

    public ChannelVO ChannelData
    {
        get { return channelData; }
        set
        {
            channelData = value;
            channelName.text = "Channel # " + channelData.channelId;
            battlefieldName.text = channelData.battlefield.battlefieldName;
            playerNumber.text = channelData.participants.Count.ToString() + "/" + channelData.maximum.ToString();
        }
    }
}