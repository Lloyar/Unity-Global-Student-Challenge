using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 拾起鼠标点击物体
/// </summary>
public class ClickObject : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// 碰撞结构体
    /// </summary>
    private RaycastHit hitinfo;
    /// <summary>
    /// 碰撞距离
    /// </summary>
    public float Distance = 100f;
    /// <summary>
    /// 碰撞摄像机
    /// </summary>
    private Camera maincamera;
    /// <summary>
    /// 拾起标志
    /// </summary>
    private bool flag;
    /// <summary>
    /// 闪烁频率
    /// </summary>
    public float rate = 5;

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
        flag = false;
        maincamera = null;
    }

    // Update is called once per frame
    void Update()
    {
        // 如果被拾起，执行移动操作，并闪烁，否则不闪烁
        if (flag)
        {
            MoveObject();
            Blink(rate);
        }
        else
        {
            Blink(0);
        }
    }

    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    /// <param name="eventData"></param> 发生此事件所产生的信息
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.LogFormat("{0}", eventData.button);
        maincamera = eventData.pressEventCamera;
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
        if (!flag)
        {
            flag = true;
        }
        else
        {
            Score = 0;
            flag = false;
            // 放置后查找与本物体相同标签的 GameObject ，获取其坐标后与本物体计算距离，得到分数。
            Score += PositionScoring(AimRange.transform.position);
            //Debug.Log(Score);
        }
    }

    /// <summary>
    /// 移动物体，并根据是否在可放置范围内改变闪烁颜色
    /// </summary>
    private void MoveObject()
    {

        Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitinfo, Distance, layerMask: 1 << 9))
        {
            gameObject.transform.position = hitinfo.point;
            if (hitinfo.normal.y > 0.707)
            {
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
    /// 不断改变此物体着色器上的边缘光强度，已达到闪烁的效果
    /// </summary>
    /// <param name="rate"></param> 改变的速率
    private void Blink(float rate)
    {
        Renderer render = gameObject.GetComponent<Renderer>();
        if (rate == 0)
        {
            render.material.SetFloat("_RimPower", 0);
        }
        else
        {
            render.material.SetFloat("_RimPower", 5 * Mathf.Cos(Time.time * rate) + 6);
        }

    }

    /// <summary>
    /// 设置此物体着色器上的边缘光颜色
    /// </summary>
    /// <param name="col"></param> 设置的颜色
    private void SetColor(Color col)
    {
        Renderer render = gameObject.GetComponent<Renderer>();
        render.material.SetColor("_RimColor", col);
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
