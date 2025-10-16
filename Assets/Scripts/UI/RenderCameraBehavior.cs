using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class RenderCameraBehavior : MonoBehaviour
{
    private Camera _cam;
    
    //TODO: Determine how to let UI access RenderTexture.
    private RenderTexture _renderTexture;
    [SerializeField] private RenderGetter _renderGetter; 
    
    private void Awake()
    {
        _cam = GetComponent<Camera>();    
    }
    
    void Start()
    {
        _cam.clearFlags = CameraClearFlags.SolidColor;
        _cam.backgroundColor = new Color32(0, 0, 0, 0);
        
        //Potentially chaotic and unsure how this'll send over....
        _renderTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
        _renderTexture.Create();
        _cam.targetTexture = _renderTexture;
        
        
        
        UniversalAdditionalCameraData urpData = _cam.GetUniversalAdditionalCameraData();
        urpData.renderPostProcessing = false;
        urpData.renderShadows = false;
        urpData.requiresColorOption = CameraOverrideOption.On;
        
        
        _renderGetter.gameObject.GetComponent<RawImage>().texture = _renderTexture;
    }

    void Update()
    {
        //_cam.targetTexture = _renderTexture;
    }
}
