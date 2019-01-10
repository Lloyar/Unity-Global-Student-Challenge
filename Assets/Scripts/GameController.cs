using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject image;
    public GameObject toolsbox;
    private bool MoveActiveState;
    public GameObject SelectedGameob;
    public bool PickupFlag { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        image.SetActive(false);
        SelectedGameob = null;
        PickupFlag = false;
        MoveActiveState = false;
        toolsbox = GameObject.FindGameObjectWithTag("Tools Box");
    }

    IEnumerator Move()
    {
        // 如果有物体被拾起，执行移动操作，并闪烁
        while (true)
        {
            if (PickupFlag && SelectedGameob != null)
            {
                SelectedGameob.GetComponent<ChangeObjectStates>().Move();
                SelectedGameob.GetComponent<ChangeObjectStates>().Blink(5);
            }
            yield return null;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B");
            toolsbox.SetActive(true);
            StopCoroutine("Move");
            MoveActiveState = false;
            Debug.Log("StopMove");
            image.SetActive(false);
        }
    }
    public void MoveActiveStateSwitch()
    {
        if (!MoveActiveState)
        {
            SelectedGameob = null;
            MoveActiveState = true;
            StartCoroutine("Move");
            Debug.Log("StartMove");
            image.SetActive(true);
        }
        // else
        // {
        //     StopCoroutine("Move");
        //     MoveActiveState = false;
        //     Debug.Log("StopMove");
        //     image.SetActive(false);
        // }
    }


}
