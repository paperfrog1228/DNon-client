using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject MapPanel;
    public GameObject NicknamePanel;
    private void Awake()
    {
        instance = this;
    }
    public void OpenMap() {
        MapPanel.SetActive(true);
    }
    #region singleton
    static UIManager instance;
    public static UIManager Instance() { 
            return instance;
    }
    #endregion
}
