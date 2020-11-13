
[System.Serializable]
public class PlayerVO
{
    /// <summary>
    /// Player identifier
    /// </summary>
    public int playerId;
    /// <summary>
    /// Player name, not duplicated in channel
    /// </summary>
    public string playerName;
    /// <summary>
    /// Guest or signed
    /// </summary>
    public bool guest;
    /// <summary>
    /// Highest score player obtained
    /// </summary>
    public int highscore;
    /// <summary>
    /// The date player entered this channel
    /// </summary>
    public string dateEntered;
}