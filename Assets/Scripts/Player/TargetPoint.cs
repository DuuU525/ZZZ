using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 跟随点
/// </summary>
public class TargetPoint : MonoBehaviour
{
    
    //高度
    private float height;
    private void Awake()
    {
        height = transform.position.y;
    }
    void LateUpdate()
    {
        Vector3 playerPos = PlayerController.INSTANCE.playerModel.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y + height, playerPos.z);
    }
}
