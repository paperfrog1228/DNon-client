using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelList : MonoBehaviour
{
    public List<ChannelItem> channelList;

    [SerializeField]
    GameObject channelListContent = null;

    [SerializeField]
    GameObject channelItem = null;

    // Start is called before the first frame update
    void Start()
    {
        RefreshChannelList();
    }

    public void RefreshChannelList()
    {
        foreach (Transform child in channelListContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        APIClient.GetClient().GetChannelList().Then(channels =>
        {
            foreach (ChannelVO channel in channels)
            {
                GameObject newItem = Instantiate(channelItem);
                newItem.transform.SetParent(channelListContent.transform);
                ChannelItem channelInfo = newItem.GetComponent<ChannelItem>();
                string channelName = "Channel # " + channel.channelId.ToString();
                channelInfo.UpdateItem(channelName, channel.battlefield.battlefieldName, channel.participants.Count, channel.maximum);
            }
        });
    }
}
