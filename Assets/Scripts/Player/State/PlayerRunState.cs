using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家奔跑状态
/// </summary>
public class PlayerRunState : PlayerStateBase
{
    private Camera mainCamera;
    public override void Enter()
    {
        base.Enter();
        mainCamera = Camera.main;
        playerController.PlayAnimation("Run");
    }
    public override void Update()
    {
        base.Update();

        #region 检测攻击
        #endregion

        #region 检测待机
        if(playerController.inputMoveVec2 == Vector2.zero)
        {
            playerController.SwitchState(PlayerState.Idle);
        }
        #endregion

        #region 处理移动方向
        Vector3 inputMoveVec3 = new Vector3(playerController.inputMoveVec2.x, 0, playerController.inputMoveVec2.y);
        //获取相机的旋转轴Y
        float cameraAxisY = mainCamera.transform.rotation.eulerAngles.y;
        //四元数 x 向量
        Vector3 targetDir = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;
        Quaternion targetQua = Quaternion.LookRotation(targetDir);
        playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, targetQua, Time.deltaTime * playerController.rotationSpeed);
        #endregion
    }
}
