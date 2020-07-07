using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjects : MonoBehaviour
{
    public List<GameObject> objects;

    private void OnEnable()
    {
        foreach(GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
}
