using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(Collider))]
public class DoorOpener : MonoBehaviour
{
    [Header("Door Visual (The rotating part)")]
    [SerializeField] private Transform doorVisual;

    [Header("UI Prompt")]
    [SerializeField] private GameObject _openPromptGO;

    [Header("Sound")]
    [SerializeField] private AudioClip _openClip;

    [Header("Animation")]
    [SerializeField] private float openDuration = 1f;
    [SerializeField] private float openAngle = -90f;

    private bool _playerInRange = false;
    private bool _isOpen = false;
    private AudioSource _audio;
    private static GameObject _currentActivePrompt;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();

        if (_openPromptGO != null)
            _openPromptGO.SetActive(false);

        if (doorVisual == null)
        {
            UnityEngine.Debug.LogError("DoorOpener: 'doorVisual' (the rotating mesh) is not assigned!");
        }

        Collider col = GetComponent<Collider>();
        if (!col.isTrigger)
        {
            UnityEngine.Debug.LogWarning("Door collider must be set as trigger!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isOpen)
        {
            if (_currentActivePrompt != null && _currentActivePrompt != _openPromptGO)
                _currentActivePrompt.SetActive(false);

            _playerInRange = true;

            if (_openPromptGO != null)
            {
                _openPromptGO.SetActive(true);
                _currentActivePrompt = _openPromptGO;
            }

            UnityEngine.Debug.Log("Player entered door trigger.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;

            if (_openPromptGO != null && _currentActivePrompt == _openPromptGO)
            {
                _openPromptGO.SetActive(false);
                _currentActivePrompt = null;
            }

            UnityEngine.Debug.Log("Player exited door trigger.");
        }
    }

    private void Update()
    {
        if (_playerInRange && !_isOpen && Input.GetKeyDown(KeyCode.E))
        {
            UnityEngine.Debug.Log("E pressed, opening door.");

            if (_openPromptGO != null)
            {
                _openPromptGO.SetActive(false);
                if (_currentActivePrompt == _openPromptGO)
                    _currentActivePrompt = null;
            }

            StartCoroutine(AnimateOpen());
            _isOpen = true;
        }
    }

    private IEnumerator AnimateOpen()
    {
        if (_openClip != null)
            _audio.PlayOneShot(_openClip);

        Quaternion startRot = doorVisual.localRotation;
        Quaternion endRot = startRot * Quaternion.Euler(0f, openAngle, 0f);

        float elapsed = 0f;
        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / openDuration);
            doorVisual.localRotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }

        doorVisual.localRotation = endRot;
        UnityEngine.Debug.Log("Door opened.");
    }

    public void ResetDoor()
    {
        _isOpen = false;
        if (doorVisual != null)
            doorVisual.localRotation = Quaternion.identity;
    }
}
