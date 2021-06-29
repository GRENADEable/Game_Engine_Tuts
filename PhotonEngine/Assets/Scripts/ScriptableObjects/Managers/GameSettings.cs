using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Settings", menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    #region Private Variables
    [SerializeField] private string _gameVersion = "0.0.0";
    public string MyGameVersion { get { return _gameVersion; } }

    public string myNickname = "Punfish";
    #endregion
}