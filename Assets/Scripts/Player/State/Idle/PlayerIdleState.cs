using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("Idle");
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
        #region 监听移动
        if(playerController.inputMoveVec2 != Vector2.zero)
        {
            //切换到奔跑状态
            playerController.SwitchState(PlayerState.Run);
            return;
        }
        #endregion
    }
}
