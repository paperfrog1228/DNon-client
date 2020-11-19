using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelList : MonoBehaviour
{
    public List<ChannelItem> channelList;

    [SerializeField]
    ChannelUIManager channelUIManager = null;

    [SerializeField]
    GameObject channelListContent = null;

    [SerializeField]
    GameObject channelItem = null;

    [SerializeField]
    InputField playerNameField = null;

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
                ChannelItem item = newItem.GetComponent<ChannelItem>();
                item.ChannelData = channel;

                newItem.GetComponent<Button>().onClick.AddListener(delegate { channelUIManager.EnterGame(channel.channelId, playerNameField.text); });
            }
            if (!APIClient.GetClient().Signed) playerNameField.Select();
        });
    }
}
