using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : MonoBehaviour
{
    [Header("移动速度")]
    public int speed = 5;
    [Header("旋转速度")]
    public int rotateSpeed = 100;
    [Header("长度限制")]
    public int limit = 1024;

    // 用List来记录物体的Position
    private List<Vector3> historyPos = new List<Vector3>();
    // 用List来记录物体的Rotation
    private List<Quaternion> historyRot = new List<Quaternion>();
    // 是否正在倒流
    private bool isTimeBack = false;

    void Update()
    {
        if (isTimeBack)
            TimeBack();
        else
            ControlPos();
    }

    void ControlPos()
    {
        // Position
        Vector3 pos = this.transform.position;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // 左右移动
        if (Mathf.Abs(horizontal) > 0.0001f)
        {
            pos.x += Time.deltaTime * horizontal * speed;
        }
        // 上下移动
        if (Mathf.Abs(vertical) > 0.0001f)
        {
            pos.y += Time.deltaTime * vertical * speed;
        }
        this.transform.position = pos;
        // 加入Position列表
        historyPos.Add(pos);

        // Rotation
        Quaternion rot = this.transform.rotation;
        Vector3 rotv = rot.eulerAngles;
        float rotate = Input.GetAxis("Fire1");
        // 绕z轴旋转
        if (Mathf.Abs(rotate) > 0.0001f)
        {
            rotv.z += Time.deltaTime * rotate * rotateSpeed;
        }
        rot = Quaternion.Euler(rotv);
        this.transform.rotation = rot;
        // 加入Rotation列表
        historyRot.Add(rot);

        // 长度限制
        if (historyPos.Count > limit)
        {
            historyPos.RemoveAt(0);
            historyRot.RemoveAt(0);
        }
    }

    void TimeBack()
    {
        if (historyPos.Count > 0)
        {
            int index = historyPos.Count - 1;
            this.transform.position = historyPos[index];
            historyPos.RemoveAt(index);
        }
        if (historyRot.Count > 0)
        {
            int index = historyRot.Count - 1;
            this.transform.rotation = historyRot[index];
            historyRot.RemoveAt(index);
        }
    }

    void OnGUI()
    {
        if (GUILayout.Button("时间倒流"))
        {
            isTimeBack = true;
        }
        if (GUILayout.Button("时间重置"))
        {
            historyRot.Clear();
            historyPos.Clear();
            isTimeBack = false;
        }
    }
}
