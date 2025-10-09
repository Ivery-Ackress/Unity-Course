using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private List<DiceSide> diceSides;
    private int currentValue;
    
    public Rigidbody Rb => rb;

    public List<DiceSide> DiceSides => diceSides;
    
    public int CurrentValue
    {
        get { return Rb.IsSleeping() ? currentValue : 0; }
        private set { currentValue = value; }
    }

    private void Awake()
    {
        foreach(var side in DiceSides)
        {
            side.TriggerEntering.AddListener(OnTriggerEntered);
        }
    }

    private void OnTriggerEntered(DiceSide side)
    {
        CurrentValue = 7 - side.Value;
    }
}
