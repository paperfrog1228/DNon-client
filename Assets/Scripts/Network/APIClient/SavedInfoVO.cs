using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedInfoVO
{
    [System.Serializable]
    public class LocationVO
    {
        public int x;
        public int y;
    }

    /// <summary>
    /// Whether this player is guest or signed
    /// </summary>
    public bool guest;
    /// <summary>
    /// Unique saved-info identifier
    /// </summary>
    public int savedId;
    /// <summary>
    /// One-time password needed when loading the info
    /// </summary>
    public string guestPassword;
    /// <summary>
    /// Identifier for channel that game belongs
    /// </summary>
    public int channelId;
    /// <summary>
    /// The player's name
    /// </summary>
    public string playerName;
    /// <summary>
    /// The point that player gained till saves the game info
    /// </summary>
    public int score;
    /// <summary>
    /// The location player saved this data and quit the gmae.
    /// </summary>
    public LocationVO location;
    /// <summary>
    /// List of items the player has
    /// </summary>
    public string[] items;
    /// <summary>
    /// The date player saved this game
    /// </summary>
    public string dateSaved;
}
