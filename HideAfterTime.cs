using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterTime : MonoBehaviour
{
    [SerializeField] private float showTime;
    void Start()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        while (showTime > 0)
        {
            showTime -= Time.deltaTime;
            yield return null;
        }
        GetComponent<TMPro.TMP_Text>().enabled = false;
    }
}
