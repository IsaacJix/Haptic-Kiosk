using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDelay : MonoBehaviour
{
    public List<GameObject> ToggleObjects, ActivateObjects, DeactivateObjects;

    public float Delay = 1;

    private float CurrentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        CurrentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;

        if(CurrentTime > Delay)
        {
            foreach (GameObject obj in ToggleObjects)
            {
                obj.SetActive(!obj.activeSelf);
            }
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
}
