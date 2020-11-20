using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class ChannelUIManager : MonoBehaviour
{
    [SerializeField] FrontPageUIManager frontPageUIManager;

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
            playerNameField.text = "";
            logoutButton.gameObject.SetActive(false);
            userInfoButton.gameObject.SetActive(false);
            toSignButton.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    public void EnterGame(int channelId, string playerName)
    {
        if (playerName.Equals(""))
        {
            frontPageUIManager.PopUpMessage("Error", "이름을 입력해 주세요.");
            return;
        }

        APIClient.GetClient().PostPlayer(channelId, playerName).Then(res =>
        {
            SceneManager.LoadScene("#2_Game");
        }).Catch(err =>
        {
            string msg = err.Message;
            if (msg.Contains("404")) frontPageUIManager.PopUpMessage("Error", "채널이 존재하지 않습니다.");
            else if (msg.Contains("409")) frontPageUIManager.PopUpMessage("Error", "채널에 해당 이름이 이미 존재합니다.");
            else if (msg.Contains("422")) frontPageUIManager.PopUpMessage("Error", "이름에는 영문자, 숫자 외의 문자는 사용할 수 없습니다.");
            else if (msg.Contains("423")) frontPageUIManager.PopUpMessage("Error", "채널이 가득 찼습니다.");
            else frontPageUIManager.PopUpMessage("Error", "예상치 못한 에러가 발생했습니다. 관리자에게 문의해 주세요. \n" + msg);
        });
    }
}
