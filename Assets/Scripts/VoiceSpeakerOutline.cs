using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceSpeakerOutline : MonoBehaviour
{
    public PhotonVoiceView photonVoiceView;
    public Outline outline;

    // Update is called once per frame
    void Update()
    {
        outline.enabled = photonVoiceView.IsSpeaking;
    }
}
