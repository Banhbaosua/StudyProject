using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public RobotController robotController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void robotControllereventCheck()
    {
        robotController.eventCheck();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
