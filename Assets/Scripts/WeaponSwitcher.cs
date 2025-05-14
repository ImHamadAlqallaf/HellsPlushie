using UnityEngine;
using System;

public class WeaponSwitcher : MonoBehaviour
{
    /// <summary>
    /// Fired whenever the player requests a weapon change.
    /// oldIndex = the weapon that was active,
    /// newIndex = the weapon to switch to.
    /// </summary>
    public event Action<int, int> onSwitchRequest;

    int _currentIndex = 0;
    Transform[] _weapons;

    void Start()
    {
        // Cache all child weapons
        int count = transform.childCount;
        _weapons = new Transform[count];
        for (int i = 0; i < count; i++)
            _weapons[i] = transform.GetChild(i);

        // Ensure only the first weapon is active at start
        SwitchTo(0);
    }

    void Update()
    {
        int prev = _currentIndex;

        // Number keys
        if (Input.GetKeyDown(KeyCode.Alpha1)) _currentIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && _weapons.Length > 1) _currentIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && _weapons.Length > 2) _currentIndex = 2;

        // Scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
            _currentIndex = (_currentIndex + 1) % _weapons.Length;
        else if (scroll < 0f)
            _currentIndex = (_currentIndex - 1 + _weapons.Length) % _weapons.Length;

        // If the index changed, fire the event *then* actually switch
        if (prev != _currentIndex)
        {
            onSwitchRequest?.Invoke(prev, _currentIndex);
            SwitchTo(_currentIndex);
        }
    }

    void SwitchTo(int index)
    {
        for (int i = 0; i < _weapons.Length; i++)
            _weapons[i].gameObject.SetActive(i == index);
    }
}
