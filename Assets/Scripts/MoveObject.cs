using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    /// <summary>  
    /// 射线碰撞的结构
    /// </summary>  
    private RaycastHit m_rayhit;
    /// <summary>  
    /// 鼠标拾取的有效距离  
    /// </summary>  
    public float m_fDistance = 20f;
    /// <summary>
    /// 射线选取的游戏物体
    /// </summary>
    private GameObject m_obSelected;
    /// <summary>
    /// 是否已选取游戏物体标记
    /// </summary>
    private bool flag = false;
    /// <summary>
    /// 记录鼠标移动前一帧的位置
    /// </summary>
    private Vector3 m_v3OldMousePos;
    /// <summary>
    /// 闪烁频率
    /// </summary>
    public float Blink_Rate = 5f;

    void Update()
    {
        // 当鼠标按下时选取
        if (Input.GetMouseButtonDown(0))
        {
            // 当有物体被选中时
            if (flag)
            {
                if (m_obSelected)
                {
                    m_obSelected.GetComponent<Renderer>().material.SetFloat("_RimPower", 0);   
                }
                m_obSelected = null;
                flag = false;
            }
            // 当没有物体被选中时
            else
            {
                Ray selectedRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit selectedRayHit;
                if (Physics.Raycast(selectedRay, out selectedRayHit, m_fDistance, LayerMask.GetMask("Ornament")))
                {
                    m_obSelected = selectedRayHit.collider.gameObject;
                }
                
                flag = true;
            }
        }
        // 撤销选中目标
        //if (Input.GetMouseButtonUp(0))
        //{
        //    m_obSelected.GetComponent<Renderer>().material.SetFloat("_RimPower", 0);
        //    m_obSelected = null;
        //}
        // 鼠标长按投射参照物（地面，墙体）,进行位移
        if (m_obSelected)
        {
            Blink(Blink_Rate);
            Vector3 v3CurMousePos = Input.mousePosition;
            //拖拽
            if (v3CurMousePos != m_v3OldMousePos)
            {
                Ray ray = Camera.main.ScreenPointToRay(v3CurMousePos);
                //选取标记为Ref的游戏物体（地面，墙体）
                if (Physics.Raycast(ray, out m_rayhit, m_fDistance, LayerMask.GetMask("Ref")))
                {
                    SetColor(new Color(0, 1, 0, 1));
                    MoveOrnament();
                }
                else
                {
                    SetColor(new Color(1, 0, 0, 1));
                }
            }
            m_v3OldMousePos = v3CurMousePos;
        }
    }
    /// <summary>
    /// 移动选中的摆件
    /// </summary>
    private void MoveOrnament()
    {
        //获取射线碰撞到的点
        //Vector3 movePos = m_rayhit.point;
        // 限制y轴坐标
        //movePos.y = m_obSelected.transform.position.y;
        //m_obSelected.transform.position = movePos;
        m_obSelected.transform.position = m_rayhit.point;
        m_obSelected.transform.up = m_rayhit.normal;
        //m_obSelected.transform.Translate(Vector3.up * 0.5f * m_obSelected.transform.localScale.y, Space.Self);
    }

    private void Blink(float rate)
    {
		Renderer render = m_obSelected.GetComponent<Renderer>();
		render.material.SetFloat("_RimPower", 5*Mathf.Cos(Time.time * rate) + 6);
    }

    private void SetColor(Color col)
    {
        Renderer render = m_obSelected.GetComponent<Renderer>();
        render.material.SetColor("_RimColor",col);
    }
}