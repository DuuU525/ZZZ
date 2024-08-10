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
                playerController.PlayAnimation("Run_End_R");
                break;
            case ModelFoot.Left:
                playerController.PlayAnimation("Run_End_L");
                break;
        }
        #endregion
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

        #region 移动监测
        if(playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(PlayerState.Run);
        }
        #endregion

        #region 动画是否播放结束
        if(stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        {
            playerController.SwitchState(PlayerState.Idle);
        }
        #endregion
    }
}
