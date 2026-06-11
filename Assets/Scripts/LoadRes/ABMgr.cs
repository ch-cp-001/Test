using UnityEngine;




public class ABMgr : SingletonBase<ABMgr>
{
    private ABMgr() { }

    // ab包加载路径
    public string abPath
    {
        get
        {
            return Application.streamingAssetsPath+"/";
        }
    }

    //public void LoadDependency(string pkgName)
    //{
    //    string path = abPath+
    //    AssetBundle.LoadFromFile();
    //}

    //public T LoadABRes<T>(string pkgName,string resName)
    //{
    //    // 先加载依赖包


    //    return null;
    //}
}
