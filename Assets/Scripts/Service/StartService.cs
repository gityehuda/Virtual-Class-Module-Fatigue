using UnityEngine;
using Binus.WebGL.Service;

public class StartService
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnRuntimeMethodLoad()
    {
        GameObject goDownloadFileCSVWebGL = new GameObject("Download File CSV WebGL");
        var serviceDownloadFileCSVWebGL = goDownloadFileCSVWebGL.AddComponent<DownloadFileCSVWebGLService>();

        GameObject goMobileService = new GameObject("Mobile Service");
        var serviceMobileService = goMobileService.AddComponent<MobileService>();

        InteropWebGLService interopWebGLService = new InteropWebGLService();

        ServiceLocator.RegisterService<IDownloadFileCSVWebGLService>(serviceDownloadFileCSVWebGL);
        ServiceLocator.RegisterService<IMobileService>(serviceMobileService);
        ServiceLocator.RegisterService<IInteropWebGLService>(interopWebGLService);
    }
}
