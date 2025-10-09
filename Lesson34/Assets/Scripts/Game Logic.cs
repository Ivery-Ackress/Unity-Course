using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Dice dicePrefab;
    private List<Dice> dices;
    [SerializeField] private RandomValue speedBoundary;
    [SerializeField] private RandomValue torqueBoundary;

    public Dice DicePrefab => dicePrefab;

    public RandomValue SpeedBoundary => speedBoundary;

    public RandomValue TorqueBoundary => torqueBoundary;

    public UnityEvent ReDraw;

    public int CurrentValue => dices.Sum(dice => dice.CurrentValue);

    public int DiceCount
    {
        get { return dices.Count; }
        set 
        {
            dices.ForEach(dice => Destroy(dice.gameObject));
            dices.Clear(); 
            CreateDices(value);
        }
    }

    private void Awake()
    {
        dices = new List<Dice>();
    }

    private void FixedUpdate()
    {
        ReDraw.Invoke();
    }

    private void CreateDices(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newDice = Instantiate(DicePrefab);
            newDice.transform.SetParent(transform);
            newDice.transform.position = transform.position + new Vector3(
                Random.Range(-10f, 10f), 
                Random.Range(-5f, 5f),  
                Random.Range(-10f, 10f)  
            );
            dices.Add(newDice);
        }
    }

    public void DropDices()
    {
        foreach (var dice in dices)
        {
            dice.Rb.isKinematic = false;
            dice.Rb.useGravity = true;
            dice.Rb.AddForce(SpeedBoundary.GetRandomValue(),
                            SpeedBoundary.GetRandomValue(),
                            SpeedBoundary.GetRandomValue(),
                            ForceMode.Impulse);
            dice.Rb.AddTorque(TorqueBoundary.GetRandomValue(),
                            TorqueBoundary.GetRandomValue(),
                            TorqueBoundary.GetRandomValue(),
                            ForceMode.Impulse);
        }
    }
}
