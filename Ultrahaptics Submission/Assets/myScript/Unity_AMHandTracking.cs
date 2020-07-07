using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ultrahaptics;

public class Unity_AMHandTracking : MonoBehaviour
{
    AmplitudeModulationEmitter _emitter;
    Alignment _alignment;
    Leap.Controller _leap;

    public List<GameObject> HandPoints, WorldPoints;

    void Start()
    {
        
        // Initialize the emitter
        _emitter = new AmplitudeModulationEmitter();
        _emitter.initialize();
        _leap = new Leap.Controller();

        // NOTE: This example uses the Ultrahaptics.Alignment class 
        // to convert Leap coordinates to the Ultrahaptics device space.
        // You can either use this as-is for Ultrahaptics development kits, 
        // create your own alignment files for custom devices,
        // or replace the Alignment references in this example with your own
        // coordinate conversion system.

        // Load the appropriate alignment file for the currently-used device
        _alignment = _emitter.getDeviceInfo().getDefaultAlignment();
        // Load a custom alignment file (absolute path, or relative path 
        // from current working directory)
        // _alignment = new Alignment("my_custom.alignment.xml");
    }

    // Converts a Leap Vector directly to a UH Vector3
    Ultrahaptics.Vector3 LeapToUHVector(Leap.Vector vec)
    {
        return new Ultrahaptics.Vector3(vec.x, vec.y, vec.z);
    }

    void Update()
    {
        if (_leap.IsConnected)
        {
            var frame = _leap.Frame();
            if (frame.Hands.Count > 0)
            {
                bool on = false;
                foreach(GameObject obj in HandPoints)
                {
                    if (obj.activeInHierarchy)
                    {
                        on = true;
                    }
                }
                if (!on)
                {
                    foreach (GameObject obj in WorldPoints)
                    {
                        if (obj.activeInHierarchy)
                        {
                            on = true;
                        }
                    }
                }

                if (on)
                {

                    // The Leap Motion can see a hand, so get its palm position
                    Leap.Vector leapPalmPosition = frame.Hands[0].PalmPosition;
                    // Convert to our vector class, and then convert to our coordinate space
                    Ultrahaptics.Vector3 uhPalmPosition1 =
                      _alignment.fromTrackingPositionToDevicePosition(LeapToUHVector(leapPalmPosition));

                    var pointList = new List<AmplitudeModulationControlPoint>();

                    for (int i = 0; i < HandPoints.Count; i++)
                    {
                        if (HandPoints[i].activeInHierarchy)
                        {
                            AmplitudeModulationControlPoint point =
                          new AmplitudeModulationControlPoint(uhPalmPosition1 +
                          new Ultrahaptics.Vector3(HandPoints[i].transform.position.x, HandPoints[i].transform.position.z, HandPoints[i].transform.position.y)
                          , 1.0f, 200.0f);

                            pointList.Add(point);
                        }
                        else
                        {
                            AmplitudeModulationControlPoint point =
                          new AmplitudeModulationControlPoint(uhPalmPosition1 +
                          new Ultrahaptics.Vector3(HandPoints[i].transform.position.x, HandPoints[i].transform.position.z, HandPoints[i].transform.position.y)
                          , 1.0f, 200.0f);

                            pointList.Remove(point);
                        }
                    }

                    for (int i = 0; i < WorldPoints.Count; i++)
                    {
                        if (WorldPoints[i].activeInHierarchy)
                        {
                            AmplitudeModulationControlPoint point =
                              new AmplitudeModulationControlPoint(//uhPalmPosition1 +
                              new Ultrahaptics.Vector3(WorldPoints[i].transform.position.x, WorldPoints[i].transform.position.z, WorldPoints[i].transform.position.y)
                              , 1.0f, 200.0f);

                            pointList.Add(point);
                        }
                        else
                        {
                            AmplitudeModulationControlPoint point =
                              new AmplitudeModulationControlPoint(//uhPalmPosition1 +
                              new Ultrahaptics.Vector3(WorldPoints[i].transform.position.x, WorldPoints[i].transform.position.z, WorldPoints[i].transform.position.y)
                              , 1.0f, 200.0f);

                            if (pointList.Contains(point))
                            {
                                pointList.Remove(point);
                            }
                        }
                    }


                    for (int i = 0; i < pointList.Count; i++)
                    {
                        _emitter.update(new List<AmplitudeModulationControlPoint> { pointList[i] });
                    }
                }
                else
                {
                    _emitter.stop();
                }
            }
            else
            {
                _emitter.stop();
            }
        }
        else
        {
            Debug.LogWarning("No Leap connected");
            _emitter.stop();

           
        }
    }

    // Ensure the emitter is stopped when disabled
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

        if (_alignment != null)
        {
            _alignment.Dispose();
            _alignment = null;
        }
    }
}