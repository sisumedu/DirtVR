using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// 矢
/// </summary>
public class ArrowObject : MonoBehaviour
{
    Collider m_body;

    Rigidbody m_rigit;

    void Awake()
    {

        m_rigit = GetComponent<Rigidbody>();
        m_rigit.isKinematic = true;
        m_rigit.useGravity = false;
        m_body = GetComponent<Collider>();
        m_body.enabled = false;

        //何かに衝突したら遅れて削除される
        //this.OnCollisionEnterAsObservable()
        //    .Delay(TimeSpan.FromMilliseconds(3000.0))
        //    .Subscribe(p => Destroy(gameObject)).AddTo(gameObject);

        //何かに刺さったら止まる
        //this.OnCollisionEnterAsObservable()
        //    .Subscribe(p => Stop(p.transform));

    }


    /// <summary>
    /// 矢を放つ
    /// </summary>
    /// <param name="power">Power.</param>
    public void Shot(Vector3 power)
    {
        Debug.Log("Arrow Shot");
        m_body.enabled = true;
        m_rigit.isKinematic = false;
        m_rigit.useGravity = true;
        m_rigit.AddRelativeForce(power);
        this.FixedUpdateAsObservable().Subscribe(p => FallArrowTask()).AddTo(gameObject);
    }


    /// <summary>
    /// 矢が刺さって止まる
    /// </summary>
    /// <param name="target">Target.</param>
    void Stop(Transform target)
    {
        m_body.enabled = false;
        var ver = m_rigit.velocity;
        m_rigit.velocity = Vector3.zero;
        m_rigit.useGravity = false;
        m_rigit.isKinematic = true;
        //少々突き刺ささる
        m_rigit.transform.position += ver * Time.fixedDeltaTime * 2;
        this.transform.transform.SetParent(target);
    }


    /// <summary>
    /// 矢じりを落ちる方向に向ける
    /// </summary>
    void FallArrowTask()
    {
        if (!m_rigit.isKinematic)
        {
            var ver = m_rigit.velocity;
            var falldir = ver.normalized;
            //落ちる方向を向くようにする
            m_rigit.MoveRotation(Quaternion.LookRotation(falldir));
        }

    }
}
