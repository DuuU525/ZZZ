using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制器
/// </summary>
public class PlayerController : SingleMonoBase<PlayerController>, IStateMachineOwner
{
    //输入系统
    [HideInInspector] public InputSystem inputSystem;
    //玩家移动输入
    public Vector2 inputMoveVec2;
    //玩家模型
    public PlayerModel playerModel;
    //转向速度
    public float rotationSpeed = 8f;
    private StateMachine stateMachine;
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);
        inputSystem = new InputSystem();
    }
    private void Start()
    {
        //切换到待机状态
        SwitchState(PlayerState.Idle);
    }
    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="playerState">状态</param>
    public void SwitchState(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                stateMachine.EnterState<PlayerIdleState>();
                break;
            case PlayerState.Run:
                stateMachine.EnterState<PlayerRunState>();
                break;
        }
        playerModel.state = playerState;
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animationName">动画名称</param>
    /// <param name="fixeTransitionDuration">过渡时间</param>
    public void PlayAnimation(string animationName, float fixeTransitionDuration = 0.25f)
    {
        playerModel.animator.CrossFadeInFixedTime(animationName, fixeTransitionDuration);
    }

    private void Update()
    {
        //更新玩家的移动输入
        inputMoveVec2 = inputSystem.Player.Move.ReadValue<Vector2>().normalized;
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }
    private void OnDisable()
    {
        inputSystem.Disable();
    }
}
