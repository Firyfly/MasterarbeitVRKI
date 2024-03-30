using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------
//Tracks the distance which the hands are moving
//-----------------------------------------

public class MeasureLoudness : MonoBehaviour
{

    //gets the loudness from the audio clip 
    public float GetLoudnessFromAudioClip(AudioClip clip)
    {

        float[] waveData = new float[clip.samples];
        clip.GetData(waveData, 0);

        float totalLoudness = 0;

        for(int i = 0; i < clip.samples; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / clip.samples;
    }


}
