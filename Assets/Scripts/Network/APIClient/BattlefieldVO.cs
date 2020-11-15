using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattlefieldVO
{
    /// <summary>
    /// 맵 고유번호
    /// </summary>
    public int battlefieldId;
    /// <summary>
    /// 맵 이름
    /// </summary>
    public string battlefieldName;
    /// <summary>
    /// 맵 설명
    /// </summary>
    public string description;
    /// <summary>
    /// 맵 지형, 이 데이터는 아마 안쓸듯
    /// </summary>
    public int[][] geography;
}
