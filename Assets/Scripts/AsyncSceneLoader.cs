using System.Collections;
using DefaultNamespace;
using DefaultNamespace.SingletonUtility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncSceneLoader : MonoBehaviourSingletonPersistent<AsyncSceneLoader>
{
    [SerializeField] private int sceneSequenceID;

    // Start is called before the first frame update
    void Start()
    {
        //LoadSceneWithSequenceID(sceneSequenceID);
    }

    public void LoadSceneWithSequenceID(int sceneSequenceID)
    {
        StartCoroutine(LoadYourAsyncScene(sceneSequenceID));
    }

    IEnumerator LoadYourAsyncScene(int sceneSequenceID)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneSequenceID);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
    public void TraverseToSceneWithSequenceID(int sourceSceneSequenceID, int destinationSceneSequenceID)
    {
        SceneManager.UnloadSceneAsync(sourceSceneSequenceID);
        LoadYourAsyncScene(destinationSceneSequenceID);
    }
}
