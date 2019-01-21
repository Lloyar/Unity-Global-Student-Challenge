using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 更改物体的位置，闪烁状态，闪烁颜色
/// </summary>
public class MoveObject : MonoBehaviour
{
    private static GameObject hitcamera;

    private void Start() {
        hitcamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    /// <summary>
    /// 移动物体，并根据是否在可放置范围内改变闪烁颜色
    /// </summary>
    public void Move()
    {
        RaycastHit hitinfo;
        Ray ray = hitcamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitinfo, 100, layerMask: 1 << 9))
        {
            if (hitinfo.normal.y > 0.707)
            {
                gameObject.transform.position = hitinfo.point;
                gameObject.transform.up = hitinfo.normal;
            }
            SetColor(new Color(0, 1, 0, 1));
        }
        else
        {
            SetColor(new Color(1, 0, 0, 1));
        }
    }

    /// <summary>
    /// 设置此物体着色器上的边缘光颜色
    /// </summary>
    /// <param name="col"></param> 设置的颜色    
    public void SetColor(Color col)
    {
        //Renderer render = renderer;
        GetComponent<Renderer>().material.SetColor("_RimColor", col);
    }

}
