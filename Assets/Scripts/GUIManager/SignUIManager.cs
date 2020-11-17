using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUIManager : MonoBehaviour
{
    [SerializeField] Text signCanvasText;

    [SerializeField] InputField emailInput;
    [SerializeField] InputField pwdInput;
    [SerializeField] InputField pwdConfirmInput;
    [SerializeField] InputField userNameInput;

    [SerializeField] Button signButton;

    [SerializeField] GameObject pwdConfirmLayout;
    [SerializeField] GameObject userNameLayout;

    [SerializeField] MessageBoxManager messageBoxOriginal;

    private MessageBoxManager messageBox;

    private bool isSignIn = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        emailInput.text = pwdInput.text = pwdConfirmInput.text = userNameInput.text = "";
        isSignIn = false;
        ToggleSign();
    }

    // Update is called once per frame
    void Update()
    {
        if (emailInput.isFocused)
        {
            if (Input.GetKeyUp(KeyCode.Tab)) pwdInput.Select();
        }
        if (pwdInput.isFocused)
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                if (!isSignIn) pwdConfirmInput.Select();
                else emailInput.Select();
            }
        }
        if (pwdConfirmInput.isFocused)
        {
            if (Input.GetKeyUp(KeyCode.Tab)) userNameInput.Select();
        }
        if (userNameInput.isFocused)
        {
            if (Input.GetKeyUp(KeyCode.Tab)) emailInput.Select();
        }
    }

    public void ToggleSign()
    {
        if (isSignIn)   // To sign up
        {
            signCanvasText.text = "Sign Up";
            pwdConfirmLayout.SetActive(true);
            userNameLayout.SetActive(true);
            signButton.GetComponentInChildren<Text>().text = "Sign In";
            isSignIn = false;
        }
        else            // To sign in
        {
            signCanvasText.text = "Sign In";
            pwdConfirmLayout.SetActive(false);
            userNameLayout.SetActive(false);
            signButton.GetComponentInChildren<Text>().text = "Sign Up";
            isSignIn = true;
        }
        emailInput.Select();
    }

    public void Sign()
    {
        if (emailInput.text.Equals(""))
        {
            PopUpMessage("Error!", "Email을 입력해 주세요.");
            return;
        }
        if (pwdInput.text.Equals(""))
        {
            PopUpMessage("Error!", "비밀번호를 입력해 주세요.");
            return;
        }

        if (isSignIn)
        {
            APIClient.GetClient().LoginUser(emailInput.text, pwdInput.text).Then(() =>
            {
                gameObject.GetComponentInParent<FrontPageUIManager>().ToChannelPage();
            }).Catch(err =>
            {
                if (err.Message.Contains("401"))
                {
                    PopUpMessage("Error", "Email 혹은 비밀번호가 바르지 않습니다.");
                }
                else
                {
                    PopUpMessage("Error", "예상치 못한 에러가 발생했습니다. 관리자에게 문의해 주세요. \n" + err.Message);
                }
            });
        }
        else
        {
            if (pwdConfirmInput.text.Equals(""))
            {
                PopUpMessage("Error!", "비밀번호를 한 번 더 입력해 주세요.");
                return;
            }
            if (userNameInput.text.Equals(""))
            {
                PopUpMessage("Error!", "사용자 이름을 입력해 주세요.");
                return;
            }
            APIClient.GetClient().PostUser(userNameInput.text, emailInput.text, pwdInput.text).Then(res =>
            {
                gameObject.GetComponentInParent<FrontPageUIManager>().ToChannelPage();
                PopUpMessage("회원가입", "성공적으로 가입되었습니다.");
            }).Catch(err =>
            {
                if (err.Message.Contains("409"))
                {
                    PopUpMessage("Error", "이미 존재하는 계정입니다.");
                }
                else
                {
                    PopUpMessage("Error", "예상치 못한 에러가 발생했습니다. 관리자에게 문의해 주세요. \n" + err.Message);
                }
            });
        }

    }

    private void PopUpMessage(string title, string body)
    {
        messageBox = Instantiate(messageBoxOriginal, gameObject.GetComponentInParent<FrontPageUIManager>().transform);
        messageBox.SetMessage(title, body);
    }
}
