using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class timeLineTrigger : MonoBehaviour
{
    public PlayableDirector director;

    public List<TimelineAsset> timelines;

    public void PlayerFormTimeLines(int index)
    {
        TimelineAsset selectedAsset;

        if (timelines.Count <= index)
            selectedAsset = timelines[timelines.Count - 1];
        else
            selectedAsset = timelines[index];

        director.Play(selectedAsset);
    }
}
