using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 提示，加入设定，砍树的时候可能
/// </summary>

public class FellObject : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// 砍伐簇的子部分计数
    /// </summary>
    private static int flag = 0;

    /// <summary>
    /// 砍伐簇计数
    /// </summary>
    private static int flag_rownum = 0;

    private static MeshRenderer[] element = new MeshRenderer[3];

    /// <summary>
    /// 植物簇数
    /// </summary>
    public static int ColNum = 20;

    /// <summary>
    /// 植物节数
    /// </summary>
    public static int RowNum = 3;
    /// <summary>
    /// 植物簇分组列表
    /// </summary>
    MeshRenderer[,] meshlist = new MeshRenderer[ColNum, RowNum];

    private void Awake()
    {
        /// <summary>
        /// 获得植物簇
        /// </summary>
        MeshRenderer[] ob = gameObject.GetComponentsInChildren<MeshRenderer>();
        /// <summary>
        /// 对 ob 进行分组
        /// </summary>

        for (int i = 0; i < ColNum; i++)
        {
            for (int j = 0; j < RowNum; j++)
            {
                meshlist[i, j] = ob[i * RowNum + j];
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Fell();
    }

    /// <summary>
    /// 选择列表的任意一行
    /// </summary>
    /// <param name="list"></param> 传入的植物簇列表，行为哪一株，列为此株的哪一子部分
    /// <returns></returns> 返回选中的一株植物
    private MeshRenderer[] ClickRow(MeshRenderer[,] list, int rownum)
    {
        MeshRenderer[] element = new MeshRenderer[list.GetLength(1)];
        for (int i = 0; i < list.GetLength(1); i++)
        {
            element[i] = list[rownum, i];
        }
        Array.Reverse(element);
        return element;
    }

    private void Fell()
    {
        if (flag == 0)
        {
            element = ClickRow(meshlist, flag_rownum);
            flag_rownum += 1;
            if (flag_rownum > ColNum-1)
            {
                throw new Exception(string.Format("大兄弟，没有树可以砍了啊！"));
            }
        }
        element[flag].enabled = false;
        flag += 1;
        if (flag > RowNum-1)
        {
            flag = 0;
        }
    }
}
