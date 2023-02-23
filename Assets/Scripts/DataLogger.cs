using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    public void ValueUpdate()
    {
        print("Button pressed");
        ExperimentLogger.Instance.UpdateResponses();
    }
}
