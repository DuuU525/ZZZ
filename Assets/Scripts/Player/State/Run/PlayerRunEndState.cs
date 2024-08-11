using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 玩家急停状态
/// </summary>
public class PlayerRunEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        #region 判断左右脚
        switch (playerModel.foot)
        {
            case ModelFoot.Right:
                playerController.PlayAnimation("Run_End_R", 0.1f);
                break;
            case ModelFoot.Left:
                playerController.PlayAnimation("Run_End_L", 0.1f);
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
        if(playerController.inputSystem.Player.Evade.triggered)
        {
            //切换闪避状态
            playerController.SwitchState(PlayerState.Evade_Back);
            return;
        }
        #endregion

        #region 移动监测
        if(playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(PlayerState.Run);
            return;
        }
        #endregion

        #region 动画是否播放结束
        if(IsAnimationEnd())
        {
            playerController.SwitchState(PlayerState.Idle);
            return;
        }
        #endregion
    }
}
