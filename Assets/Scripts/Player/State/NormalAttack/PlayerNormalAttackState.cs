using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家普通攻击状态
/// </summary>
public class PlayerNormalAttackState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        //播放攻击动画
        playerController.PlayAnimation("Attack_Normal_" + playerModel.skillConfig.currentNormalAttackIndex);
    }

    public override void Update()
    {
        base.Update();
        #region 检测动画是否结束
        if(IsAnimationEnd())
        {
            //切换到普通攻击后摇状态
            playerController.SwitchState(PlayerState.NormalAttackEnd);
            return;
        }
        #endregion 
    }
}
