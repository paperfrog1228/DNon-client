using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;   // RestClient
using RSG;          // IPromise
using System;

/// <summary>
/// API Server에 Request를 보내 Promise 기반 Response를 반환해주는 클래스
/// 반환된 인스턴스의 Then() 메소드로 response에 대한 행동 정의, 
/// Catch() 메스드로 예외 상황 처리 가능.
/// </summary>
public class APIClient
{
    [SerializeField] string serverURL;

    private string JWT;

    private static APIClient uniqueInstance;

    private APIClient()
    {
        serverURL = "http://54.159.199.82:5000";
        JWT = "";
    }

    /// <summary>
    /// Get a singleton APIClient instance
    /// </summary>
    public static APIClient GetClient()
    {
        if (uniqueInstance == null)
            uniqueInstance = new APIClient();
        return uniqueInstance;
    }

    /// <summary>
    /// GET /ch
    /// </summary>
    /// <returns>response : List of channels</returns>
    public IPromise<ChannelVO[]> GetChannelList()
    {
        return RestClient.GetArray<ChannelVO>(serverURL + "/ch");
    }

    /// <summary>
    /// GET /ch/{channelId}
    /// </summary>
    /// <param name="channelId"></param>
    /// <returns>response : The channel</returns>
    public IPromise<ChannelVO> GetChannel(int channelId)
    {
        return RestClient.Get<ChannelVO>(serverURL + "/ch/" + channelId.ToString());
    }

    [System.Serializable]
    private class LoginInfo
    {
        public string email;
        public string password;
    }
    public IPromise<ResponseHelper> LoginUser(string email, string pwd)
    {
        LoginInfo user = new LoginInfo
        {
            email = email,
            password = pwd
        };

        return RestClient.Request(new RequestHelper
        {
            Uri = serverURL + "/auth/login",
            Method = "POST",
            Body = user
        });
    }




}