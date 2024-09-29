using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public GameObject cameraPrefab;
    //public GameObject objectToInstantiate;

    void Start()
    {
        AsyncOperationHandle<SceneInstance> handler = Addressables.LoadSceneAsync(sceneName);
        handler.Completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        Instantiate(cameraPrefab);
    }
}
