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

    private bool isSignIn = false;

    // Start is called before the first frame update
    void Start()
    {
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
}
