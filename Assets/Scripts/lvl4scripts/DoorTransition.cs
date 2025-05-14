using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private bool requiresKeyPress = true;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!requiresKeyPress)
            {
                LoadNextScene();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (requiresKeyPress && playerInRange && Input.GetKeyDown(interactionKey))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("Scene name not specified in DoorTransition script!");
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}