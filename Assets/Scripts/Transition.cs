using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    // These are the Scenes. Make sure to set them in the Inspector window.
    [SerializeField] List<string> sceneNameList = new List<string>();

    int currentSceneIndex;
    string currentSceneName;
    int numberOfScenes;

    float sceneStartTime;

    ParticipantResponses pr;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sceneStartTime = Time.realtimeSinceStartup;
        numberOfScenes = sceneNameList.Count;

        pr = GetComponent<ParticipantResponses>();
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
            print("next");
            //ExperimentLogger.Instance.log(Scenes[ActiveScene].name + " ended after " + (Time.realtimeSinceStartup - sceneStartTime) + "s", Scenes[ActiveScene].name, Time.realtimeSinceStartup - sceneStartTime);
            ExperimentLogger.Instance.log(SceneManager.GetActiveScene().name + " ended after " + (Time.realtimeSinceStartup - sceneStartTime) + "s");
            Dictionary<string, int> dict = pr.participantResponseDict;
            print(dict);
            pr.NewDict();
            print(dict);
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
            print("Prev");
            //ExperimentLogger.Instance.log(Scenes[ActiveScene].name + " ended after " + (Time.realtimeSinceStartup - sceneStartTime) + "s", Scenes[ActiveScene].name, Time.realtimeSinceStartup - sceneStartTime);
            ExperimentLogger.Instance.log(currentSceneName + " ended after " + (Time.realtimeSinceStartup - sceneStartTime) + "s");
            SceneManager.LoadScene(sceneNameList[currentSceneIndex - 1]);
        }
        else
        {
            print("Reached First Scene");
        }
    }

    private void OnApplicationQuit()
    {
        ExperimentLogger.Instance.log("Application quit");
    }
}
