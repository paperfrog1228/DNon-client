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
    [SerializeField]
    Text scoreField;
    [SerializeField]
    Text classField;

    private string playerName;
    private int rank;
    private int score;
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
            rankField.text = rank.ToString() + ".";
        }
    }
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreField.text = score.ToString();
        }
    }
    public string PlayerClass
    {
        get { return playerClass; }
        set
        {
            playerClass = value;
            classField.text = playerClass;
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
