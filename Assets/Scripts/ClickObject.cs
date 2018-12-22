using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class MoveObjectEvent : UnityEvent{}
[Serializable]
public class BlinkEvent : UnityEvent<float>{}

public class ClickObject : MonoBehaviour, IPointerClickHandler
{
    public MoveObjectEvent MoveObjectEvent;
    public BlinkEvent BlinkEvent;

    /// <summary>
    /// 拾起标志
    /// </summary>
    private bool PickUpFlag;
    /// <summary>
    /// 计算得分
    /// </summary>
    public static int Score;
    /// <summary>
    /// 目标区域
    /// </summary>
    public GameObject AimRange;
    /// <summary>
    /// 距离目标区域中心点的评分范围，要求是5的倍数
    /// </summary>
    public int limits = 25;

    // Use this for initialization
    private void Awake()
    {
        PickUpFlag = false;
    }

    private void Start()
    {
        if (MoveObjectEvent == null)
        {
            MoveObjectEvent = new MoveObjectEvent();
        }
        if (BlinkEvent == null)
        {
            BlinkEvent = new BlinkEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 如果被拾起，执行移动操作，并闪烁，否则不闪烁
        if (PickUpFlag)
        {
            MoveObjectEvent.Invoke();
            BlinkEvent.Invoke(5);
        }
        else
        {
            BlinkEvent.Invoke(0);
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
            SetMoveFlagAndScoring();
        }
    }

    /// <summary>
    /// 如果此物体未被拾起则拾起。否则，放置并打分。
    /// </summary>
    private void SetMoveFlagAndScoring()
    {
        if (!PickUpFlag)
        {
            PickUpFlag = true;
        }
        else
        {
            Score = 0;
            PickUpFlag = false;
            // 放置后查找与本物体相同标签的 GameObject ，获取其坐标后与本物体计算距离，得到分数。
            Score += PositionScoring(AimRange.transform.position);
            //Debug.Log(Score);
        }
    }

    /// <summary>
    /// 通过位置对放置物给出一个评分
    /// </summary>
    /// <param name="source_point"></param> 目标区域的位置
    /// <returns></returns>
    private int PositionScoring(Vector3 source_point)
    {
        float distance = Vector3.Distance(source_point, gameObject.transform.position);
        //Debug.LogFormat("{0} {1} {2}", distance, distance / 5, (int)distance / 5);
        int _Score;
        switch ((int)distance / (limits / 5))
        {
            case 0:
                _Score = 10;
                break;
            case 1:
                _Score = 8;
                break;
            case 2:
                _Score = 6;
                break;
            case 3:
                _Score = 4;
                break;
            case 4:
                _Score = 2;
                break;
            case 5:
                _Score = 1;
                break;
            default:
                _Score = 0;
                break;
        }
        return _Score;
    }

    /// <summary>
    /// 碰撞开始的瞬间调用
    /// </summary>
    /// <param name="other"></param> 被碰撞体
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("碰到{0}了", other.name);
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("碰撞中，每帧调用");
        print(":" + other.name);
    }
     */

    /// <summary>
    /// 碰撞结束的瞬间调用
    /// </summary>
    /// <param name="other"></param> 被碰撞体
    private void OnTriggerExit(Collider other)
    {
        Debug.LogFormat("离开了{0}", other.name);
    }
}