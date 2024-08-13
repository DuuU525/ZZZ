using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public enum ModelFoot
{
    Right,
    Left
}

/// <summary>
/// 玩家模型
/// </summary>
public class PlayerModel : MonoBehaviour, IHurt
{
    //动画
    [HideInInspector] public Animator animator;

    //玩家状态
    [HideInInspector] public PlayerState currentState;
    //角色控制器
    [HideInInspector] public CharacterController characterController;
    //重力
    public float gravity = -9.8f;
    //技能配置文件
    public SkillConfig skillConfig;

    //大招Start镜头
    public GameObject bigSkillStartShot;
    //大招收尾镜头
    public GameObject bigSkillShot;
    //武器列表
    public WeaponController[] weapons;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    /// <summary>
    /// 初始化武器
    /// </summary>
    /// <param name="enemyTagList"></param>
    public void Init(List<string> enemyTagList)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Init(enemyTagList, OnHit);
        }
    }

    /// <summary>
    /// 命中事件
    /// </summary>
    private void OnHit(IHurt enemy)
    {
        Debug.Log(((Component)enemy).name);
    }
    #region 动画状态
    [HideInInspector] public ModelFoot foot = ModelFoot.Right;

    //动画信息
    protected AnimatorStateInfo stateInfo;


    /// <summary>
    /// 进入模型
    /// </summary>
    public void Enter(Vector3 pos, Quaternion rot)
    {
        //强行移除退场逻辑
        MonoManager.INSTANCE.RemoveUpdateAction(OnExit);

        #region 设置角色出场位置
        //计算右方向向量
        Vector3 rightDirection = rot * Vector3.right;
        //向右偏移0.8个单位
        pos += rightDirection * 0.8f;

        //计算后方向向量
        Vector3 backDirection = rot * Vector3.back;
        pos += backDirection * 3f;

        characterController.Move(pos - transform.position);
        transform.rotation = rot;
        #endregion
    }

    /// <summary>
    /// 模型退场
    /// </summary>
    public void Exit()
    {
        animator.CrossFadeInFixedTime("SwitchOut_Normal", 0.1f);
        MonoManager.INSTANCE.AddUpdateAction(OnExit);
    }

    /// <summary>
    /// 退场逻辑
    /// </summary>
    public void OnExit()
    {
        #region 检测动画是否播放结束
        if (IsAnimationEnd())
        {
            gameObject.SetActive(false);
            MonoManager.INSTANCE.RemoveUpdateAction(OnExit);
        }
        #endregion
    }
    /// <summary>
    /// 判断动画是否结束
    /// </summary>
    public bool IsAnimationEnd()
    {
        #region 动画是否播放结束
        //刷新动画状态信息
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0);
        #endregion
    }

    /// <summary>
    /// 迈出左脚
    /// </summary>
    public void SetOutLeftFoot()
    {
        foot = ModelFoot.Left;
    }
    /// <summary>
    /// 迈出右脚
    /// </summary>
    public void SetOutRightFoot()
    {
        foot = ModelFoot.Right;
    }
    #endregion

    private void OnDisable()
    {
        //重置普通攻击段数
        skillConfig.currentNormalAttackIndex = 1;
    }
}
