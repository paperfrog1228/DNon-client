using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ChannelVO
{
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
