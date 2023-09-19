using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RGVA;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{

    public void RestartScene() 
    { 
        AudioManager.Instance.Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    public void LevelLoader( RectTransform buttonTransform) 
    { 
        AudioManager.Instance.Play("Button");
        SceneManager.LoadScene(buttonTransform.gameObject.name); 
    }
    public void NextLevel() 
    { 
        AudioManager.Instance.Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void Tutorial() 
    { 
        AudioManager.Instance.Play("Button"); 
        Mode.inCasualMode = false;
        SceneManager.LoadScene(1);
    }

}
