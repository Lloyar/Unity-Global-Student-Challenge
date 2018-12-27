using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickObjectSystem : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// 拾起标志
    /// </summary>
    private bool PickupFlag;
    /// <summary>
    /// 计算得分
    /// </summary>
    public static int Score;

    // Use this for initialization
    private void Start()
    {
        PickupFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 如果被拾起，执行移动操作，并闪烁，否则不闪烁
        if (PickupFlag)
        {
            GetComponent<ChangeObjectStates>().Move();
            GetComponent<ChangeObjectStates>().Blink(5);
            // MoveObjectEvent.Invoke();
            // BlinkEvent.Invoke(5);
        }
        else
        {
            GetComponent<ChangeObjectStates>().Blink(0);
            //BlinkEvent.Invoke(0);
        }
    }

    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    /// <param name="eventData"></param> 发生此事件所产生的信息
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SetMoveFlagAndScoringPos();
        }
    }

    /// <summary>
    /// 如果此物体未被拾起则拾起。否则，放置并打分。
    /// </summary>
    private void SetMoveFlagAndScoringPos()
    {
        if (!PickupFlag)
        {
            PickupFlag = true;
        }
        else
        {
            Score = 0;
            PickupFlag = false;
            Scoring();
        }
    }

    private void Scoring()
    {
        throw new NotImplementedException();
    }
}