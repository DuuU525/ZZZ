using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum ModelFoot
{
    Right,
    Left
}

public class PlayerModel : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    //玩家状态
    [HideInInspector] public PlayerState state;
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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    #region 动画状态
    [HideInInspector] public ModelFoot foot = ModelFoot.Right;
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
