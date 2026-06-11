using UnityEngine;

public class Monster : MonoBehaviour
{
    public int money;

    private void Awake()
    {
        Invoke("Dead", 2f);
    }


    public void Dead()
    {
        Debug.Log("¹ÖÎïËÀÍö");
        DeadTrigger();
    }

    public void DeadTrigger()
    {
        EventCenter.Instance.EventTrigger<Monster>(E_EventType.E_Monster_Dead, this);
    }
}
