using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class ExperimentLogger : MonoBehaviour
{
    int hashValue;
    FileStream fs;
    StreamWriter sw;
    string filename;
    int dbIndex = 0;    //tracks the number of data base writes to firebase

    public ParticipantResponses pr;


    [SerializeField] TextMeshProUGUI instancetext;

    public static ExperimentLogger Instance { get; private set; }

    [DllImport("__Internal")]
    private static extern void Hello();


    [DllImport("__Internal")]
    private static extern void WriteFirebase(string projectName, string userID, string scenename, string responses, float time, int dbindex);
    //private static extern void WriteFirebase(string projectName, string userID, string scenename, string responses, int dbindex);



    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            // determine random hash and recompute in case of a random hash collission
            hashValue = Random.Range(1, 100000);
            filename = Application.persistentDataPath + "/" + hashValue.ToString() + ".txt";
            instancetext.text = "" + hashValue;

            while (File.Exists(filename))
            {
                hashValue = Random.Range(1, 100000);
                filename = Application.persistentDataPath + "/" + hashValue.ToString() + ".txt";
                instancetext.text = "" + hashValue;

            }

            Debug.Log("New subject... writing logs to: " + filename);

            // dependency injection
            pr = new ParticipantResponses();
        }
    }

    public void log(string sceneName, Dictionary<string, int> dict, float time = -1)
    {
        Debug.Log(sceneName + "; " + pr.DictionaryToString(dict));
        var projectName = Application.productName;


        using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.WriteLine(pr.DictionaryToString(dict));
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        if (sceneName != "")
        {
            dbIndex += 1;
            WriteFirebase(projectName, hashValue.ToString(), sceneName, pr.DictionaryToJSON(dict), time, dbIndex);
        }
#endif
    }

    public void UpdateResponses()
    {
        pr.UpdateSelection();
        //Dictionary<string, int> changes = pr.GetRecentParticipantResponses();
        //print("recent changes:" + pr.DictionaryToString(changes));
    }
}
