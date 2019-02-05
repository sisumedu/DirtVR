using UnityEngine;

public class SmoothAccelerometer : MonoBehaviour
{
    [SerializeField] private int _numSamples;
    [SerializeField] private AnimationCurve _weightCurve;

    private Vector3[] _samples;
    private float[] _weights;
    private float _weightSum;
    private int _currentSample;
    private Vector3 _acceleration;

    public Vector3 acceleration
    {
        get { return _acceleration; }
    }

    private void Awake()
    {
        _samples = new Vector3[_numSamples];
        _weights = new float[_numSamples];
        _weightSum = 0;
        for (int i = 0; i < _numSamples; i++)
        {
            _samples[i] = Vector3.zero;
            _weights[i] = _weightCurve.Evaluate(1f - 1f / _numSamples * i);
            _weightSum += _weights[i];
        }
    }

    void Update()
    {
        //_samples[_currentSample] = Input.acceleration;

        _samples[_currentSample] =new Vector3 (SmartPhoneInput.PadGradY*100,SmartPhoneInput.PadGradZ*100,SmartPhoneInput.PadGradX*100);

        _acceleration = Vector3.zero;
        int index;
        for (int i = 0; i < _numSamples; i++)
        {
            index = _currentSample - i;
            if (index < 0)
                index = _numSamples - (i - _currentSample);
            _acceleration += _samples[index] * _weights[index];
        }
        _acceleration /= _weightSum;
        _currentSample = ++_currentSample % _numSamples;


        transform.rotation = Quaternion.Euler(_samples[0]);

    }
}
