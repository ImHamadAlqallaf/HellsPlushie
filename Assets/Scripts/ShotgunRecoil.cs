using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunRecoil : MonoBehaviour
{
    public GameObject Gun;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(StartRecoil());
        }
    }

    IEnumerator StartRecoil()
    {
        Gun.GetComponent<Animator>().Play("ShotgunRecoil");
        yield return new WaitForSeconds(0.75f);
        Gun.GetComponent<Animator>().Play("New State");
    }
}
