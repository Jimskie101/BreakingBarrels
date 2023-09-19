using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using RGVA;

public class SaveManager : Singleton<SaveManager>
{
    public int OpenLevelsRegular {get {return PlayerPrefs.GetInt(nameof(OpenLevelsRegular), 0); }  set {PlayerPrefs.SetInt(nameof(OpenLevelsRegular), value); } }
    public int OpenLevelsCasual {get {return PlayerPrefs.GetInt(nameof(OpenLevelsCasual), 0); }  set {PlayerPrefs.SetInt(nameof(OpenLevelsCasual), value); } }
}
