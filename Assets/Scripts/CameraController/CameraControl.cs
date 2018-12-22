using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject mainCamera;//获取摄像机
    public GameObject cameraStartPos; // 摄像机起始坐标
    public GameObject terrain;//获取地图
    public float moveSpeed = 0.5f;//摄像机移动速度系数
    public float zoomSpeed = 0.3f;//缩放速度系数
    public float rotateSpeed = 60f;//旋转速度系数
    private float maxRangeX;//摄像机x坐标变化范围
    private float minRangeX;
    private float maxRangeZ;//摄像机z坐标变化范围
    private float minRangeZ;
    private float maxRangeY = 30f;//缩放最大高度
    private float minRangeY = 15f;//缩放最小高度

    Vector3 cameraPos;//摄像机临时坐标

    private float sizeX;//地图尺寸x
    private float sizeZ;//地图尺寸z
    void Start()
    {
        //获取地图尺寸
        sizeX = terrain.GetComponent<TerrainCollider>().bounds.size.x;
        sizeZ = terrain.GetComponent<TerrainCollider>().bounds.size.z;
        maxRangeX = terrain.transform.position.x + 0.75f * sizeX;
        minRangeX = terrain.transform.position.x + 0.25f * sizeX;
        maxRangeZ = terrain.transform.position.z + 0.75f * sizeZ;
        minRangeZ = terrain.transform.position.z + 0.25f * sizeZ;

        cameraPos = cameraStartPos.transform.position + new Vector3(0, 20, -10);
        //设置摄像机位置
        mainCamera.transform.position = cameraPos;
        mainCamera.transform.eulerAngles = new Vector3(25, 0, 0);

        //mainCamera.transform.forward = new Vector3(0, 0, 1);
        Debug.Log(mainCamera.transform.position);
        Debug.Log("地图尺寸为：" + sizeX + "X" + sizeZ);
    }
    void Update()
    {
        //更新摄像机坐标
        MoveCamera();
        RotateCamera();
        ZoomCamera();
        LimitRange();
        mainCamera.transform.position = cameraPos;
    }
    void MoveCamera()
    {
        //第一种方法按下WS、AD，Input.GetAxis()会返回1或-1
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirectionZ = transform.forward;
        moveDirectionZ.y = 0;
        cameraPos += moveDirectionZ * moveZ * moveSpeed;
        float moveX = Input.GetAxis("Horizontal");
        Vector3 moveDirectionX = transform.right;
        moveDirectionZ.y = 0;
        cameraPos += moveDirectionX * moveX * moveSpeed;
        //按下shift加速移动摄像机
        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed = 2.5f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = 1.5f;
    }

    void ZoomCamera()
    {
        if (Input.GetKey(KeyCode.E) && cameraPos.y <= maxRangeY)
        {
            Vector3 moveDirectionY = new Vector3(0, 1, 0);
            cameraPos += moveDirectionY * zoomSpeed;
        }
        if (Input.GetKey(KeyCode.Q) && cameraPos.y >= minRangeY)
        {
            Vector3 moveDirectionY = new Vector3(0, -1, 0);
            cameraPos += moveDirectionY * zoomSpeed;
        }
    }

    // void ZoomCamera()
    // {
    //     if (Input.GetAxis("Mouse ScrollWheel") > 0 && cameraPos.y <= maxRangeY)
    //     {
    //         Vector3 moveDirectionY = new Vector3(0, 1, 0);
    //         cameraPos += moveDirectionY * zoomSpeed;
    //     }
    //     if (Input.GetAxis("Mouse ScrollWheel") < 0 && cameraPos.y >= minRangeY)
    //     {
    //         Vector3 moveDirectionY = new Vector3(0, -1, 0);
    //         cameraPos += moveDirectionY * zoomSpeed;
    //     }
    // }

    // void ZoomCamera() {
    //     //滚动鼠标滑轮缩放摄像机
    //     if (Input.GetAxis("Mouse ScrollWheel") > 0) {
    //         if (cameraPos.y < minRangeY)
    //             return;
    //         Vector3 moveDirectionZ = transform.forward;
    //         cameraPos += moveDirectionZ * zoomSpeed;
    //     }
    //     if (Input.GetAxis("Mouse ScrollWheel") <0) {
    //         if (cameraPos.y > maxRangeY)
    //             return;
    //         Vector3 moveDirectionZ = -transform.forward;
    //         cameraPos += moveDirectionZ * zoomSpeed;
    //     }
    // }
    void RotateCamera()
    {
        //按下鼠标右键旋转摄像机
        if (Input.GetMouseButton(1))
        {
            mainCamera.transform.RotateAround(mainCamera.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
            mainCamera.transform.RotateAround(mainCamera.transform.position, mainCamera.transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime);
        }
    }
    void LimitRange()
    {
        //摄像机移动范围限制
        if (cameraPos.x > maxRangeX)
            cameraPos.x = maxRangeX;
        if (cameraPos.x < minRangeX)
            cameraPos.x = minRangeX;
        if (cameraPos.z > maxRangeZ)
            cameraPos.z = maxRangeZ;
        if (cameraPos.z < minRangeZ)
            cameraPos.z = minRangeZ;
    }
}
