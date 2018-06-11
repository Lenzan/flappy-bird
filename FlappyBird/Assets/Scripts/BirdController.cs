using System;
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

    private Vector3 localPos;
    //private Vector2 perPos;
    //private float offset;
    //private Vector3 targetRotation;
    //public Vector3 target;
    //public float step;
    //public float height;
    //public float rotate;
    //public float rotateTime;

    //当前的状态
    public State currentState;
    private Animator animator;
    private void Start() {
        instance = this;
        currentState = State.Idle;
        animator = GetComponent<Animator>();
        localPos = transform.localPosition;
        //perPos = transform.localPosition;
    }

    public void InitPos()
    {
        transform.localPosition = localPos;
        speed = 10;
    }
    /// <summary>
    ///  更新小鸟的动画状态
    /// </summary>
    private void Update() {
        if (currentState == State.Idle) return;
        if (Input.GetMouseButtonDown(0) && currentState != State.Die)
        {
            AudioManager.instance.AudioShotPlay("sfx_swooshing");
            vecY = fixedSpeed;
        }
        vecY -= Time.deltaTime * speed;
        transform.position += new Vector3(0, vecY, 0);
        #region Lerp
        //offset = transform.position.magnitude - perPos.magnitude;
        //perPos = transform.position;
        //if (offset > 0.15f)
        //{
        //    step = 1f;
        //}
        //if (offset < 0.15f)
        //{
        //    target = new Vector3(0, -182, 0);
        //    targetRotation = new Vector3(0, 0, -90);
        //    step = 0.2f;
        //    rotateTime = 1.5f;
        //}
        //if (Input.GetMouseButtonDown(0)&& currentState != State.Die)
        //{
        //    AudioManager.instance.AudioShotPlay("sfx_swooshing");
        //    if (transform.position.y > 520)
        //    {
        //        target = new Vector3(0f, 520, 0);
        //    }
        //    else
        //    {
        //        target = transform.localPosition + new Vector3(0, height, 0);
        //    }
        //    targetRotation = new Vector3(0, 0, rotate);
        //}
        //transform.DORotate(targetRotation, rotateTime);
        //transform.localPosition = Vector3.Lerp(transform.localPosition, target, step * Time.deltaTime * 10);
        #endregion
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