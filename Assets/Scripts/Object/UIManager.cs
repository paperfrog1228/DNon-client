using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject MapPanel;
    public GameObject NicknamePanel;
    [SerializeField] private GameObject classPanel;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        classPanel.SetActive(true);
    }
    #region Button
    public void BtnChemical() {
        OtherUserManager.Instance().InstantiatePlayer("Chemical");
        classPanel.SetActive(false);
    }
    #endregion
    #region singleton
    static UIManager instance;
    public static UIManager Instance() { 
            return instance;
    }
    #endregion
}
