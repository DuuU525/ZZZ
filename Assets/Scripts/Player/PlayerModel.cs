using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    RunEnd
}

public enum ModelFoot
{
    Right,
    Left
}
public class PlayerModel : MonoBehaviour
{
    public Animator animator;
    
    //玩家状态
    [HideInInspector] public PlayerState state;
    
    #region 动画状态
    public ModelFoot foot = ModelFoot.Right;
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
}
