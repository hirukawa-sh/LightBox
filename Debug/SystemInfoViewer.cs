#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Text;

namespace Game.Debugger
{
    /// <summary>
    /// SystemInfoを表示する
    /// </summary>
    public class SystemInfoViewer : MonoBehaviour
    {
        [SerializeField]
        UnityEvent<string> _onUpdateLog;

        /// <summary>
        /// ログ更新頻度(秒)
        /// </summary>
        [SerializeField]
        float _updateTime = 1.0f;

        WaitForSeconds _wait;

        // Start is called before the first frame update
        void Start()
        {
            _wait = new WaitForSeconds(_updateTime);
        }

        void OnEnable()
        {
            StartCoroutine(UpdateLog());
        }

        void OnDisable()
        {
            StopCoroutine(UpdateLog());
        }

        IEnumerator UpdateLog()
        {
            yield return _wait;
            _onUpdateLog.Invoke(GetSystemInfo());
        }

        string GetSystemInfo()
        {
            StringBuilder log = new StringBuilder();

            log.Append($"isPlaying = {Application.isPlaying}\n");
            log.Append($"platform = {Application.platform}\n");
            log.Append($"dataPath = {Application.dataPath}\n");
            log.Append($"absoluteURL = {Application.absoluteURL}\n");
            log.Append($"buildGUID = {Application.buildGUID}\n");
            log.Append($"companyName = {Application.companyName}\n");
            log.Append($"productName = {Application.productName}\n");
            log.Append($"identifier = {Application.identifier}\n");
            log.Append($"installerName = {Application.installerName}\n");
            log.Append($"installMode = {Application.installMode}\n");
            log.Append($"internetReachability = {Application.internetReachability}\n");
            log.Append($"isBatchMode = {Application.isBatchMode}\n");
            log.Append($"isConsolePlatform = {Application.isConsolePlatform}\n");
            log.Append($"isEditor = {Application.isEditor}\n");
            log.Append($"isFocused = {Application.isFocused}\n");
            log.Append($"version = {Application.version}\n");
            log.Append($"unityVersion = {Application.unityVersion}\n");
            log.Append($"targetFrameRate = {Application.targetFrameRate}\n");
            log.Append($"systemLanguage = {Application.systemLanguage}\n");
            log.Append($"isDebugBuild = {Debug.isDebugBuild}\n");
            log.Append($"--------\n");
            log.Append($"batteryLevel = {SystemInfo.batteryLevel}\n");
            log.Append($"batteryStatus = {SystemInfo.batteryStatus}\n");
            log.Append($"deviceModel = {SystemInfo.deviceModel}\n");
            log.Append($"deviceName = {SystemInfo.deviceName}\n");
            log.Append($"deviceType = {SystemInfo.deviceType}\n");
            log.Append($"deviceUniqueIdentifier = {SystemInfo.deviceUniqueIdentifier}\n");
            log.Append($"--------\n");
            log.Append($"graphicsDeviceID = {SystemInfo.graphicsDeviceID}\n");
            log.Append($"graphicsDeviceName = {SystemInfo.graphicsDeviceName}\n");
            log.Append($"graphicsDeviceType = {SystemInfo.graphicsDeviceType}\n");
            log.Append($"graphicsDeviceVendor = {SystemInfo.graphicsDeviceVendor}\n");
            log.Append($"graphicsDeviceVendorID = {SystemInfo.graphicsDeviceVendorID}\n");
            log.Append($"graphicsDeviceVersion = {SystemInfo.graphicsDeviceVersion}\n");
            log.Append($"graphicsMemorySize = {SystemInfo.graphicsMemorySize}\n");
            log.Append($"graphicsMultiThreaded = {SystemInfo.graphicsMultiThreaded}\n");
            log.Append($"graphicsShaderLevel = {SystemInfo.graphicsShaderLevel}\n");
            log.Append($"graphicsUVStartsAtTop = {SystemInfo.graphicsUVStartsAtTop}\n");
            log.Append($"graphicsDeviceVersion = {SystemInfo.graphicsDeviceVersion}\n");
            log.Append($"--------\n");
            log.Append($"hasDynamicUniformArrayIndexingInFragmentShaders = {SystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders}\n");
            log.Append($"hasHiddenSurfaceRemovalOnGPU = {SystemInfo.hasHiddenSurfaceRemovalOnGPU}\n");
            log.Append($"hasMipMaxLevel = {SystemInfo.hasMipMaxLevel}\n");
            log.Append($"hdrDisplaySupportFlags = {SystemInfo.hdrDisplaySupportFlags}\n");
            log.Append($"--------\n");
            log.Append($"maxComputeBufferInputsCompute = {SystemInfo.maxComputeBufferInputsCompute}\n");
            log.Append($"maxComputeBufferInputsDomain = {SystemInfo.maxComputeBufferInputsDomain}\n");
            log.Append($"maxComputeBufferInputsFragment = {SystemInfo.maxComputeBufferInputsFragment}\n");
            log.Append($"maxComputeBufferInputsGeometry = {SystemInfo.maxComputeBufferInputsGeometry}\n");
            log.Append($"maxComputeBufferInputsHull = {SystemInfo.maxComputeBufferInputsHull}\n");
            log.Append($"maxComputeBufferInputsVertex = {SystemInfo.maxComputeBufferInputsVertex}\n");
            log.Append($"maxComputeWorkGroupSize = {SystemInfo.maxComputeWorkGroupSize}\n");
            log.Append($"maxComputeWorkGroupSizeX = {SystemInfo.maxComputeWorkGroupSizeX}\n");
            log.Append($"maxComputeWorkGroupSizeY = {SystemInfo.maxComputeWorkGroupSizeY}\n");
            log.Append($"maxComputeWorkGroupSizeZ = {SystemInfo.maxComputeWorkGroupSizeZ}\n");
            log.Append($"maxCubemapSize = {SystemInfo.maxCubemapSize}\n");
            log.Append($"maxTextureSize = {SystemInfo.maxTextureSize}\n");
            log.Append($"constantBufferOffsetAlignment = {SystemInfo.constantBufferOffsetAlignment}\n");
            log.Append($"--------\n");
            log.Append($"npotSupport = {SystemInfo.npotSupport}\n");
            log.Append($"operatingSystem = {SystemInfo.operatingSystem}\n");
            log.Append($"operatingSystemFamily = {SystemInfo.operatingSystemFamily}\n");
            log.Append($"processorCount = {SystemInfo.processorCount}\n");
            log.Append($"processorFrequency = {SystemInfo.processorFrequency}\n");
            log.Append($"processorType = {SystemInfo.processorType}\n");
            log.Append($"renderingThreadingMode = {SystemInfo.renderingThreadingMode}\n");
            log.Append($"--------\n");
            log.Append($"supportedRandomWriteTargetCount = {SystemInfo.supportedRandomWriteTargetCount}\n");
            log.Append($"supportedRenderTargetCount = {SystemInfo.supportedRenderTargetCount}\n");
            log.Append($"supports2DArrayTextures = {SystemInfo.supports2DArrayTextures}\n");
            log.Append($"supports32bitsIndexBuffer = {SystemInfo.supports32bitsIndexBuffer}\n");
            log.Append($"supports3DRenderTextures = {SystemInfo.supports3DRenderTextures}\n");
            log.Append($"supports3DTextures = {SystemInfo.supports3DTextures}\n");
            log.Append($"supportsAccelerometer = {SystemInfo.supportsAccelerometer}\n");
            log.Append($"supportsAsyncCompute = {SystemInfo.supportsAsyncCompute}\n");
            log.Append($"supportsAsyncGPUReadback = {SystemInfo.supportsAsyncGPUReadback}\n");
            log.Append($"supportsAudio = {SystemInfo.supportsAudio}\n");
            log.Append($"supportsCompressed3DTextures = {SystemInfo.supportsCompressed3DTextures}\n");
            log.Append($"supportsComputeShaders = {SystemInfo.supportsComputeShaders}\n");
            log.Append($"supportsConservativeRaster = {SystemInfo.supportsConservativeRaster}\n");
            log.Append($"supportsCubemapArrayTextures = {SystemInfo.supportsCubemapArrayTextures}\n");
            log.Append($"supportsGeometryShaders = {SystemInfo.supportsGeometryShaders}\n");
            log.Append($"supportsGpuRecorder = {SystemInfo.supportsGpuRecorder}\n");
            log.Append($"supportsGraphicsFence = {SystemInfo.supportsGraphicsFence}\n");
            log.Append($"supportsGyroscope = {SystemInfo.supportsGyroscope}\n");
            log.Append($"supportsHardwareQuadTopology = {SystemInfo.supportsHardwareQuadTopology}\n");
            log.Append($"supportsInstancing = {SystemInfo.supportsInstancing}\n");
            log.Append($"supportsLocationService = {SystemInfo.supportsLocationService}\n");
            log.Append($"supportsMipStreaming = {SystemInfo.supportsMipStreaming}\n");
            log.Append($"supportsMotionVectors = {SystemInfo.supportsMotionVectors}\n");
            log.Append($"supportsMultisampleAutoResolve = {SystemInfo.supportsMultisampleAutoResolve}\n");
            log.Append($"supportsMultisampled2DArrayTextures = {SystemInfo.supportsMultisampled2DArrayTextures}\n");
            log.Append($"supportsMultisampledTextures = {SystemInfo.supportsMultisampledTextures}\n");
            log.Append($"supportsMultiview = {SystemInfo.supportsMultiview}\n");
            log.Append($"supportsRawShadowDepthSampling = {SystemInfo.supportsRawShadowDepthSampling}\n");
            log.Append($"supportsRayTracing = {SystemInfo.supportsRayTracing}\n");
            log.Append($"supportsRenderTargetArrayIndexFromVertexShader = {SystemInfo.supportsRenderTargetArrayIndexFromVertexShader}\n");
            log.Append($"supportsSeparatedRenderTargetsBlend = {SystemInfo.supportsSeparatedRenderTargetsBlend}\n");
            log.Append($"supportsSetConstantBuffer = {SystemInfo.supportsSetConstantBuffer}\n");
            log.Append($"supportsShadows = {SystemInfo.supportsShadows}\n");
            log.Append($"supportsSparseTextures = {SystemInfo.supportsSparseTextures}\n");
            log.Append($"supportsStoreAndResolveAction = {SystemInfo.supportsStoreAndResolveAction}\n");
            log.Append($"supportsTessellationShaders = {SystemInfo.supportsTessellationShaders}\n");
            log.Append($"supportsTextureWrapMirrorOnce = {SystemInfo.supportsTextureWrapMirrorOnce}\n");
            log.Append($"supportsVibration = {SystemInfo.supportsVibration}\n");
            log.Append($"--------\n");
            log.Append($"systemMemorySize = {SystemInfo.systemMemorySize}\n");
            log.Append($"usesLoadStoreActions = {SystemInfo.usesLoadStoreActions}\n");
            log.Append($"usesReversedZBuffer = {SystemInfo.usesReversedZBuffer}\n");

            return log.ToString();
        }
    }
}
#endif