using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{
    public void LoadLevel(string scene)
    {
        Application.LoadLevel(scene);
    }

    public void LoadNextLevel()
    {
        switch (Application.loadedLevelName)
        {
            case "Level_0":
                LoadLevel("Level_1");
                break;
            case "Level_1":
                LoadLevel("Level_2");
                break;
            case "Level_2":
                LoadLevel("Level_3");
                break;
            case "Level_3":
                LoadLevel("Level_4");
                break;
            case "Level_4":
                LoadLevel("Level_5");
                break;
            case "Level_5":
                LoadLevel("Level_6");
                break;
            case "Level_6":
                LoadLevel("Level_7");
                break;
            case "Level_7":
                LoadLevel("Level_8");
                break;
            case "Level_8":
                LoadLevel("Level_9");
                break;
            default :
                print("next level for " + Application.loadedLevelName +
                        " not found. Returning to Menu");
                LoadLevel("Menu");
                break;
        }
    }

    public void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
