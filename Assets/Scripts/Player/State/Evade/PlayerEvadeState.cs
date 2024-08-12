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
        switch (playerModel.currentState)
        {
            case PlayerState.Idle:
            case PlayerState.RunEnd:
            case PlayerState.NormalAttackEnd:
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
        if (IsAnimationEnd())
        {
            switch (playerModel.currentState)
            {
                case PlayerState.Evade_Front:
                    if (playerController.inputSystem.Player.Evade.IsPressed())
                    {
                        playerController.SwitchState(PlayerState.Run);
                        return;
                    }
                    playerController.SwitchState(PlayerState.Evade_Front_End);
                    break;
                case PlayerState.Evade_Back:
                    playerController.SwitchState(PlayerState.Evade_Back_End);
                    break;
            }
            playerController.SwitchState(PlayerState.EvadeEnd);
            return;
        }
        #endregion
    }
}
