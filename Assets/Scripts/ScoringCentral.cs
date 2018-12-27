using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringCentral : MonoBehaviour
{
    /// <summary>
    /// 距离目标区域中心点的评分范围，要求是5的倍数
    /// </summary>
    public int limits = 25;
    public static int Score { get; }

    private Text text;
    // Use this for initialization
    void Start()
    {
        text = GameObject.Find("Score").GetComponent<Text>();
    }

    public int PositionScoring()
    {
        int score = 0;
        var trans = gameObject.transform.parent.parent.Find("AimRange").Find(gameObject.name);
        Debug.Log(trans.position);
		float distance = Vector3.Distance(trans.position,
            gameObject.transform.position);
        switch ((int)distance/(limits/5))
        {
            case 0:
                score = 10;
                break;
            case 1:
                score = 8;
                break;
            case 2:
                score = 6;
                break;
            case 3:
                score = 4;
                break;
            case 4:
                score = 2;
                break;
            case 5:
                score = 1;
                break;
            default:
                score = 0;
                break;
        }
        return score;
    }

    private int CollisionScoring()
    {
        int score = 0;

        return score;
    }

    /// <summary>
    /// 碰撞开始的瞬间调用
    /// </summary>
    /// <param name="other"></param> 被碰撞体
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("碰到{0}了", other.name);
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     Debug.Log("碰撞中，每帧调用");
    //     print(":" + other.name);
    // }

    /// <summary>
    /// 碰撞结束的瞬间调用
    /// </summary>
    /// <param name="other"></param> 被碰撞体
    private void OnTriggerExit(Collider other)
    {
        Debug.LogFormat("离开了{0}", other.name);
    }
}
