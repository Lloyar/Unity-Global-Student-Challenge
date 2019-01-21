using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工具中心，负责调用被选中物体各个基础组件，是对基础操作的一次封装，使得 UI 组件 Button 可以只与此组件绑定
/// </summary>
public class ToolsCentral : MonoBehaviour
{
    private GameController GameController;

    /// <summary>
    /// Move Tool
    /// </summary>
    public GameObject MoveActiveExplain;

    /// <summary>
    /// Rotation Tool
    /// </summary>
    public GameObject RotationActiveExplain;
    public GameObject RotationButton;


    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void ExitToolsBox()
    {
        GameController.SelectedGameob.GetComponent<ObjectsClickEvent>().ExitSelected();
    }

    /* ******************************** Move ******************************** */
    public void MoveActive()
    {
        MoveActiveExplain.SetActive(true);
        StartCoroutine("Move");
        StartCoroutine("MoveCallback");
    }

    IEnumerator Move()
    {
        // 如果有物体被拾起，执行移动操作，并闪烁
        while (true)
        {
            if (GameController.SelectedGameob != null)
            {
                GameController.SelectedGameob.GetComponent<MoveObject>().Move();
                GameController.SelectedGameob.GetComponent<ObjectsClickEvent>().Blink(5);
            }
            yield return null;
        }
    }

    IEnumerator MoveCallback()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveActiveFalse();
                StopCoroutine("MoveCallback");
            }
            yield return null;
        }
    }

    private void MoveActiveFalse()
    {
        GameController.SelectedGameob.GetComponent<MoveObject>().SetColor(new Color(1, 1, 1, 1));
        StopCoroutine("Move");
        MoveActiveExplain.SetActive(false);
    }

    /* ******************************** Rotation ******************************** */
    public void RotationActive()
    {
        RotationActiveExplain.SetActive(true);
        RotationButton.SetActive(true);
        StartCoroutine("RotationCallback");
    }

    public void RotationLeft()
    {
        GameController.SelectedGameob.GetComponent<RotationObject>().rotationleft();
    }

    public void RotationRight()
    {
        GameController.SelectedGameob.GetComponent<RotationObject>().rotationright();
    }

    IEnumerator RotationCallback()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                RotationActiveFalse();
                StopCoroutine("RotationCallback");
            }
            yield return null;
        }
    }

    private void RotationActiveFalse()
    {
        RotationActiveExplain.SetActive(false);
        RotationButton.SetActive(false);
    }

    /* ******************************** Axe ******************************** */
    public void Axe()
    {

    }

}

