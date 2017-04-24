using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CityNoise : MonoBehaviour
{
    [SerializeField] private AudioSource _noiseAudioSource;
    [SerializeField] private AnimationCurve _noiseCurve;

	private void Update ()
	{
	    _noiseAudioSource.volume = 0.2f + _noiseCurve.Evaluate(Time.time) * Mathf.Clamp(ParametersCounter.GetPopulationSum() / 750, 0, 1);
	}
}