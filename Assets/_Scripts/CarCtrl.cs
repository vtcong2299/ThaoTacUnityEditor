using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCtrl : MonoBehaviour
{
    static public CarCtrl instance;
    private void Awake()
    {
        CarCtrl.instance = this;
    }
}
