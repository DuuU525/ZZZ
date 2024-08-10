using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
}
public class PlayerModel : MonoBehaviour
{
    public Animator animator;
    [HideInInspector]
    //玩家状态
    public PlayerState state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
