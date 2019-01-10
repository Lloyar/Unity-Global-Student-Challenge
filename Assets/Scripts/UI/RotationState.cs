using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationState : MonoBehaviour
{
    private bool state;
    // Start is called before the first frame update
    void Start()
    {
        state = gameObject.activeSelf;
    }
    public void StateSwitch()
    {
        if (state)
        {
            gameObject.SetActive(false);
            state = false;
        }
        else
        {
            gameObject.SetActive(true);
            state = true;
        }
    }
}
