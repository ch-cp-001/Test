
using UnityEngine;
using UnityEngine.UI;

public class AAMainTest : MonoBehaviour
{
    public Image img1;
    public Image img2;
    
    // Start is called before the first frame update
    void Start()
    {
        //ResMgr.Instance.LoadAsync<Sprite>("img", CreateCube1);
        //Debug.Log(ResMgr.Instance.GetRefNum<Sprite>("img"));

        //ResMgr.Instance.LoadAsync<Sprite>("img", CreateCube2);
        //Debug.Log(ResMgr.Instance.GetRefNum<Sprite>("img"));

        //ResMgr.Instance.UnLoad<Sprite>("img");
        //Debug.Log(ResMgr.Instance.GetRefNum<Sprite>("img"));
        //ResMgr.Instance.UnLoad<Sprite>("img");
        //Debug.Log(ResMgr.Instance.GetRefNum<Sprite>("img"));

        //GameObject obj = Instantiate (EditorMgr.Instance.LoadEditorRes<GameObject>("Sphere"));
        //MeshRenderer r = obj.GetComponent<MeshRenderer>();
        //r.material = EditorMgr.Instance.LoadEditorRes<Material>("MaterialTest");
    }

    private void CreateCube1(Sprite sprite)
    {
        img1.sprite = sprite;
    }
    private void CreateCube2(Sprite sprite)
    {
        img2.sprite = sprite;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PoolMgr.Instance.GetObj("Cube");
        }

    }
}
