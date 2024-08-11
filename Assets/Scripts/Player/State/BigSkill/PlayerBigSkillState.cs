using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家大招状态
/// </summary>
public class PlayerBigSkillState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        //切换镜头
        playerModel.bigSkillStartShot.SetActive(false);
        playerModel.bigSkillShot.SetActive(true);
        //播放动画
        playerController.PlayAnimation("BigSkill", 0f);
    }
    public override void Update()
    {
        base.Update();
        #region 检测动画播放结束
        if(IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.BigSkillEnd);
            return;
        }
        #endregion
    }
}
