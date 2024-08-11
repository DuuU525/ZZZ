using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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
        #region  迈出左右脚的判断
        switch (playerModel.foot)
        {
            case ModelFoot.Right:
                playerController.PlayAnimation("Run", 0.25f, 0f);
                playerModel.foot = ModelFoot.Left;
                break;
            case ModelFoot.Left:
                playerController.PlayAnimation("Run", 0.25f, 0.5f);
                playerModel.foot = ModelFoot.Right;
                break;
        }
        #endregion
    }
    public override void Update()
    {
        base.Update();

        #region 检测大招
        if(playerController.inputSystem.Player.BigSkill.triggered)
        {
            //切换到进入大招状态
            playerController.SwitchState(PlayerState.BigSkillStart);
            return;
        }
        #endregion
        #region 检测攻击
        if(playerController.inputSystem.Player.Fire.triggered)
        {
            //切换到普通攻击状态
            playerController.SwitchState(PlayerState.NormalAttack);
            return;
        }
        #endregion
        #region 检测闪避
        if(playerController.inputSystem.Player.Evade.IsPressed())
        {
            //切换闪避状态
            playerController.SwitchState(PlayerState.Evade_Front);
            return;
        }
        #endregion
        #region 检测待机
        if (playerController.inputMoveVec2 == Vector2.zero)
        {
            playerController.SwitchState(PlayerState.RunEnd);
            return;
        }
        else
        {
            #region 处理移动方向
            Vector3 inputMoveVec3 = new Vector3(playerController.inputMoveVec2.x, 0, playerController.inputMoveVec2.y);
            //获取相机的旋转轴Y
            float cameraAxisY = mainCamera.transform.rotation.eulerAngles.y;
            //四元数 x 向量
            Vector3 targetDir = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;
            Quaternion targetQua = Quaternion.LookRotation(targetDir);
            //计算旋转角度
            float angles = Mathf.Abs(targetQua.eulerAngles.y - playerModel.transform.eulerAngles.y);
            if (angles > 177.5f && angles < 182.5f)
            {
                //切换到转身状态
                playerController.SwitchState(PlayerState.TurnBack);
            }
            else
            {
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, targetQua, Time.deltaTime * playerController.rotationSpeed);
            }
            #endregion
            return;
        }
        #endregion

    }
}
