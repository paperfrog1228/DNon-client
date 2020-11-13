using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserVO
{
    /// <summary>
    /// User identifier
    /// </summary>
    public int userId;
    /// <summary>
    /// User name
    /// </summary>
    public string userName;
    /// <summary>
    /// User email, for login
    /// </summary>
    public string email;
    /// <summary>
    /// User password, for login
    /// </summary>
    public string userPassword;
    /// <summary>
    /// The date user registered
    /// </summary>
    public string dateRegistered;
}
