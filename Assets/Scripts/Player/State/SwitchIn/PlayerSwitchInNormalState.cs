using UnityEngine;

/// <summary>
/// 角色普通入场状态
/// </summary>
public class PlayerSwitchInNormalState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        playerController.PlayAnimation("SwitchIn_Normal", 0f);
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

        #region 检测动画是否结束
        if(IsAnimationEnd())
        {
            //切换到待机
            playerController.SwitchState(PlayerState.Idle);
        }
        #endregion
    }
}