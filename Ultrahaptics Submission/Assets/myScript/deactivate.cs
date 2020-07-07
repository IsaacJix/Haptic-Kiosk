using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivate : MonoBehaviour
{
    public List<GameObject> ActivateObjects, DeactivateObjects;
    public float Wait = 0;

    private void OnEnable()
    {
        if (DeactivateObjects.Count == 0)
        {
            DeactivateObjects.Add(gameObject);
        }
        else
        {
            for(int i = 0; i < DeactivateObjects.Count; i++)
            {
                if(DeactivateObjects[i] == null)
                {
                    DeactivateObjects[i] = gameObject;
                }
            }
        }

        if(Wait > 0)
        {
            Invoke("Deactivate", Wait);
        }
    }

    private void OnDisable()
    {
        Deactivate();
    }

    void Deactivate()
    {
        CancelInvoke("Deactivate");

        foreach (GameObject obj in ActivateObjects)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in DeactivateObjects)
        {
            obj.SetActive(false);
        }
    }
}
