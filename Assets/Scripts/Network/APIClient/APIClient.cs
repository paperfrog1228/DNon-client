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

    /// <summary>
    /// Does the API Client have JWT token?
    /// </summary>
    public bool Signed
    {
        get { return !JWT.Equals("0"); }
    }

    /// <summary>
    /// Response not defined as a data type
    /// </summary>
    [System.Serializable]
    public class UniversalServerResponse
    {
        [System.Serializable]
        public class SavedResult
        {
            public int savedId;
            public string otp;
        }
        public string status;
        public string message;
        public string Authorization;
        public SavedResult data;
    }

    private APIClient()
    {
        //serverURL = "http://127.0.0.1:5000";
        serverURL = "http://54.159.199.82:5000";
        JWT = "0";
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
    /// Set Authorization header with bearer + JWT
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, string> SetAuthHeader()
    {
        return new Dictionary<string, string>
        {
            { "Authorization", "Bearer " + JWT }
        };
    }

    #region /user APIs

    /// <summary>
    /// GET /user
    /// </summary>
    /// <returns></returns>
    public IPromise<UserVO[]> GetUsers()
    {
        return RestClient.GetArray<UserVO>(serverURL + "/user/");
    }

    /// <summary>
    /// POST /user
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    public IPromise<ResponseHelper> PostUser(string name, string email, string pwd)
    {
        UserVO newUser = new UserVO
        {
            userName = name,
            email = email,
            userPassword = pwd
        };

        return RestClient.Post(serverURL + "/user/", newUser);
    }

    /// <summary>
    /// GET /user/{userName}
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public IPromise<UserVO> GetUser(string userName)
    {
        return RestClient.Get<UserVO>(serverURL + "/user/" + userName);
    }

    /// <summary>
    /// DELETE /user/{userName}
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public IPromise<ResponseHelper> DeleteUser(string userName)
    {
        return RestClient.Delete(new RequestHelper
        {
            Uri = serverURL + "/user/" + userName,
            Method = "DELETE",
            Headers = SetAuthHeader()
        });
    }

    /// <summary>
    /// DELETE /user/{userName}/saved-info/{savedId}
    /// Delete & Get svaed data
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="savedId"></param>
    /// <returns></returns>
    public IPromise<SavedInfoVO> DeleteUserSavedInfo(string userName, int savedId)
    {
        return RestClient.Request<SavedInfoVO>(new RequestHelper
        {
            Uri = serverURL + "/user/" + userName + "/saved-info/" + savedId.ToString(),
            Method = "DELETE",
            Headers = SetAuthHeader()
        });
    }

    #endregion

    #region /ch APIs

    /// <summary>
    /// GET /ch
    /// </summary>
    /// <returns>response : List of channels</returns>
    public IPromise<ChannelVO[]> GetChannelList()
    {
        return RestClient.GetArray<ChannelVO>(serverURL + "/ch/");
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

    /// <summary>
    /// GET /ch/{channelId}/participants
    /// </summary>
    /// <param name="channelId"></param>
    /// <returns></returns>
    public IPromise<PlayerVO[]> GetPlayers(int channelId)
    {
        return RestClient.GetArray<PlayerVO>(serverURL + "/ch/" + channelId.ToString() + "/participants/");
    }

    /// <summary>
    /// POST /ch/{channelId}/participants
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="playerName"></param>
    /// <returns></returns>
    public IPromise<ResponseHelper> PostPlayer(int channelId, string playerName)
    {
        return RestClient.Post(serverURL + "/ch/" + channelId.ToString() + "/participants/", new PlayerVO
        {
            playerName = playerName
        });
    }

    /// <summary>
    /// DELETE /ch/{channelId}/participants/{playerId}
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="playerId"></param>
    /// <returns></returns>
    public IPromise<ResponseHelper> DeletePlayer(int channelId, int playerId)
    {
        return RestClient.Delete(serverURL + "/ch/" + channelId + "/participants/" + playerId);
    }

    /// <summary>
    /// PUT /ch/{channelId}/participants/{playerId}
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="playerId"></param>
    /// <param name="score"></param>
    /// <returns></returns>
    public IPromise<ResponseHelper> UpdatePlayer(int channelId, int playerId, int score)
    {
        return RestClient.Put(serverURL + "/ch/" + channelId + "/participants/" + playerId, new PlayerVO
        {
            highscore = score
        });
    }

    /// <summary>
    /// GET /ch/{channelId}/ranking
    /// </summary>
    /// <param name="channelId"></param>
    /// <returns></returns>
    public IPromise<PlayerVO[]> GetRanking(int channelId)
    {
        return RestClient.GetArray<PlayerVO>(serverURL + "/ch/" + channelId.ToString() + "/ranking/");
    }

    #endregion

    #region /saved-info APIs

    /// <summary>
    /// POST /saved-info
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="playerName"></param>
    /// <param name="score"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public IPromise<UniversalServerResponse> PostSavedInfo(int channelId, string playerName, int score, int x, int y, string[] items)
    {
        SavedInfoVO newSaved = new SavedInfoVO
        {
            channelId = channelId,
            playerName = playerName,
            score = score,
            location = new SavedInfoVO.LocationVO
            {
                x = x, y = y
            },
            items = items
        };

        string uri = serverURL + "/saved-info/";

        if (Signed)
        {
            return RestClient.Post<UniversalServerResponse>(new RequestHelper
            {
                Uri = uri,
                Body = newSaved,
                Headers = SetAuthHeader()
            });
        }
        else
        {
            return RestClient.Post<UniversalServerResponse>(uri, newSaved);
        }
    }

    public IPromise<SavedInfoVO> DeleteGuestSavedInfo(int savedId, string otp)
    {
        return RestClient.Request<SavedInfoVO>(new RequestHelper
        {
            Uri = serverURL + "/saved-info/" + savedId.ToString() + "/" + otp,
            Method = "DELETE",
        });
    }

    #endregion

    #region /auth APIs

    [System.Serializable]
    private class LoginInfo
    {
        public string email;
        public string password;
    }

    /// <summary>
    /// POST /auth/login
    /// Login 수행, Catch와 Finally 메소드로 예외 처리 및 이후 행동 정의 가능.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    public IPromise LoginUser(string email, string pwd)
    {
        LoginInfo user = new LoginInfo
        {
            email = email,
            password = pwd
        };

        return RestClient.Post<UniversalServerResponse>(serverURL + "/auth/login", user).Then(res =>
        {
            this.JWT = res.Authorization;
        });
    }

    /// <summary>
    /// POST /auth/logout
    /// Logout 수행, JWT 토큰 파기
    /// </summary>
    /// <returns></returns>
    public IPromise LogoutUser()
    {
        if (Signed)
        {
            return RestClient.Post(new RequestHelper
            {
                Uri = serverURL + "/auth/logout",
                Headers = SetAuthHeader()
            }).Then(res =>
            {
                this.JWT = "0";
            });
        }
        else
        {
            return null;
        }
    }
    #endregion

}