using UnityEngine;
using System.Collections;

public class GunRecoil : MonoBehaviour
{
    [Header("Recoil Settings")]
    [Tooltip("How far (in local Z) the gun moves backwards when fired")]
    public float recoilDistance = 0.1f;
    [Tooltip("How fast it moves into the recoil position")]
    public float recoilSpeed = 20f;
    [Tooltip("How fast it returns to its starting position")]
    public float returnSpeed = 10f;

    private Vector3 _startPos;

    void Start()
    {
        // cache the “rest” position
        _startPos = transform.localPosition;
    }

    // call this from your firing code
    public void FireRecoil()
    {
        StopAllCoroutines();
        StartCoroutine(DoRecoil());
    }

    IEnumerator DoRecoil()
    {
        Vector3 target = _startPos - Vector3.forward * recoilDistance;

        // recoil back
        while (Vector3.Distance(transform.localPosition, target) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition, target, Time.deltaTime * recoilSpeed);
            yield return null;
        }

        // return forward
        while (Vector3.Distance(transform.localPosition, _startPos) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition, _startPos, Time.deltaTime * returnSpeed);
            yield return null;
        }

        transform.localPosition = _startPos;
    }
}
