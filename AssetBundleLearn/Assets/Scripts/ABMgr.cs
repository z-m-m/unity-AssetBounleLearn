using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABMgr : SingletonUtoMono<ABMgr>
{
    //AB包管理器 目的是
    //让外部更加方便的进行资源加载
   
    //主包
    private AssetBundle mainAB = null;
    //依赖包获取用的配置文件
    private AssetBundleManifest manifest = null;

    //AB包不能重复加载，重复加载AB包会出错
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();
    //AB包存放路径
    private string PathUrl {
        get {
            return Application.streamingAssetsPath+"/";
        }
    }
    //主包名 方便修改
    private string MainABName {

        get {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "ANDROID";
#else 
            return "PC";
#endif
        }
    }

    public void LoadAB(string abName) {
        //加载AB包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            //加载主包中的关键配置文件
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //获取依赖包相关信息
        string[] strs = manifest.GetAllDependencies(abName);
        AssetBundle ab = null;
        for (int i = 0; i < strs.Length; i++)
        {
            if (!abDic.ContainsKey(strs[i]))
            {
                //加载依赖包
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //加载资源来源包
        //如果没有加载 再加载
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }

    //同步加载 不指定类型
    public object LoadRes(string abName,string resName) {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //为了外面使用方便，在加载资源时 判断一下 资源是不是GameObject
        //如果是 直接实例化 再返回给外部
        Object obj = abDic[abName].LoadAsset(resName);
        if (obj is GameObject)
        {
            return Instantiate(obj);
        }
        else {
            return obj;
        }
    }

    //同步加载根据type指定类型
    public object LoadRes(string abName, string resName,System.Type type)
    {
        LoadAB(abName);
        //加载资源
        //为了外面使用方便，在加载资源时 判断一下 资源是不是GameObject
        //如果是 直接实例化 再返回给外部
        Object obj = abDic[abName].LoadAsset(resName,type);
        if (obj is GameObject)
        {
            return Instantiate(obj);
        }
        else
        {
            return abDic[abName].LoadAsset(resName);
        }
    }

    //同步加载 使用泛型
    public T LoadRes<T>(string abName, string resName) where T:Object
    {
        //加载AB包
        LoadAB(abName);
        //加载资源
        //为了外面使用方便，在加载资源时 判断一下 资源是不是GameObject
        //如果是 直接实例化 再返回给外部
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;
        }
    }
    //异步加载

    //单个包卸载
    public void UnLoad(string abName) {
        if (abDic.ContainsKey(abName)) {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }
    //所有包的卸载
    public void ClearAB() {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}