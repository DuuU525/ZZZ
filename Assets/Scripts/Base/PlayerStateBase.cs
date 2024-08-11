using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    RunEnd,
    TurnBack,
    Evade_Front,
    Evade_Back,
    EvadeEnd,
    NormalAttack,
    NormalAttackEnd,
    BigSkillStart,
    BigSkill,
    BigSkillEnd
}
public class PlayerStateBase : StateBase
{
    //玩家控制器
    protected PlayerController playerController;
    //玩家模型
    protected PlayerModel playerModel;
    //动画信息
    protected AnimatorStateInfo stateInfo;
    //当前动画进入的时间
    protected float animationPlayTime = 0f;

    public override void Init(IStateMachineOwner owner)
    {
        playerController = (PlayerController)owner;
        playerModel = playerController.playerModel;

    }
    public override void Enter()
    {
        animationPlayTime = 0;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void LateUpdate()
    {
    }

    public override void UnInit()
    {
    }

    public override void Update()
    {
        //施加重力影响
        playerModel.characterController.Move(new Vector3(0, playerModel.gravity * Time.deltaTime, 0));
        //刷新动画状态信息
        stateInfo = playerModel.animator.GetCurrentAnimatorStateInfo(0);
        //状态进入时间计时
        animationPlayTime += Time.deltaTime;
    }

    /// <summary>
    /// 判断动画是否结束
    /// </summary>
    public bool IsAnimationEnd()
    {
        #region 动画是否播放结束
        return stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0);
        #endregion
    }
}
