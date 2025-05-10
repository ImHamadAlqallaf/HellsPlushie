using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    int _current = 0;
    Transform[] _weapons;

    void Start()
    {
        // Cache all weapon children
        int count = transform.childCount;
        _weapons = new Transform[count];
        for (int i = 0; i < count; i++)
            _weapons[i] = transform.GetChild(i);

        // Activate only the first weapon
        SwitchTo(0);
    }

    void Update()
    {
        int prev = _current;

        // 1,2,3 hotkeys
        if (Input.GetKeyDown(KeyCode.Alpha1)) _current = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && _weapons.Length > 1) _current = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && _weapons.Length > 2) _current = 2;

        // Scroll wheel (forward/back)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
            _current = (_current + 1) % _weapons.Length;
        else if (scroll < 0f)
            _current = (_current - 1 + _weapons.Length) % _weapons.Length;

        if (prev != _current)
            SwitchTo(_current);
    }

    void SwitchTo(int index)
    {
        for (int i = 0; i < _weapons.Length; i++)
            _weapons[i].gameObject.SetActive(i == index);
    }
}
