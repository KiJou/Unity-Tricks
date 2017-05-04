using System;
using System.IO;
using UnityEngine;

public class RecordSound : MonoBehaviour
{
    private int frequency = 44100;
    private AudioClip clip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // 开始录音
    void StarRecord()
    {
        // deviceName loop lengthSec frequency
        clip = Microphone.Start(null, false, 30, frequency);
    }

    // 停止录音
    void StopRrecord()
    {
        float audioLength;
        int lastPos = Microphone.GetPosition(null);
        if (Microphone.IsRecording(null))
        {
            audioLength = (float)lastPos / frequency;
        }
        else
        {
            audioLength = 30.0f;
        }

        // deviceName
        Microphone.End(null);

        if (audioLength < 1.0f)
        {
            Debug.Log("录音时间太短...");
        }
        else
        {
            Debug.Log("录音时间 : " + audioLength);
        }
    }

    // 播放录音
    void PlayRecord()
    {
        audioSource.PlayOneShot(clip);
    }

    // 保存录音
    void StoreRecord()
    {
        string path = "F:/HelloWorld/Unity/Unity Tricks/Assets/NO19/Sound/";
        string name = "test.wav";
        using (FileStream fileStream = CreateEmpty(path + name))
        {
            ConvertAndWrite(fileStream, clip);
            WriteHeader(fileStream, clip);
        }
    }

    void OnGUI()
    {
        // 开始录音
        if (GUILayout.Button("开始录音"))
        {
            Debug.Log("开始录音");
            StarRecord();
        }
        // 停止录音
        if (GUILayout.Button("停止录音"))
        {
            Debug.Log("停止录音");
            StopRrecord();
        }
        // 播放录音
        if (GUILayout.Button("播放录音"))
        {
            Debug.Log("播放录音");
            PlayRecord();
        }
        // 保存录音
        if (GUILayout.Button("保存录音"))
        {
            Debug.Log("保存录音");
            StoreRecord();
        }
    }

    //////////
    private static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
    {

        float[] samples = new float[clip.samples];

        clip.GetData(samples, 0);

        short[] intData = new short[samples.Length];

        // bytesData array is twice the size of
        // dataSource array because a float converted in Int16 is 2 bytes.
        byte[] bytesData = new byte[samples.Length * 2];

        // to convert float to Int16
        int rescaleFactor = 32767;

        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor);
            byte[] byteArr = new byte[2];
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);
        }
        fileStream.Write(bytesData, 0, bytesData.Length);
    }
    private static FileStream CreateEmpty(string filepath)
    {
        FileStream fileStream = new FileStream(filepath, FileMode.Create);
        byte emptyByte = new byte();

        // preparing the header
        for (int i = 0; i < 44; i++)
        {
            fileStream.WriteByte(emptyByte);
        }

        return fileStream;
    }
    private static void WriteHeader(FileStream stream, AudioClip clip)
    {
        int hz = clip.frequency;
        int channels = clip.channels;
        int samples = clip.samples;

        stream.Seek(0, SeekOrigin.Begin);

        byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        stream.Write(riff, 0, 4);

        byte[] chunkSize = BitConverter.GetBytes(stream.Length - 8);
        stream.Write(chunkSize, 0, 4);

        byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        stream.Write(wave, 0, 4);

        byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        stream.Write(fmt, 0, 4);

        byte[] subChunk1 = BitConverter.GetBytes(16);
        stream.Write(subChunk1, 0, 4);

        ushort one = 1;

        byte[] audioFormat = BitConverter.GetBytes(one);
        stream.Write(audioFormat, 0, 2);

        byte[] numChannels = BitConverter.GetBytes(channels);
        stream.Write(numChannels, 0, 2);

        byte[] sampleRate = BitConverter.GetBytes(hz);
        stream.Write(sampleRate, 0, 4);

        // sampleRate * bytesPerSample * number of channels, here 44100 * 2 * 2  
        byte[] byteRate = BitConverter.GetBytes(hz * channels * 2);
        stream.Write(byteRate, 0, 4);

        ushort blockAlign = (ushort)(channels * 2);
        stream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

        ushort bps = 16;
        byte[] bitsPerSample = BitConverter.GetBytes(bps);
        stream.Write(bitsPerSample, 0, 2);

        byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
        stream.Write(datastring, 0, 4);

        byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        stream.Write(subChunk2, 0, 4);
    }
    //////////
}
