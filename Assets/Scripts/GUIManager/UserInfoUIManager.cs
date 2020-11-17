using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUIManager : MonoBehaviour
{
    [SerializeField] Text userNameText;
    [SerializeField] Text emailText;
    [SerializeField] Text dateRegisteredText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        if (APIClient.GetClient().Signed)
        {
            UserVO user = APIClient.GetClient().SignedUserInfo;

            userNameText.text = user.userName;
            emailText.text = user.email;
            dateRegisteredText.text = user.dateRegistered;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
