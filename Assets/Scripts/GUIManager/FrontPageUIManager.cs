using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontPageUIManager : MonoBehaviour
{
    [SerializeField] Canvas channelCanvas;
    [SerializeField] Canvas signCanvas;
    [SerializeField] Canvas userInfoCavas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToSignPage()
    {
        channelCanvas.gameObject.SetActive(false);
        signCanvas.gameObject.SetActive(true);
        userInfoCavas.gameObject.SetActive(false);
    }

    public void ToChannelPage()
    {
        channelCanvas.gameObject.SetActive(true);
        signCanvas.gameObject.SetActive(false);
        userInfoCavas.gameObject.SetActive(false);
    }

    public void ToUserInfoPage()
    {
        channelCanvas.gameObject.SetActive(false);
        signCanvas.gameObject.SetActive(false);
        userInfoCavas.gameObject.SetActive(true);
    }

    public void Logout()
    {
        if (APIClient.GetClient().Signed)
        {
            APIClient.GetClient().LogoutUser().Then(() =>
            {
                signCanvas.gameObject.SetActive(false);
                channelCanvas.gameObject.SetActive(false);
                channelCanvas.gameObject.SetActive(true);
            });
        }
    }
}
