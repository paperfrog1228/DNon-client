using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ChannelItem : MonoBehaviour
{
    public Text channelName;
    public Text battlefieldName;
    public Text playerNumber;

    public void UpdateItem(string ch, string bf, int cur, int max)
    {
        channelName.text = ch;
        battlefieldName.text = bf;
        playerNumber.text = cur.ToString() + "/" + max.ToString();
    }
}