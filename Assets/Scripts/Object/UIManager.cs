using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    public GameObject NicknamePanel;
    [SerializeField] private GameObject classPanel;
    [SerializeField] private GameObject DiePanel;
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
    public void SetActiveDiepanel() {
        DiePanel.SetActive(true);
    }

    public void BtnExit()
    {
        APIClient.GetClient().DeletePlayer().Then(res =>
        {
            SceneManager.LoadScene("#1_Front");
        });
    }
    #endregion
    #region singleton
    static UIManager instance;
    public static UIManager Instance() { 
            return instance;
    }
    #endregion
}
