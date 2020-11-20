using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankItem : MonoBehaviour
{
    [SerializeField]
    Text playerNameField;
    [SerializeField]
    Text rankField;

    private string playerName;
    private int rank;
    private string playerClass;

    public string PlayerName
    {
        get { return playerName; }
        set
        {
            playerName = value;
            playerNameField.text = playerName;
        }
    }
    public int Rank
    {
        get { return rank; }
        set
        {
            rank = value;
            rankField.text = rank.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
