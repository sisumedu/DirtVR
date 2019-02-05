using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationScript : MonoBehaviour
{
    private Queue<float> lastXAccels = new Queue<float>();
    private Queue<float> lastYAccels = new Queue<float>();
    private Queue<float> lastZAccels = new Queue<float>();

    // 保持するフレーム数
    private readonly int ACCEL_NUM = 15;

    private void Start()
    {
        // 加速度のキューを0埋めしておく
        for (int i = 0; i < ACCEL_NUM; i++)
        {
            lastXAccels.Enqueue(0);
            lastYAccels.Enqueue(0);
            lastZAccels.Enqueue(0);
        }
    }

    private void Update()
    {
        //float x = Input.acceleration.x;
        //float y = Input.acceleration.y;
        //float z = Input.acceleration.z;

        float x = SmartPhoneInput.PadGradX;
        float y = SmartPhoneInput.PadGradY;
        float z = SmartPhoneInput.PadGradZ;

        
        // 一番古い加速度をDequeueで削除。最新の加速度をEnqueueで追加
        lastXAccels.Dequeue();
        lastXAccels.Enqueue(x);
        lastYAccels.Dequeue();
        lastYAccels.Enqueue(y);
        lastZAccels.Dequeue();
        lastZAccels.Enqueue(z);
    }

    public Vector3 CalcVelocity()
    {
        float xVelocity;
        float yVelocity;
        float zVelocity;

        // |x|の最大値
        var xMax = lastXAccels.Select(x => Mathf.Abs(x)).Max();
        // z方向の速度（奥行方向の速度をxMaxを元に計算）
        zVelocity = Mathf.Clamp(xMax, 0, 2) * 20;

        // yの絶対値平均誤差
        var yAverage = lastYAccels.Average();
        var yVariance = lastYAccels
            .Select(y => Mathf.Abs(y - yAverage))
            .Sum() / lastYAccels.Count;
        yVelocity = yVariance * Mathf.Sign(lastYAccels.Last()) * 15;

        // zの絶対値平均誤差
        var zAverage = lastZAccels.Average();
        var zVariance = lastZAccels
            .Select(z => Mathf.Abs(z - zAverage))
            .Sum() / lastZAccels.Count;
        xVelocity = zVariance * Mathf.Sign(lastZAccels.Last()) * 20;

        var vec = new Vector3(xVelocity, yVelocity, zVelocity);

        return vec;
    }
}