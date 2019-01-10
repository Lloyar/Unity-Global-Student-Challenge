using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeColor");
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown("f"))
        // {
        //     StartCoroutine("FadeColor");
        // }
    }

    IEnumerator FadeColor()
    {
        yield return new WaitForSeconds(5f);
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = GetComponent<Renderer>().material.color;
            c.b = f;
            GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
