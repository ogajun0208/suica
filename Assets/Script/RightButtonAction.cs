using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButtonAction : MonoBehaviour
{
    bool push = false;

    [SerializeField] Cloud_move cloud = null;
    
    public void PushDown(){
        push = true;
    }

    public void PushUp(){
        push = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(push){
            cloud.RightMove();
        }   
    }
}
