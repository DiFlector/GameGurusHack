using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    private static bool shouldPlayAnimation = false;
    private static bool switchStart = false;
    private static string sceneName;

    private Animator _animator;
    private AsyncOperation loadingScreenOperation;

    public Slider loadingProgressBar;
    public TextMeshProUGUI loadingText;

    private bool confirm;

    public static void SwitchToScene(string SceneName)
    {
        sceneName = SceneName;
        instance.loadingScreenOperation = SceneManager.LoadSceneAsync("loadingScene");
        instance.loadingScreenOperation.allowSceneActivation = false;
        if(SceneManager.GetActiveScene().buildIndex != 1)
            instance._animator.SetTrigger("Start");
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _animator = GetComponent<Animator>();
        confirm = false;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            switchStart = true;
            loadingScreenOperation = SceneManager.LoadSceneAsync(sceneName);
            loadingScreenOperation.allowSceneActivation = false;

        }
        else
        {
            if (shouldPlayAnimation)
                _animator.SetTrigger("End");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (switchStart)
        {
            shouldPlayAnimation = true;
            if (confirm && Input.GetKeyDown(KeyCode.Space)) // and Space
            {
                loadingScreenOperation.allowSceneActivation = true;
                switchStart = false;
            }

            if (!confirm) {
                if (loadingProgressBar.value == 0.9f)
                {
                    loadingProgressBar.value = 1f;
                    loadingText.text = "Для продолжения нажмите SPACE";
                    confirm = true;
                }
                else
                {
                    loadingProgressBar.value = loadingScreenOperation.progress;
                }
            }
        }

    }

    public void LoadingIsDone()
    {
        instance.loadingScreenOperation.allowSceneActivation = true;
    }
}
