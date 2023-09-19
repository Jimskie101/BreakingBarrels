using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RGVA;
using Sirenix.OdinInspector;

public class LevelsAvailable : Singleton<LevelsAvailable>
{
    public int unlockedLevelsRegular = 0;
    public int unlockedLevelsCasual = 0;

    [Button]
    private void Test()
    {
        SaveManager.Instance.OpenLevelsRegular = unlockedLevelsRegular;
        SaveManager.Instance.OpenLevelsCasual = unlockedLevelsCasual;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        unlockedLevelsRegular = SaveManager.Instance.OpenLevelsRegular == 0 ? 1 : SaveManager.Instance.OpenLevelsRegular;
        unlockedLevelsCasual = SaveManager.Instance.OpenLevelsCasual == 0 ? 1 : SaveManager.Instance.OpenLevelsCasual;



        for(int i = 0; i < transform.childCount ; i++ )
        {
            transform.GetChild(i).GetComponent<Button>().interactable = false;

        }
        



        int levelCount = Mode.inCasualMode ? unlockedLevelsCasual : unlockedLevelsRegular;
        for(int i = 0; i < levelCount ; i++ )
        {
            transform.GetChild(i).GetComponent<Button>().interactable = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UnlockedNewLevel(int currentLevel)
    {
        int levelCount = Mode.inCasualMode ? unlockedLevelsCasual : unlockedLevelsRegular;

        if(levelCount < currentLevel)
        {
            if(!Mode.inCasualMode) SaveManager.Instance.OpenLevelsRegular = currentLevel;
            else SaveManager.Instance.OpenLevelsCasual = currentLevel;
        }
        
    }
}
