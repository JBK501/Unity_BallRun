using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    Transform playerTransform;
    Vector3 Offset;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position; // 카메라와 플레이어가 떨어진 위치
    }

  
    void LateUpdate()
    {
        // 플레이어가 이동할 때마다 카메라 위치 조정
        transform.position = playerTransform.position + Offset;
    }
}
