using UnityEngine;
using System.Collections;

public class SetModel : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.white);
            Debug.Log("碰撞位置：" + hit.point  + "  " + "碰撞方向：" + hit.normal);
            target.transform.position = hit.point;
            target.transform.up = hit.normal;
            target.transform.Translate(Vector3.up * 0.5f * target.transform.localScale.y, Space.Self);
        }
    }
}