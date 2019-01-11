using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsBox : MonoBehaviour
{
    public GameObject MoveExplain;
    /// <summary>
    /// GameController 对象
    /// </summary>
    private GameController GameController;
    private GameObject toolsbox;
    private int ToolsSelected;

    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        toolsbox = GameObject.FindGameObjectWithTag("Tools Box");
    }

/* **************************** Move ******************************** */
    public void MoveActive()
    {
        GameController.SelectedGameob = null;
        toolsbox.SetActive(false);
        StartCoroutine("Move");
        MoveExplain.SetActive(true);
        //GameController.ToolsSelectedPlaceholder = true;
        StartCoroutine("Movecallback");
    }

    private void MoveActiveFalse()
    {
        toolsbox.SetActive(true);
        StopCoroutine("Move");
        MoveExplain.SetActive(false);
        //GameController.ToolsSelectedPlaceholder = false;
    }
    
    IEnumerator Move()
    {
        // 如果有物体被拾起，执行移动操作，并闪烁
        while (true)
        {
            if (GameController.SelectedGameob != null)
            {
                GameController.SelectedGameob.GetComponent<ChangeObjectStates>().Move();
                GameController.SelectedGameob.GetComponent<ChangeObjectStates>().Blink(5);
            }
            yield return null;
        }
    }

    IEnumerator Movecallback()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                MoveActiveFalse();
                StopCoroutine("Movecallback");
            }
            yield return null;
        }
    }

/* **************************** Ratation ******************************** */
    public void RatationActive()
    {

    }
}
