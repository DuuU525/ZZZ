using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家普通攻击状态
/// </summary>
public class PlayerNormalAttackState : PlayerStateBase
{
    //是否进入下一段普通攻击
    private bool enterNextAttack;
    public override void Enter()
    {
        base.Enter();

        enterNextAttack = false;
        //播放攻击动画
        playerController.PlayAnimation("Attack_Normal_" + playerModel.skillConfig.currentNormalAttackIndex);
    }

    public override void Update()
    {
        base.Update();

        //检测是否直接进行下一段攻击
        if (stateInfo.normalizedTime >= 0.5f && playerController.inputSystem.Player.Fire.triggered)
        {
            enterNextAttack = true;
        }

        #region 检测动画是否结束
        if (IsAnimationEnd())
        {
            if (enterNextAttack)
            {
                //切换到下一个普通攻击状态 
                //累加当前攻击段数
                playerModel.skillConfig.currentNormalAttackIndex++;
                if (playerModel.skillConfig.currentNormalAttackIndex > playerModel.skillConfig.normalAttackDamageMultiple.Length)
                {
                    playerModel.skillConfig.currentNormalAttackIndex = 1;
                }
                //切换到攻击状态
                playerController.SwitchState(PlayerState.NormalAttack);
                return;
            }
            else
            {
                //切换到普通攻击后摇状态
                playerController.SwitchState(PlayerState.NormalAttackEnd);
            }
            return;
        }
        #endregion 
    }
}
