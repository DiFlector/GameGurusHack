using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneActivator : MonoBehaviour
{
    public string CutSceneName;
    private void OnTriggerEnter(Collider other)
    {
        CutsceneManager.Instance.StartCutscene(CutSceneName);
        Destroy(gameObject);
    }
}
