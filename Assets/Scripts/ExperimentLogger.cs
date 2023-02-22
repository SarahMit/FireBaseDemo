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
    int dbIndex = 0;

    [SerializeField] TextMeshProUGUI instancetext;

    public static ExperimentLogger Instance { get; private set; }

    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void WriteFirebase(string experimentname, string id, string scenename, string time, Dictionary<string, int> participantresponse,string dbIndex);


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        DontDestroyOnLoad(gameObject);

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
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


            //JsonConvert.SerializeObject(participantresponse);
        }
    }

    public void log(string s, string sceneName, Dictionary<string, int> d, float time = -1)
    {
        Debug.Log(s);

        using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.WriteLine(s);
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        if (slideName != "" && time != -1)
        {
            dbIndex += 1;
            WriteFirebase(SceneManager.GetActiveScene().name, hashValue.ToString(), SceneManager.GetActiveScene().name, time.ToString(), JsonConvert.SerializeObject(participantresponse), dbIndex.ToString());
        }
#endif
    }
}
