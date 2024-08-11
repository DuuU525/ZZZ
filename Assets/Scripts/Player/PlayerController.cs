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
    //闪避计时器
    private float evadeTimer = 1f;
    private StateMachine stateMachine;
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);
        inputSystem = new InputSystem();
    }
    private void Start()
    {
        LockMouse();
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
            case PlayerState.RunEnd:
                stateMachine.EnterState<PlayerRunEndState>();
                break;
            case PlayerState.TurnBack:
                stateMachine.EnterState<PlayerTurnBackState>();
                break;
            case PlayerState.Evade_Front:
            case PlayerState.Evade_Back:
                if (evadeTimer != 1f)
                {
                    return;
                }
                stateMachine.EnterState<PlayerEvadeState>();
                evadeTimer -= 1f;
                break;
            case PlayerState.EvadeEnd:
                stateMachine.EnterState<PlayerEvadeEndState>();
                break;
            case PlayerState.NormalAttack:
                stateMachine.EnterState<PlayerNormalAttackState>();
                break;
            case PlayerState.NormalAttackEnd:
                stateMachine.EnterState<PlayerNormalAttackEndState>();
                break;
            case PlayerState.BigSkillStart:
                stateMachine.EnterState<PlayerBigSkillStartState>();
                break;
            case PlayerState.BigSkill:
                stateMachine.EnterState<PlayerBigSkillState>();
                break;
            case PlayerState.BigSkillEnd:
                stateMachine.EnterState<PlayerBigSkillEndState>();
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

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animationName">动画名称</param>
    /// <param name="fixeTransitionDuration">过渡时间</param>
    /// <param name="fixedTimeOffect">动画起始播放偏移</param>
    public void PlayAnimation(string animationName, float fixeTransitionDuration, float fixedTimeOffect)
    {
        playerModel.animator.CrossFadeInFixedTime(animationName, fixeTransitionDuration, 0, fixedTimeOffect);
    }

    private void Update()
    {
        //更新玩家的移动输入
        inputMoveVec2 = inputSystem.Player.Move.ReadValue<Vector2>().normalized;
        if (evadeTimer < 1f)
        {
            evadeTimer += Time.deltaTime;
            if (evadeTimer > 1f)
            {
                evadeTimer = 1f;
            }
        }
    }

    private void LockMouse()
    {
        //光标锁定
        Cursor.lockState = CursorLockMode.Locked;
        //隐藏光标
        Cursor.visible = false;
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
