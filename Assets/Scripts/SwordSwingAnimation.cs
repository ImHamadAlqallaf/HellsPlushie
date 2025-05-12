using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwingAnimation : MonoBehaviour
{
    public GameObject Sword;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(StartSwing());
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(StartHeavySwing());
        }
    }

    IEnumerator StartSwing()
    {
        Sword.GetComponent<Animator>().Play("SwordSwingWhole");
        yield return new WaitForSeconds(0.22f);
        Sword.GetComponent<Animator>().Play("New State");
    }

    IEnumerator StartHeavySwing()
    {
        Sword.GetComponent<Animator>().Play("SwordSwingHeavyWhole");
        yield return new WaitForSeconds(0.62f);
        Sword.GetComponent<Animator>().Play("New State");
    }

}
