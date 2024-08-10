using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家180转身动画
/// </summary>
public class PlayerTurnBackState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("TurnBack", 0.1f);
    }

    public override void Update()
    {
        base.Update();

        #region 检测动画是否结束
        if(stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        {
            playerController.SwitchState(PlayerState.Run);
        }
        #endregion
    }
}
