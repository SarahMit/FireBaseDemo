using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLogger : MonoBehaviour
{
    public void ValueUpdate()
    {
        print("Button pressed");
        ExperimentLogger.Instance.UpdateResponses();
    }

    public void TimeUpdate()
    {

    }

    public void EnableNavigation()
    {
        GameObject next = GameObject.Find("/LogHandler/Canvas/Next");
        Debug.Log(next.gameObject.name);
        next.GetComponent<Button>().interactable = true;
        GameObject prev = GameObject.Find("/LogHandler/Canvas/Prev");
        Debug.Log(prev.gameObject.name);
        prev.GetComponent<Button>().interactable = true;
    }
}
