using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerPrefs : ScriptableObject
{
    private playerprefs _playerPrefs;

    public void Save()
    {
        string s = JsonUtility.ToJson(_playerPrefs);
        FileManager.WriteToFile("playerprefs.json", s);
    }

    public void Load()
    {
        string s;
        FileManager.LoadFromFile("playerprefs.json", out s);
        JsonUtility.FromJsonOverwrite(s, _playerPrefs);
    }

    public float GetVolume(string s)
    {
        switch(s)
        {
            case "master":
                return _playerPrefs.masterVol;
            case "sfx":
                return _playerPrefs.sfxVol;
            case "bgm":
                return _playerPrefs.bgmVol;
            default:
                return -999f;
        }
    }

    public void SetVolume(string s, float value)
    {
        switch (s)
        {
            case "master":
                _playerPrefs.masterVol = value;
                break;
            case "sfx":
                _playerPrefs.sfxVol = value;
                break;
            case "bgm":
                _playerPrefs.bgmVol = value;
                break;
            default:
                break;
        }
    }
}

[System.Serializable]
public class playerprefs
{
    public float masterVol = -6f;
    public float sfxVol = -6f;
    public float bgmVol = -6f;
}
