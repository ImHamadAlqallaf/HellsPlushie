using UnityEngine;

public class FrameRateLimiter : MonoBehaviour
{
    [Tooltip("Max frames per second the game will run at")]
    public int targetFPS = 144;

    void Awake()
    {
        // 1) Turn off V-Sync so it won’t override our limit
        QualitySettings.vSyncCount = 0;

        // 2) Enforce our target frame rate
        Application.targetFrameRate = targetFPS;
    }
}
