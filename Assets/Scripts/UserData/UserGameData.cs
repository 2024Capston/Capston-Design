using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 임시로 레지스트리에 현재 진행 중인 Chapter를 저장하도록 만들었습니다.
 추후 SteamCloud 사용 시 해당 부분을 이용하도록 변경할 예정이고
 저장할 데이터들도 변경 될 예정입니다.
 현재 데이터는 예시 용으로 만들었다고 생각하면 됩니다.
 */

/// <summary>
/// Player의 game 진행 정보를 담고 있는 Class
/// </summary>
public class UserGameData : IUserData
{
    public int ProgessChapter { get; set; }
    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        ProgessChapter = 0;
    }

    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            ProgessChapter = PlayerPrefs.GetInt("ProgessChapter");
            result = true;
            Logger.Log($"Progress Chapter : {ProgessChapter}");
        }
        catch (Exception e)
        {
            Logger.Log($"Load failed. (" + e.Message + ")");
        }

        return result;
    }

    public bool SaveData()
    {
        Logger.Log($"{GetType()}::SaveData");

        bool result = false;

        try
        {
            PlayerPrefs.SetInt("ProgessChapter", ProgessChapter);
            result = true;
        }
        catch (Exception e)
        {
            Logger.Log($"Save failed. (" + e.Message + ")");
        }

        return result;
    }

    
}