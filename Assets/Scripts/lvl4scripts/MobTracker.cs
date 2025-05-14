using UnityEngine;

public class MobTracker : MonoBehaviour
{
    [SerializeField] private bool debugMode = true;
    private bool hasBeenCounted = false;

    private void Start()
    {
        if (debugMode)
        {
            Debug.Log($"MobTracker attached to {gameObject.name}");

            // Check if ObjectiveTracker exists
            if (ObjectiveTracker.Instance == null)
            {
                Debug.LogError("ObjectiveTracker singleton not found! Make sure it's in the scene.");
            }
        }
    }

    // This is called when the GameObject is destroyed
    private void OnDestroy()
    {
        if (debugMode)
        {
            Debug.Log($"OnDestroy called for {gameObject.name}, HasBeenCounted: {hasBeenCounted}, IsPlaying: {Application.isPlaying}");
        }

       
        if (!hasBeenCounted && Application.isPlaying)
        {
            if (ObjectiveTracker.Instance != null)
            {
                ObjectiveTracker.Instance.MobKilled();
                hasBeenCounted = true;

                if (debugMode)
                {
                    Debug.Log($"MobKilled reported to ObjectiveTracker for {gameObject.name}");
                }
            }
            else
            {
                if (debugMode)
                {
                    Debug.LogError("ObjectiveTracker instance is null when trying to report kill!");
                }
            }
        }
    }
}