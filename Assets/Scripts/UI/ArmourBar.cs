using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmourBar : MonoBehaviour
{   public Slider slider;
    public ArmourPickup armourPickup;
    void Awake()
    {   
        slider.maxValue = armourPickup.armourTimeOut ;
        slider.value = 0;
    }

    public void SetArmour(float armourTime)
    {
        slider.value = armourTime;
    }
    
}
