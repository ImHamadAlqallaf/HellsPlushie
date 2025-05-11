using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(WeaponSwitcher))]
public class WeaponSwitchAnimator : MonoBehaviour
{
    [Tooltip("How far (in local units) to slide weapons on Y when switching")]
    public float slideDistance = 1f;
    [Tooltip("How long (seconds) each part of the switch animation takes")]
    public float switchDuration = 0.25f;

    WeaponSwitcher _switcher;
    Transform[] _weapons;
    Vector3[] _origPos;

    void Awake()
    {
        // Cache the WeaponSwitcher and all child weapons + their start positions
        _switcher = GetComponent<WeaponSwitcher>();
        int count = transform.childCount;
        _weapons = new Transform[count];
        _origPos = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            _weapons[i] = transform.GetChild(i);
            _origPos[i] = _weapons[i].localPosition;
        }
    }

    void OnEnable()
    {
        // Subscribe to the switch request event
        _switcher.onSwitchRequest += AnimateSwitch;
    }

    void OnDisable()
    {
        _switcher.onSwitchRequest -= AnimateSwitch;
    }

    void AnimateSwitch(int oldIndex, int newIndex)
    {
        // Start the slide?down/slide?up animation
        StartCoroutine(DoSwitch(oldIndex, newIndex));
    }

    IEnumerator DoSwitch(int oldIndex, int newIndex)
    {
        // Phase 1: slide old weapon down
        var oldW = _weapons[oldIndex];
        var oldStart = _origPos[oldIndex];
        var oldTarget = oldStart + Vector3.down * slideDistance;
        float t = 0f;
        while (t < switchDuration)
        {
            t += Time.deltaTime;
            float f = Mathf.SmoothStep(0f, 1f, t / switchDuration);
            oldW.localPosition = Vector3.Lerp(oldStart, oldTarget, f);
            yield return null;
        }
        oldW.gameObject.SetActive(false);
        oldW.localPosition = oldStart;

        // Phase 2: prepare new weapon below view
        var newW = _weapons[newIndex];
        var newStart = _origPos[newIndex];
        var newOff = newStart + Vector3.down * slideDistance;
        newW.localPosition = newOff;
        newW.gameObject.SetActive(true);

        // Phase 3: slide new weapon up
        t = 0f;
        while (t < switchDuration)
        {
            t += Time.deltaTime;
            float f = Mathf.SmoothStep(0f, 1f, t / switchDuration);
            newW.localPosition = Vector3.Lerp(newOff, newStart, f);
            yield return null;
        }
        newW.localPosition = newStart;
    }
}
