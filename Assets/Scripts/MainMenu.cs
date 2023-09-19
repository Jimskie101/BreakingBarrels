using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] RawImage backgroundIcons;
    [SerializeField] GameObject mainMenuObjects;
    [SerializeField] GameObject levelObjects;
    [SerializeField] GameObject backButton;



    private void Update()
    {
        backgroundIcons.uvRect = new Rect(backgroundIcons.uvRect.position + new Vector2(0.5f,0 ) * Time.deltaTime, backgroundIcons.uvRect.size);
    }

    public void RegularMode() {AudioManager.Instance.Play("Button"); Mode.inCasualMode = false; OpenLevelScreen();}
    public void CasualMode() {AudioManager.Instance.Play("Button"); Mode.inCasualMode = true; OpenLevelScreen();}
    

    public void OpenLevelScreen()
    {
        mainMenuObjects.SetActive(false);
        levelObjects.SetActive(true);
        backButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.Play("Button");
        mainMenuObjects.SetActive(true);
        levelObjects.SetActive(false);
        backButton.SetActive(false);

    }
    


}
