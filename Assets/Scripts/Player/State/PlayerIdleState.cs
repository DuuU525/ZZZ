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

        #region 监听奔跑
        if(playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(PlayerState.Run);
        }
        #endregion
    }
}
