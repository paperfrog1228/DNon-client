using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class RankingList : MonoBehaviour
{
    [SerializeField] GameObject rankItem;

    // Start is called before the first frame update
    void Start()
    {
        RefreshRankList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    public void RefreshRankList()
    {
        Debug.Log(APIClient.GetClient().CurrentChannel);
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        APIClient.GetClient().GetRanking(APIClient.GetClient().CurrentChannel).Then(list =>
        {
            int idx = 1;
            foreach (PlayerVO player in list)
            {
                GameObject newItem = Instantiate(rankItem);
                newItem.transform.SetParent(transform);
                RankItem item = newItem.GetComponent<RankItem>();
                item.PlayerName = player.playerName;
                item.Rank = idx;
                item.Score = player.highscore;
                idx++;
                if (idx > 10) break;
            }
        });
    }
}
