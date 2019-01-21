using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    public void rotationleft()
    {
        var localup = gameObject.transform.worldToLocalMatrix.MultiplyVector(gameObject.transform.up);
        gameObject.transform.Rotate(localup, 10f);
    }

    public void rotationright()
    {
        var localup = gameObject.transform.worldToLocalMatrix.MultiplyVector(gameObject.transform.up);
        gameObject.transform.Rotate(localup, -10f);
    }
}
