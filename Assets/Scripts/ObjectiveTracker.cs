using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveTracker : MonoBehaviour
{
    [Header("Objective Settings")]
    [SerializeField] private int totalMobsToKill = 10;
    [SerializeField] private string objectiveDescription = "Eliminate enemies";

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject objectiveCompleteBanner;

    private int mobsKilled = 0;

   
    public static ObjectiveTracker Instance { get; private set; }

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();

       
        if (objectiveCompleteBanner != null)
        {
            objectiveCompleteBanner.SetActive(false);
        }
    }

    public void MobKilled()
    {
        mobsKilled++;
        UpdateUI();

       
        if (mobsKilled >= totalMobsToKill)
        {
            ObjectiveComplete();
        }
    }

    private void UpdateUI()
    {
        if (objectiveText != null)
        {
            objectiveText.text = objectiveDescription;
        }

        if (countText != null)
        {
            countText.text = $"{mobsKilled} / {totalMobsToKill}";
        }
    }

    private void ObjectiveComplete()
    {
        Debug.Log("Objective Complete!");

        if (objectiveCompleteBanner != null)
        {
            objectiveCompleteBanner.SetActive(true);

            
            Invoke("HideCompletionBanner", 3f);
        }

       
    }

    private void HideCompletionBanner()
    {
        if (objectiveCompleteBanner != null)
        {
            objectiveCompleteBanner.SetActive(false);
        }
    }

    
    public void SetNewObjective(string description, int target)
    {
        objectiveDescription = description;
        totalMobsToKill = target;
        mobsKilled = 0;
        UpdateUI();
    }
}