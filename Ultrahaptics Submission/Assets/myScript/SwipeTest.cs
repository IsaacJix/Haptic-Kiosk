using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class SwipeTest : MonoBehaviour
{
    // Start is called before the first frame update

    Controller controller;
    float HandPalmPitch;
    float HandPalmYam;
    float HandPalmRoll;
    float HandWristRot;
    bool shouldMove = true;
   
    void Start()
    {
   
    }

   
    // Update is called once per frame
    void Update()
    {
        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;
        if (frame.Hands.Count > 0)
        {
            Hand fristHand = hands[0];

        }
        HandPalmPitch = hands[0].PalmNormal.Pitch;
        HandPalmRoll = hands[0].PalmNormal.Roll;
        HandPalmYam = hands[0].PalmNormal.Yaw;
        HandWristRot = hands[0].WristPosition.Pitch;

        //Debug.Log("pitch: " + HandPalmPitch);
        Debug.Log("Roll: " + HandPalmRoll);
        // Debug.Log("Yam: " + HandPalmYam);

        //MOVE OBJECT
        if (shouldMove) {
            if (HandPalmRoll > 1.5f)
            {
                transform.Translate(new Vector3(-0.01f * 2, 0, 0));
                if ( transform.position.x < -5 )
                {
                    shouldMove = false;
                }
            }
            else if (HandPalmRoll < 1.5f)
            {
                transform.Translate(new Vector3(0.01f * 2, 0, 0));
                if (transform.position.x > 5)
                {
                    shouldMove = false;
                }
            }

        }

        

    }
}
