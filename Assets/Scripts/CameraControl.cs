﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraControl : MonoBehaviour {
    public GameObject mainCamera;//获取摄像机
    public GameObject terrain;//获取地图
    public float rollAngle=25;//摄像机倾斜角度
    public float height=12f;//摄像机和地图的高度差
    public float moveSpeed = 0.5f;//摄像机移动速度系数
    public float maxRangeX;//摄像机x坐标变化范围
    public float minRangeX;
    public float maxRangeZ;//摄像机z坐标变化范围
    public float minRangeZ;
    public float maxRangeY = 15f;//缩放最大高度
    public float minRangeY = 10f;//缩放最小高度
    public float zoomSpeed = 2f;//缩放速度系数
    public float rotateAngle = 0;
    public float rotateSpeed = 50f;//旋转速度系数
    Vector3 cameraPos;//摄像机临时坐标
    public float sizeX;//地图尺寸x
    public float sizeZ;//地图尺寸z
    void Start () {
        //获取地图尺寸
        sizeX = terrain.GetComponent<TerrainCollider>().bounds.size.x;
        sizeZ = terrain.GetComponent<TerrainCollider>().bounds.size.z;
        maxRangeX = terrain.transform.position.x + 0.75f * sizeX;
        minRangeX = terrain.transform.position.x + 0.25f * sizeX;
        maxRangeZ = terrain.transform.position.z + 0.75f * sizeZ;
        minRangeZ = terrain.transform.position.z + 0.25f * sizeZ;
        //设置摄像机位置
        cameraPos.x = terrain.transform.position.x +  sizeX/ 2 ;
        cameraPos.y = terrain.transform.position.y + height;
        cameraPos.z = terrain.transform.position.z +  sizeZ/ 2 ;
        mainCamera.transform.position = cameraPos;
        mainCamera.transform.eulerAngles = new Vector3(rollAngle, 0, 0);
        Debug.Log("地图尺寸为：" + sizeX + "X" + sizeZ);
    }
	void Update () {
        //更新摄像机坐标
        MoveCamera();
        RotateCamera();
        ZoomCamera();
        LimitRange();
        mainCamera.transform.position = cameraPos;
    }
    void MoveCamera() {
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
    void ZoomCamera() {
        //滚动鼠标滑轮缩放摄像机
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            if (cameraPos.y < minRangeY)
                return;
            Vector3 moveDirectionZ = transform.forward;
            cameraPos += moveDirectionZ * zoomSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") <0) {
            if (cameraPos.y > maxRangeY)
                return;
            Vector3 moveDirectionZ = -transform.forward;
            cameraPos += moveDirectionZ * zoomSpeed;
        }
    }
    void RotateCamera() {
        //按下鼠标右键旋转摄像机
        if (Input.GetMouseButton(1)) {
            rotateAngle = Input.GetAxis("Mouse X")*rotateSpeed*Time.deltaTime;
            mainCamera.transform.Rotate(0,rotateAngle,0,Space.World);
        }
    }
    void LimitRange() {
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
