using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    private Vector3 _startingPostion;
    private float _movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        _startingPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }

        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // constant value for 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        _movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * _movementFactor;
        transform.position = _startingPostion + offset;
    }
}
