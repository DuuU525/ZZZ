using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/// <summary>
/// 玩家大招结束状态
/// </summary>
public class PlayerBigSkillEndState : PlayerStateBase
{
    
    public override void Enter()
    {
        base.Enter();
        //切换镜头
        playerModel.bigSkillShot.SetActive(false);
        CameraManager.INSTANCE.cm_brain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 1f);
        CameraManager.INSTANCE.freeLookCamera.SetActive(true);
        CameraManager.INSTANCE.ResetFreeLookCamera();
        //播放动画
        playerController.PlayAnimation("BigSkill_End", 0f);
    }

    public override void Update()
    {
        base.Update();
        #region 检测攻击
        if(playerController.inputSystem.Player.Fire.triggered)
        {
            //切换到普通攻击状态
            playerController.SwitchState(PlayerState.NormalAttack);
            return;
        }
        #endregion

        #region 检测闪避
        if(playerController.inputSystem.Player.Evade.triggered)
        {
            //切换闪避状态
            playerController.SwitchState(PlayerState.Evade_Back);
            return;
        }
        #endregion
        #region 监听移动
        if(playerController.inputMoveVec2 != Vector2.zero)
        {
            //切换到奔跑状态
            playerController.SwitchState(PlayerState.Run);
            return;
        }
        #endregion
        #region 检测动画播放结束
        if(IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.Idle);
            return;
        }
        #endregion
    }
}
