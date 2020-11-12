using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ChannelVO
{
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

    /// <summary>
    /// Channel Identifier
    /// </summary>
    public int channelId;
    /// <summary>
    /// Map Info
    /// </summary>
    public BattlefieldVO battlefield;
    /// <summary>
    /// Maximum # of players
    /// </summary>
    public int maximum;
    /// <summary>
    /// Current players list
    /// </summary>
    public List<PlayerVO> participants;
    /// <summary>
    /// Current player ranking list
    /// </summary>
    public List<PlayerVO> ranking;
}
