using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectsClickEvent : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// GameController 对象
    /// </summary>
    private GameController GameController;

    private GameObject ToolsBoxUI;

    // Use this for initialization
    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        ToolsBoxUI = GameObject.FindGameObjectWithTag("Tools Box");
        ToolsBoxUI.SetActive(false);
        //ToolsBox = GameObject.FindGameObjectsWithTag("Tools Box")[1].GetComponent<ToolsBox>();
    }

    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    /// <param name="eventData"></param> 发生此事件所产生的信息
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && GameController.SelectedGameob == null)
        {
            GetComponent<MoveObject>().SetColor(new Color(1, 1, 1, 1));
            StartCoroutine("BlinkCoroutine");
            GameController.SelectedGameob = eventData.pointerCurrentRaycast.gameObject;
            ToolsBoxUI.SetActive(true);
        }
    }

    public void ExitSelected()
    {
        Blink(0); // 停止闪烁
        StopCoroutine("BlinkCoroutine");
        ToolsBoxUI.SetActive(false);
        GameController.SelectedGameob = null;
    }

    /// <summary>
    /// 不断改变此物体着色器上的边缘光强度，已达到闪烁的效果
    /// </summary>
    /// <param name="rate"></param> 改变的速率
    public void Blink(float rate)
    {
        Renderer render = GetComponent<Renderer>();
        if (rate == 0)
        // if (Mathf.Abs(rate) <= Mathf.Epsilon)
        {
            render.material.SetFloat("_RimPower", 0);
        }
        else
        {
            render.material.SetFloat("_RimPower", 5 * Mathf.Cos(Time.time * rate) + 6);
        }
    }

    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            Blink(5);
            yield return null;
        }
    }
}