 using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器控制器
/// </summary>
public class WeaponController : MonoBehaviour
{
    // 敌人标签列表
    private List<string> enemyTagList = new List<string>();
    //单次攻击的敌人受击列表
    private List<IHurt> enemyHurtList = new List<IHurt>();
    //武器触发器
    [SerializeField] private Collider hitCollider;
    //命中事件
    private Action<IHurt> onHitAction;
    public void Init(List<string> enemyTagList, Action<IHurt> onHitAction)
    {
        this.enemyTagList.AddRange(enemyTagList);
        this.onHitAction = onHitAction;
        hitCollider.enabled = false;
    }

    /// <summary>
    /// 开启触发器
    /// </summary>
    public void StartHit()
    {
        hitCollider.enabled = true;
    }

    /// <summary>
    /// 关闭伤害检测
    /// </summary>
    public void StopHit()
    {
        hitCollider.enabled = false;
        enemyHurtList.Clear();
    }

    private void OnTriggerStay(Collider other)
    {
        //检测打击对象的标签
        if (enemyTagList.Contains(other.tag))
        {
            IHurt enemy = other.GetComponent<IHurt>();
            if (enemy != null && !enemyHurtList.Contains(enemy))
            {
                enemyHurtList.Add(enemy);
                #region 让敌人受击
                //触发命中事件（通知上级处理敌人受击）
                onHitAction?.Invoke(enemy);
                #endregion
            }
            else
            {
                if(!enemyHurtList.Contains(enemy))
                {
                    Debug.LogError($"该受击对象{other.name}不包含受击接口");
                }
            }
        }
    }
}
