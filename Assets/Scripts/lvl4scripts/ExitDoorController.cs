using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorController : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string nextLevelScene = "Level5";
    [SerializeField] private int requiredMobKills = 13;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [Header("UI References")]
    [SerializeField] private GameObject killAllMobsText;

    private bool playerInRange = false;
    private DoorTransition doorTransition;

    private void Start()
    {
        
        doorTransition = GetComponent<DoorTransition>();

        
        if (doorTransition != null)
        {
            doorTransition.enabled = false;
        }

      
        if (killAllMobsText != null)
        {
            killAllMobsText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

           
            CheckMobKillRequirement();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

           
            if (killAllMobsText != null)
            {
                killAllMobsText.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            CheckMobKillRequirement();
        }
    }

    private void CheckMobKillRequirement()
    {
       
        if (ObjectiveTracker.Instance == null)
        {
            Debug.LogError("ObjectiveTracker instance not found!");
            return;
        }

        // Get current mob kill count from ObjectiveTracker
        int currentKills = ObjectiveTracker.Instance.GetCurrentKills();

        // If player has killed enough mobs, load the next level
        if (currentKills >= requiredMobKills)
        {
            LoadNextLevel();
        }
        else
        {
            // Show the text if they haven't killed enough mobs
            if (killAllMobsText != null)
            {
                killAllMobsText.SetActive(true);
            }
        }
    }

    private void LoadNextLevel()
    {
        // Load the next level scene
        if (!string.IsNullOrEmpty(nextLevelScene))
        {
            SceneManager.LoadScene(nextLevelScene);
        }
        else
        {
            Debug.LogError("Next level scene name not specified in ExitDoorController!");
        }
    }
}