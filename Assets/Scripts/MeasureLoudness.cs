using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureLoudness : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetLoudnessFromAudioClip(AudioClip clip)
    {

        float[] waveData = new float[clip.samples];
        clip.GetData(waveData, 0);

        float totalLoudness = 0;

        for(int i = 0; i < clip.samples; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        Debug.Log(totalLoudness / clip.samples);
        return totalLoudness / clip.samples;
    }


}
