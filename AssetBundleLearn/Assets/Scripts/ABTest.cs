using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ABTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Image img;
    public RawImage rawImage;
    void Start()
    {
        GameObject  obj = ABMgr.GetInstance().LoadRes<GameObject>("model","Cube");
        obj.transform.position = Vector3.up;
        GameObject obj2 = ABMgr.GetInstance().LoadRes<GameObject>("model", "Sphere");
        obj2.transform.position = -Vector3.up;
        //    //关于AB包的依赖——一个资源身上用到了别的AB资源包中的资源 这个时候 如果只加载自己的AB包
        //    //通过它创建对象 会出现资源丢失的情况
        //    //这种时候 需要把依赖包一起加载了 才能正常使用

        //    //依赖包的关键知识点——利用主包 获取依赖信息
        //    //加载主包
        //  AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "PC");
        //    //加载主包中方的固定文件
        //  AssetBundleManifest abMainfest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //    //从固定文件中 得到依赖信息
        //    string[] strs = abMainfest.GetAllDependencies("model");
        //    //得到了依赖包的名字
        //    foreach (var item in strs) 
        //    {
        //        AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + item);
        //    }

        //    //第一步 加载AB包
        //    AssetBundle ab= AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/"+"model");
        //    //第二步 加载AB包中的资源
        //    //只是使用名字加载 会出现 同名不同类型的资源分不清
        //    //建议使用 泛型或者是Type指定类型

        //    GameObject obj= ab.LoadAsset("Cube",typeof(GameObject))as GameObject;
        //    Instantiate(obj);


        //    //AB 包不能重复加载 否则会报错
        //    // AssetBundle ab2 = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");
        //    //GameObject obj2 = ab.LoadAsset<GameObject>("Sphere");
        //    //Instantiate(obj2);

        //    //异步加载——>协程
        //    //StartCoroutine(LoadABRes("ui", "BG"));
    }

    //IEnumerator LoadABRes(string ABName,string resname) {

    //    //第一步 加载AB包
    //  AssetBundleCreateRequest abcr=AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);
    //    yield return abcr;
    //    //第二步 加载资源
    //   AssetBundleRequest abq= abcr.assetBundle.LoadAssetAsync<Sprite>(resname);
    //    yield return abq;
    //    rawImage.texture =(abq.asset as Sprite).texture;
    //    img.sprite = abq.asset as Sprite; 
    //}
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        //卸载所有加载的AB包 参数为True 会把包加载的资源也卸载了。
    //        AssetBundle.UnloadAllAssetBundles(false);
    //    }
    //}
}
