using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelUIManager : MonoBehaviour
{
    [SerializeField] InputField playerNameField;

    // Start is called before the first frame update
    void Start()
    {
        playerNameField.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
