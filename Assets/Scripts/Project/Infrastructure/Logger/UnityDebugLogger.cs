using UnityEngine;

public class UnityDebugLogger : ILogger {
    public void Log (object message) {
        Debug.Log(message);
    }

    public void Log (object message, Object context) {
        Debug.Log(message, context);
    }

    public void LogWarning (object message) {
        Debug.LogWarning(message);
    }

    public void LogWarning (object message, Object context) {
        Debug.LogWarning(message, context);
    }

    public void LogError (object message) {
        Debug.LogError(message);
    }

    public void LogError (object message, Object context) {
        Debug.LogError(message, context);
    }
}