using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInTime : MonoBehaviour
{
    private float timeSinceStart;

    private void Update()
    {
        timeSinceStart += Time.deltaTime;
    }

    public void LogTime()
    {
        ExperimentLogger.Instance.log(Transition.Instance.currentSceneName, new Dictionary<string, int>(), timeSinceStart);
    }
}
