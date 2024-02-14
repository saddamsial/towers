using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public abstract class VirtualCameraReactToBlendInOut : MonoBehaviour
{
    // ... various bits from @gregoryl's example, that then invoke these 3 callbacks:
    public abstract void _OnVirtualCameraAnimateIn_Started(CinemachineVirtualCameraBase vcam);
    public abstract void _OnVirtualCameraAnimateIn_Finished(CinemachineVirtualCameraBase vcam);
    public abstract void _OnVirtualCameraAnimateOut_Started(CinemachineVirtualCameraBase vcam);
}

public class SetVirtualCamerasMask : VirtualCameraReactToBlendInOut
{
    public LayerMask cullingMaskWhileLive;
    private LayerMask _savedLayerMask;

    public override void _OnVirtualCameraAnimateIn_Started(CinemachineVirtualCameraBase vcam)
    {
        _savedLayerMask = Camera.main.cullingMask;
        Camera.main.cullingMask = cullingMaskWhileLive;
    }

    public override void _OnVirtualCameraAnimateIn_Finished(CinemachineVirtualCameraBase vcam)
    {

    }

    public override void _OnVirtualCameraAnimateOut_Started(CinemachineVirtualCameraBase vcam)
    {
        Camera.main.cullingMask = _savedLayerMask;
    }
}
