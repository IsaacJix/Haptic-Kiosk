using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ultrahaptics;

public class Unity_AMFocus : MonoBehaviour
{
    AmplitudeModulationEmitter _emitter;

    void Start()
    {
        // Initialize the emitter
        _emitter = new AmplitudeModulationEmitter();
        _emitter.initialize();
    }

    // Update on every frame
    void Update()
    {
        // Set the position to be 20cm above the centre of the array
        Ultrahaptics.Vector3 position = new Ultrahaptics.Vector3(0.0f, 0.0f, 0.2f);
        // Create a control point object using this position, with full intensity, at 200Hz
        AmplitudeModulationControlPoint point = new AmplitudeModulationControlPoint(position, 1.0f, 200.0f);
        // Output this point; technically we don't need to do this every update since nothing is changing.
        _emitter.update(new List<AmplitudeModulationControlPoint> { point });
    }

    // Ensure the emitter is stopped on exit
    void OnDisable()
    {
        _emitter.stop();
    }

    // Ensure the emitter is immediately disposed when destroyed
    void OnDestroy()
    {
        if (_emitter != null)
        {
            _emitter.Dispose();
            _emitter = null;
        }
    }
}
