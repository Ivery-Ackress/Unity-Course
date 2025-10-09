using UnityEngine;
using UnityEngine.Events;

public class DiceSide : MonoBehaviour
{
    [SerializeField][Range(1, 6)] private int value;

    public int Value =>  value;

    public UnityEvent<DiceSide> TriggerEntering;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEntering.Invoke(this);
    }
}
