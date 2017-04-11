using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    // 加载场景管理类
    private static SceneMgr _instance;
    public static SceneMgr Instance { get { return _instance; } }

    // 画布的transform
    private Transform _canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if (_canvasTransform == null)
            { _canvasTransform = GameObject.Find("Canvas").transform; }
            return _canvasTransform;
        }
    }

    public GameObject loadScenePrefab;
    private Slider loadingBar;
    private Text loadingProgress;

    void Awake()
    {
        // 单例模式
        _instance = this;
    }


    public void LoadScene(int sceneId)
    {
        // 初始化
        GameObject go = Instantiate(loadScenePrefab);
        go.transform.SetParent(CanvasTransform);
        go.transform.localPosition = Vector3.zero;
        loadingBar = go.GetComponentInChildren<Slider>();
        loadingProgress = go.GetComponentInChildren<Text>();
        StartCoroutine("LoadNormalScene", sceneId);
    }

    // 协程加载场景
    IEnumerator LoadNormalScene(int sceneId)
    {
        int startProgress = 0;
        int displayProgress = startProgress;
        int toProgress = startProgress;

        // 异步加载场景
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneId);

        // 控制异步加载的场景暂时不进入
        op.allowSceneActivation = false;

        /*
            progress的取值范围在0.1 - 1之间，但是不会等于1
            即progress可能在0.9的时候就会直接进入新场景
            所以需要分别控制两种进度0.1 - 0.9和0.9 - 1
        */

        // 计算读取的进度
        while (op.progress < 0.9f)
        {
            toProgress = startProgress + (int)(op.progress * 100);
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetProgress(displayProgress);
                yield return null;
            }
            yield return null;
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetProgress(displayProgress);
            yield return null;
        }

        // 激活场景
        op.allowSceneActivation = true;
    }

    // 设置进度
    void SetProgress(int progress)
    {
        loadingBar.value = progress * 0.01f;
        loadingProgress.text = progress.ToString() + " %";
    }
}
