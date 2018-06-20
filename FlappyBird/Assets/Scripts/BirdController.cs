using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BirdController: MonoBehaviour
{
    public static BirdController instance;
    [HideInInspector]
    public float vecY = 0;
    public float speed;
    public float fixedSpeed;

    public  Vector3 localPos;
    public Vector3 localRotateTarget;
    private float smooth = 0.5f;

    //当前的状态
    public State currentState;
    private Animator animator;
    public bool isDie = false;
    private void Start() {
        instance = this;
        currentState = State.Idle;
        animator = GetComponent<Animator>();
        localPos = transform.localPosition;
    }

    public void InitPos()
    {
        transform.localPosition = localPos;
        transform.localEulerAngles = Vector3.zero;
    }

    /// <summary>
    ///  更新小鸟的动画状态
    /// </summary>
    private void Update() {
        #region 乱七八糟
        if (currentState == State.Idle || isDie )
        {
            return;
        }
        if (currentState == State.Fly && isDie == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.instance.AudioShotPlay("sfx_swooshing");
                localRotateTarget = new Vector3(0, 0, 40);
                vecY = fixedSpeed;
                smooth = 0.3f;
            }
            if (vecY < 0)
            {
                localRotateTarget = new Vector3(0, 0, 0);
            }
            vecY -= Time.deltaTime * speed;
            transform.position += new Vector3(0, vecY, 0);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, localRotateTarget, smooth);
        }
        else if (currentState == State.Die && isDie == false)
        {
            isDie = true;
            smooth = 0.5f;
            transform.DOLocalMoveY(-580, smooth);
            transform.DOLocalRotate(new Vector3(0, 0, -90), smooth);
            StartCoroutine(Play());
        }
        #endregion
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(0.3f);
        AudioManager.instance.AudioShotPlay("sfx_die");
    }
    /// <summary>
    /// 设置小鸟动画的播放速度
    /// </summary>
    /// <param name="state">状态</param>
    public void SetAnimatorSpeed(State state) {
        currentState = state;
        switch (state)
        {
            case State.Idle:
                animator.speed = 1;
            break;
            case State.Fly:
                animator.speed = 1.5f;
            break;
            case State.Die:
                animator.speed = 0f;
            break;
            default:
            break;
        }
    }

}