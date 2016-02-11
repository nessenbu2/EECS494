using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{
    public void LoadLevel(string scene)
    {
        Application.LoadLevel(scene);
    }
}
