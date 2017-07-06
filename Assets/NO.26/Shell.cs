using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [Header("第一个数")]
    public int a;
    [Header("第二个数")]
    public int b;

    void Start()
    {
        StartCoroutine("PyShell");
    }

    IEnumerator PyShell()
    {
        // 初始化程序
        string workingDirectory = Application.dataPath + "/Main/py/";
        string filename = "python";
        string arguments = workingDirectory + "demo.py" + " " + a + " " + b;
        UnityEngine.Debug.Log(arguments);
        ProcessStartInfo proc = new ProcessStartInfo(filename, arguments);
        proc.WorkingDirectory = workingDirectory;
        proc.UseShellExecute = false;
        proc.CreateNoWindow = true;
        proc.WindowStyle = ProcessWindowStyle.Normal;
        proc.RedirectStandardOutput = true;
        proc.RedirectStandardError = true;

        // 启动程序
        Process p = Process.Start(proc);
        StreamReader sr = p.StandardOutput;
        StreamReader srerr = p.StandardError;

        // 等待程序
        while (!p.HasExited) { yield return null; }

        // 读取结果
        while (!sr.EndOfStream) { UnityEngine.Debug.Log(sr.ReadLine()); }
        while (!srerr.EndOfStream) { UnityEngine.Debug.Log(srerr.ReadLine()); }

        UnityEngine.Debug.Log("[Done!]");
        yield return null;
    }
}
