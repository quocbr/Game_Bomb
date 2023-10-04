using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Singleton/GameSetting")]
public class GameSetting : SingletonScriptableObject<GameSetting>
{
    [SerializeField]
    private string _gameVersion = "0.0.0";
    public string GameVersiron {
        get { return _gameVersion; }
    }
    [SerializeField]
    private string _nickName = "Quoc";

    public string NickName
    {
        get
        {
            int value = Random.Range(0, 9999);
            return _nickName + value.ToString();
        }
    }
}
