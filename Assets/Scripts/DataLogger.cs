using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    public void ValueUpdate()
    {
        ExperimentLogger.Instance.UpdateResponses();
    }
}
