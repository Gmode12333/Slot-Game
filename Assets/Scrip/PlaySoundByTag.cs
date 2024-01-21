using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundByTag : MonoBehaviour
{
    public void PlaySound(string tag)
    {
        SoundManager.Instance.PlaySound(tag);
    }
    
}
