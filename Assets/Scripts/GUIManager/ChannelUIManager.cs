using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelUIManager : MonoBehaviour
{
    [SerializeField] InputField playerNameField;

    [SerializeField] Button logoutButton;
    [SerializeField] Button userInfoButton;
    [SerializeField] Button toSignButton;
    [SerializeField] Button refreshButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        if (APIClient.GetClient().Signed)
        {
            playerNameField.text = APIClient.GetClient().SignedUserInfo.userName;
            logoutButton.gameObject.SetActive(true);
            userInfoButton.gameObject.SetActive(true);
            toSignButton.gameObject.SetActive(false);
        }
        else
        {
            logoutButton.gameObject.SetActive(false);
            userInfoButton.gameObject.SetActive(false);
            toSignButton.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
