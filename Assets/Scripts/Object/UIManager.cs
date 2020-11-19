using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    public GameObject NicknamePanel;
    [SerializeField] private GameObject classPanel;
    private void Awake()
    {
        instance = this;
        loadingPanel.SetActive(true);
    }
    private void Start()
    {
        classPanel.SetActive(true);
    }
    public void SetFalseLoadingPanel() {
        loadingPanel.SetActive(false);
    }
    #region Button
    public void BtnChemical() {
        OtherUserManager.Instance().SetPlayer("Chemical");
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
