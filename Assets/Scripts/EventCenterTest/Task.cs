using UnityEngine;

public class Task : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.Instance.AddListener<Monster>(E_EventType.E_Monster_Dead, Task1);
    }

    public void Task1(Monster monster)
    {
        Debug.Log("任务完成进度加一");
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveListener<Monster>(E_EventType.E_Monster_Dead, Task1);
    }
}
