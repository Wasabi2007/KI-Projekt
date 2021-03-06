﻿using UnityEngine;

/*
 *	Source: http://www.tasharen.com/forum/index.php?topic=10223.msg48057#msg48057
 *	Author: mathiassoeholm
 */

[ExecuteInEditMode]
public class ClippedModel : MonoBehaviour
{
	private UIPanel _panel;
	private Material _material;
	private Camera _camera;
	
	private int _panelSizeXProperty;
	private int _panelSizeYProperty;
	private int _panelOffsetAndSharpnessProperty;
	
	private float _virtualScreenWidth;
	private float _virtualScreenHeight;
	
	void Start()
	{
		_panel = UIPanel.Find(transform);
		_camera = UICamera.FindCameraForLayer(gameObject.layer).camera;
		_material = !Application.isPlaying ? renderer.sharedMaterial : renderer.material;
		
		_virtualScreenWidth = UIRoot.GetPixelSizeAdjustment(gameObject) * Screen.width;
		_virtualScreenHeight = UIRoot.GetPixelSizeAdjustment(gameObject) * Screen.height;
		
		_panelSizeXProperty = Shader.PropertyToID("_PanelSizeX");
		_panelSizeYProperty = Shader.PropertyToID("_PanelSizeY");
		_panelOffsetAndSharpnessProperty = Shader.PropertyToID("_PanelOffsetAndSharpness");
		
		Update();
	}
	
	void Update()
	{
		if (_panel.hasClipping)
		{
			var soft = _panel.clipSoftness;
			var sharpness = new Vector2(1000.0f, 1000.0f);
			if (soft.x > 0f)
			{
				sharpness.x = _panel.baseClipRegion.z / soft.x;
			}
			if (soft.y > 0f)
			{
				sharpness.y = _panel.baseClipRegion.w / soft.y;
			}
			
			Vector4 panelOffsetAndSharpness;
			
			// Get offset
			Vector2 bottomLeft = _camera.WorldToViewportPoint(_panel.worldCorners[0]);
			panelOffsetAndSharpness.x = bottomLeft.x;
			panelOffsetAndSharpness.y = bottomLeft.y;
			
			// Get sharpness
			panelOffsetAndSharpness.z = sharpness.x;
			panelOffsetAndSharpness.w = sharpness.y;
			
			// Set shader properties
			_material.SetFloat(_panelSizeXProperty, _panel.baseClipRegion.z / _virtualScreenWidth);
			_material.SetFloat(_panelSizeYProperty, _panel.baseClipRegion.w / _virtualScreenHeight);
			_material.SetVector(_panelOffsetAndSharpnessProperty, panelOffsetAndSharpness);
		}
	}
}
