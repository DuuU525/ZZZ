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

        #region 检测闪避
        if(playerController.inputSystem.Player.Evade.IsPressed())
        {
            //切换闪避状态
            playerController.SwitchState(PlayerState.Evade_Back);
        }
        #endregion
        #region 监听奔跑
        if(playerController.inputMoveVec2 != Vector2.zero)
        {
            //切换到奔跑状态
            playerController.SwitchState(PlayerState.Run);
        }
        #endregion
    }
}
