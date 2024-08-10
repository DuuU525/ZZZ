using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家闪避状态
/// </summary>
public class PlayerEvadeState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        #region 判断前后闪避
        switch (playerModel.state)
        {
            case PlayerState.Idle:
            case PlayerState.RunEnd:
                playerController.PlayAnimation("Evade_Back");
                break;
            case PlayerState.Run:
                playerController.PlayAnimation("Evade_Front");
                break;
            case PlayerState.TurnBack:
            
                break;
        }
        #endregion
    }

    public override void Update()
    {
        base.Update();
        
        #region 检测动画是否结束
        if(stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        {
            playerController.SwitchState(PlayerState.EvadeEnd);
        }
        #endregion
    }
}
