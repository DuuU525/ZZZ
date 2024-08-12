using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    //玩家信息
    public PlayerConfig playerConfig;
    //配队
    private List<PlayerModel> controllableModels;
    //当前操控的角色下标
    private int currentModelIndex;
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);
        inputSystem = new InputSystem();
        controllableModels = new List<PlayerModel>();

        #region 生成角色模型
        for (int i = 0; i < playerConfig.models.Length; i++)
        {
            GameObject model = Instantiate(playerConfig.models[i], transform);
            model.gameObject.SetActive(false);
            controllableModels.Add(model.GetComponent<PlayerModel>());
        }
        #endregion

        #region 操控配队中第一个角色
        currentModelIndex = 0;
        controllableModels[currentModelIndex].gameObject.SetActive(true);
        playerModel = controllableModels[currentModelIndex];
        #endregion
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
        playerModel.currentState = playerState;
        switch (playerState)
        {
            case PlayerState.Idle:
                stateMachine.EnterState<PlayerIdleState>(true);
                break;
            case PlayerState.Run:
                stateMachine.EnterState<PlayerRunState>(true);
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
            case PlayerState.Evade_Back_End:
            case PlayerState.Evade_Front_End:
                stateMachine.EnterState<PlayerEvadeEndState>();
                break;
            case PlayerState.NormalAttack:
                stateMachine.EnterState<PlayerNormalAttackState>(true);
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
            case PlayerState.SwitchInNormal:
                stateMachine.EnterState<PlayerSwitchInNormalState>(true);
                break;
        }
    }

    /// <summary>
    /// 切换到下一个角色
    /// </summary>
    public void SwitchNextModel()
    {
        //刷新状态机
        stateMachine.Clear();
        //退出当前模型
        playerModel.Exit();
        #region  控制下一个模型
        currentModelIndex++;
        if (currentModelIndex >= controllableModels.Count)
        {
            currentModelIndex = 0;
        }
        PlayerModel nextModel = controllableModels[currentModelIndex];
        nextModel.gameObject.SetActive(true);
        Vector3 prevPos = playerModel.transform.position;
        Quaternion prevRot = playerModel.transform.rotation;
        playerModel = nextModel;
        #endregion
        //进入下一个模型
        playerModel.Enter(prevPos, prevRot);
        //切换到入场状态
        SwitchState(PlayerState.SwitchInNormal);
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
