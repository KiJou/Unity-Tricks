using UnityEngine;
using System.Collections.Generic;

///
/// 一个简单的思路就是用Stack来记录物体的Position和Rotation，当需要时间回退的时候就Pop出来，赋值到物体上。
/// 不过为了可以进行拓展，比如只能回退到某段时间内的，而不是一下子回退到最开始的地方，我们需要剔除太久之前的信息。
/// 因此选择使用List而不是Stack。
///
public class TimeTravel : MonoBehaviour
{
    public int Speed = 3;
    public int RotateSpeed = 100;
    public bool ShouldLimit = false;
    public int Limit = 100;  // 可以存放的坐标上限

    private List<Vector3> HistoryPos;
    private List<Quaternion> HistoryRot;
    private bool _IsTimeBack = false;

    void Start()
    {
        HistoryPos = new List<Vector3>();
        HistoryRot = new List<Quaternion>();
    }

    void Update()
    {
        if (_IsTimeBack)
            TimeBack();
        else
            ControlPos();
    }

    void ControlPos()
    {
        //Position
        Vector3 pos = this.transform.position;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > 0.0001f)  // 左右移动
        {
            pos.x += Time.deltaTime * horizontal * Speed;
        }
        if (Mathf.Abs(vertical) > 0.0001f)  // 上下移动
        {
            pos.y += Time.deltaTime * vertical * Speed;
        }
        this.transform.position = pos;

        HistoryPos.Add(pos);  // 加入 Position 列表

        //Rotation
        Quaternion rot = this.transform.rotation;
        Vector3 rotv = rot.eulerAngles;
        float rotate = Input.GetAxis("Fire1");

        if (Mathf.Abs(rotate) > 0.0001f)  // 绕 z 轴旋转
        {
            rotv.z += Time.deltaTime * rotate * RotateSpeed;
        }
        rot = Quaternion.Euler(rotv);
        this.transform.rotation = rot;

        HistoryRot.Add(rot);  // 加入 Rotation 列表

        if (ShouldLimit && HistoryPos.Count > Limit)  // 是否限制列表长度
        {
            HistoryPos.RemoveAt(0);
            HistoryRot.RemoveAt(0);
        }
    }

    void TimeBack()
    {
        if (HistoryPos.Count > 0)
        {
            int index = HistoryPos.Count - 1;
            this.transform.position = HistoryPos[index];
            HistoryPos.RemoveAt(index);
        }
        if (HistoryRot.Count > 0)
        {
            int index = HistoryRot.Count - 1;
            this.transform.rotation = HistoryRot[index];
            HistoryRot.RemoveAt(index);
        }
    }

    void OnGUI()
    {
        if (GUILayout.Button("时间倒流"))
        {
            _IsTimeBack = true;
        }
        if (GUILayout.Button("Reset"))
        {
            HistoryRot.Clear();
            HistoryPos.Clear();
            _IsTimeBack = false;
        }
    }
}