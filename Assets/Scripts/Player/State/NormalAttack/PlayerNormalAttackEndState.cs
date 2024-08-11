using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通攻击后摇动画状态
/// </summary>
public class PlayerNormalAttackEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        //播放普通攻击后摇动画
        playerController.PlayAnimation($"Attack_Normal_{playerModel.skillConfig.currentNormalAttackIndex}_End");
    }
    public override void Update()
    {
        base.Update();

        #region 检测攻击状态
        if (playerController.inputSystem.Player.Fire.triggered)
        {
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
        #endregion

        #region 检测闪避
        if (playerController.inputSystem.Player.Evade.triggered)
        {
            //切换闪避状态
            playerController.SwitchState(PlayerState.Evade_Back);
            playerModel.skillConfig.currentNormalAttackIndex = 1;
            return;
        }
        #endregion
        #region 监听移动
        if (playerController.inputMoveVec2 != Vector2.zero && animationPlayTime > 0.5f)
        {
            //切换到移动状态
            playerController.SwitchState(PlayerState.Run);
            playerModel.skillConfig.currentNormalAttackIndex = 1;
            return;
        }
        #endregion

        #region 检测动画播放结束
        if (IsAnimationEnd())
        {
            //切换到待机状态
            playerController.SwitchState(PlayerState.Idle);
            //攻击段数重置
            playerModel.skillConfig.currentNormalAttackIndex = 1;
            return;
        }
        #endregion
    }
}
