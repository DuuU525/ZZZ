using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerBigSkillStartState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        //切换镜头
        CameraManager.INSTANCE.cm_brain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);
        CameraManager.INSTANCE.freeLookCamera.SetActive(false);
        playerModel.bigSkillStartShot.SetActive(true);
        //播放动画
        playerController.PlayAnimation("BigSkill_Start", 0f);
    }

    public override void Update()
    {
        base.Update();
        #region 检测动画播放结束
        if(IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.BigSkill);
            return;
        }
        #endregion
    }
}
