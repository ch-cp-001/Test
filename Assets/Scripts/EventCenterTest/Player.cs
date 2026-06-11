using UnityEngine;

public class Player : MonoBehaviour
{
    public int money = 0;

    private void Awake()
    {
        EventCenter.Instance.AddListener<Monster>(E_EventType.E_Monster_Dead, GetReward);
    }


    public void GetReward(Monster monster)
    {
        money += monster.money;
        Debug.Log("Õ½¶·Ê¤Àû");
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveListener<Monster>(E_EventType.E_Monster_Dead, GetReward);
    }
}
