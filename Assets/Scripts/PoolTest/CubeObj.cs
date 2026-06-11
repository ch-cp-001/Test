using UnityEngine;

public class CubeObj : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DestroyMe",3f);
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        transform.Translate(transform.forward*5*Time.deltaTime);
    }

    private void DestroyMe()
    {
        PoolMgr.Instance.PushObj(this.gameObject);
    }
}
