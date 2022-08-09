using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public RectTransform loadingOverlay;

    private AsyncOperation _sceneLoadingOperation;

    private void Start()
    {
        loadingOverlay.gameObject.SetActive(false);

        _sceneLoadingOperation = SceneManager.LoadSceneAsync(Scenes.MAIN_SCENE);
        _sceneLoadingOperation.allowSceneActivation = false;
    }

    public void LoadScene()
    {
        loadingOverlay.gameObject.SetActive(true);

        _sceneLoadingOperation.allowSceneActivation = true;
    }
}
