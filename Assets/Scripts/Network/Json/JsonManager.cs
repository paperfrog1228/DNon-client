using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
/// <summary>
/// Json 자료형 변환 클래스
/// </summary>
public class JsonManager {
    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }
    public T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
    public byte[] GetByte(string s) {
        return Encoding.UTF8.GetBytes(s);
    }
    public byte[] GetByte(JsonBase j) {
        return Encoding.UTF8.GetBytes(ObjectToJson(j));
    }

    #region singleton
    private static JsonManager instance;
    public static JsonManager Instance() { 
        if (instance == null)
                instance = new JsonManager();
            return instance;
    }
    #endregion
}



