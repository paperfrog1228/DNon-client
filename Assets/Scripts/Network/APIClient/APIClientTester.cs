using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class APIClientTester : MonoBehaviour
{
    private APIClient apiClient = APIClient.GetClient();

    public void PWDTester()
    {
        
    }

    public void APILoginTester()
    {
        apiClient.LoginUser("test01@gmail.com", "1234").Catch(err =>
        {
            if (err.Message.Contains("401"))
            {
                Debug.Log("Wrong email or password");
            }
            else
            {
                Debug.Log(err.Message);
            }
        }).Finally(() =>
        {
            if (APIClient.GetClient().Signed)
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }

    public void APILogoutTester()
    {
        apiClient.LogoutUser().Catch(err =>
        {
            Debug.Log(err.Message);
        }).Finally(() =>
        {
            Debug.Log(APIClient.GetClient().Signed);
        });
    }

    public void APIPostTester()
    {
        string playerName = GameObject.Find("PlayerName").GetComponent<UnityEngine.UI.Text>().text;
        if (playerName.Equals("")) { playerName = "DNON"; }
        apiClient.PostPlayer(1, playerName).Then(res =>
        {
            Debug.Log(res.message);
        }).Catch(err =>
        {
            Debug.Log(err.Message);
        });
    }

    public void APIGetTester()
    {
        apiClient.GetRanking(1).Then(players =>
        {
            int idx = 0;
            foreach(PlayerVO player in players)
            {
                idx++;
                Debug.Log(idx + ":" + player.playerName + "(" +  player.highscore + ")");
            }
        });
    }

    public void APIPutTester()
    {
        apiClient.UpdatePlayer(1, 4, 9999).Then(res =>
         {
             Debug.Log(res.message);
         });
    }

    public void APIDeleteTester()
    {
        apiClient.DeleteUserSavedInfo("test01", 5).Then(saved =>
        {
            Debug.Log(saved.score);
        }).Catch(err =>
        {
            Debug.Log(err.Message);
        });


    }

    /// <summary>
    /// Test more than 2 methods
    /// </summary>
    public void APISequenceTester()
    {
        //int signedId = 0;
        //apiClient.LoginUser("test02@gmail.com", "1234").Catch(err =>
        //{
        //    Debug.LogError(err.Message);
        //}).Finally(() =>
        //{
        //    apiClient.PostSavedInfo(1, "anteater", 1052, 1, 2, new string[] { "Everthing" }).Then(res =>
        //    {
        //        signedId = res.data.savedId;
        //        Debug.Log(signedId);
        //    }).Catch(err =>
        //    {
        //        Debug.LogError(err.Message);
        //    }).Finally(() =>
        //    {
        //        apiClient.DeleteUserSavedInfo("test02", signedId).Then(saved =>
        //        {
        //            Debug.Log(saved.playerName + ":" + saved.score);
        //        }).Catch(err =>
        //        {
        //            Debug.LogError(err.Message);
        //        });
        //    });
        //});

        int id = 0;
        string otp = "";
        apiClient.PostSavedInfo(1, "savedTester", 555, 1, 1, new string[] { "Nothing" }).Then(res =>
        {
            id = res.data.savedId;
            otp = res.data.otp;
            Debug.Log(id + " : " + otp);
        }).Catch(err =>
        {
            Debug.Log(err.Message);
        }).Finally(() =>
        {
            apiClient.DeleteGuestSavedInfo(id, otp).Then(saved =>
            {
                Debug.Log(saved.playerName + ":" + saved.score);
            }).Catch(err =>
            {
                Debug.Log(err.Message);
            });
        });
    }
}