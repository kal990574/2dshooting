using System;
using UnityEngine;

[Serializable]
public class UserData
{
    public int highScore;

    public UserData()
    {
        highScore = 0;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static UserData FromJson(string json)
    {
        return JsonUtility.FromJson<UserData>(json);
    }
}