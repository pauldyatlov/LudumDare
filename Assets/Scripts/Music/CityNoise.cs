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
	    var population = GameController.GameStarted ? ParametersCounter.GetPopulationSum() : 0;

        _noiseAudioSource.volume = 0.2f + _noiseCurve.Evaluate(Time.time) * Mathf.Clamp(population / 750, 0, 1);
	}
}