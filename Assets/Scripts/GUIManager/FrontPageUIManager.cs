using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontPageUIManager : MonoBehaviour
{
    [SerializeField] Canvas channelCanvas;
    [SerializeField] Canvas signCanvas;

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
    }

    public void ToChannelPage()
    {
        channelCanvas.gameObject.SetActive(true);
        signCanvas.gameObject.SetActive(false);
    }
}
