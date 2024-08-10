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
