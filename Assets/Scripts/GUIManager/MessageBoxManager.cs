﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoxManager : MonoBehaviour
{
    [SerializeField] Text messageTitle;
    [SerializeField] Text messageBody;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMessage(string title, string body)
    {
        messageTitle.text = title;
        messageBody.text = body;
    }

    public void Ok()
    {
        Destroy(gameObject);
    }
}
