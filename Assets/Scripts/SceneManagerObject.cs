using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerObject : MonoBehaviour
{
    public string SceneName;

    public void LoadScene(string sceneName)
    {
        SceneTransition.SwitchToScene(sceneName);
    }
}
