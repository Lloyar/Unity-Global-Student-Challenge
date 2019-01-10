using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectsController : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// GameController 对象
    /// </summary>
    private GameController GameController;
    /// <summary>
    /// 此位置的得分
    /// </summary>
    public static int Score;

    // Use this for initialization
    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    /// <param name="eventData"></param> 发生此事件所产生的信息
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!GameController.PickupFlag)
            {
                GameController.SelectedGameob = eventData.pointerCurrentRaycast.gameObject;
                GameController.PickupFlag = true;
            }
            else
            {
                Score = 0;
                GameController.SelectedGameob = null;
                GameController.PickupFlag = false;
                GetComponent<ChangeObjectStates>().Blink(0); // 停止闪烁
                Scoring();
            }
        }
    }

    /// <summary>
    /// 如果此物体未被拾起则拾起。否则，放置并打分。
    /// </summary>
    private void SetMoveFlagAndScoringPos()
    {

    }

    private void Scoring()
    {
        //throw new NotImplementedException();
    }
}