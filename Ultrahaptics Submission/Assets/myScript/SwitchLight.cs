using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLight : MonoBehaviour
{
    public GameObject lt;
    private bool toggle = false;
    
    // Interpolate light color between two colors back and forth
    float duration = 1.0f;
    Color color0 = Color.red;
    Color color1 = Color.blue;
    Light lt2;

    public void SwitchLightON()
    {
        if (!toggle)
        {
            lt.SetActive(true);
            toggle = true;
        }
        else
        {
            lt.SetActive(false);
            toggle = false;
        }
    }

    public void ChangeLightColor()
    {
        lt2 = GetComponent<Light>();
        // set light color
        float t = Mathf.PingPong(Time.time, duration) / duration;
        lt2.color = Color.Lerp(color0, color1, t);
    }
}
