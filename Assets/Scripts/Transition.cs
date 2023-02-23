using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    // These are the Scenes. Make sure to set them in the Inspector window.
    [SerializeField] List<string> sceneNameList = new List<string>();
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject prevButton;

    int currentSceneIndex;
    string currentSceneName;
    int numberOfScenes;

    float sceneStartTime;


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sceneStartTime = Time.realtimeSinceStartup;
        numberOfScenes = sceneNameList.Count;
    }

    void OnGUI()
    {
        // Return the current Active Scene in order to get the current Scene name.
        Scene scene = SceneManager.GetActiveScene();
        currentSceneName = scene.name;
        currentSceneIndex = sceneNameList.IndexOf(currentSceneName);
    }

    public void GoToNext()
    {
        if (currentSceneName != sceneNameList[numberOfScenes-1])
        {
            print("Next slide");
            var dict = ExperimentLogger.Instance.pr.TrackChanges();
            ExperimentLogger.Instance.log(currentSceneName + " logged in after " + (Time.realtimeSinceStartup - sceneStartTime) + "s", currentSceneName, dict);

            SceneManager.LoadScene(sceneNameList[currentSceneIndex+1]);
        }
        else
        {
            print("Reached Last Scene");
        }

    }

    public void GoToPrev()
    {
        if (currentSceneName != sceneNameList[0])
        {
            print("Previous slide");
            var dict = ExperimentLogger.Instance.pr.TrackChanges();
            ExperimentLogger.Instance.log(currentSceneName + " logged in after " + (Time.realtimeSinceStartup - sceneStartTime) + "s", currentSceneName, dict);

            SceneManager.LoadScene(sceneNameList[currentSceneIndex - 1]);
        }
        else
        {
            print("Reached First Scene");
        }
    }

    private void OnApplicationQuit()
    {
        //ExperimentLogger.Instance.log("Application quit");
    }
}
