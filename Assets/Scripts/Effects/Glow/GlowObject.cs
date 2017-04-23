using UnityEngine;
using System.Collections.Generic;

public class GlowObject : MonoBehaviour
{
	public Color GlowColor;
	public Renderer[] Renderers { get; private set; }
    private List<Material> _materials = new List<Material>();
	
	void Start()
	{
		Renderers = GetComponentsInChildren<Renderer>();

		foreach (var renderer in Renderers)
			_materials.AddRange(renderer.materials);
	}
    
    private void Update()
	{
		for (int i = 0; i < _materials.Count; i++)
			_materials[i].SetColor("_GlowColor", GlowColor);
	}
}
