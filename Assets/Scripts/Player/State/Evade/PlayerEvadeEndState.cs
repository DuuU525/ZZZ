using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家闪避结束状态
/// </summary>
public class PlayerEvadeEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        #region  判断前后闪避
        switch (playerModel.state)
        {
            case PlayerState.Evade_Front:
                playerController.PlayAnimation("Evade_Front_End");
                break;
            case PlayerState.Evade_Back:
                playerController.PlayAnimation("Evade_Back_End");
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
