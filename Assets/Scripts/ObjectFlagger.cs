using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ObjectFlagger : MonoBehaviour
{
    DepthOfField dof;
    Vignette vignette;
    Bloom bloom;
    WhiteBalance whiteBalance;

    public void ChangeActiveState(GameObject objectToDisable)
    {
        objectToDisable.SetActive(!objectToDisable.activeSelf);
    }

    public void ChangeDOF(Volume volume)
    {
        if (volume.profile.TryGet(out dof))
        {
            dof.active = !dof.active;
        }
    }

    public void ChangeVignette(Volume volume)
    {
        if (volume.profile.TryGet(out vignette))
        {
            vignette.active = !vignette.active;
        }
    }

    public void ChangeBloom(Volume volume)
    {
        if (volume.profile.TryGet(out bloom))
        {
            bloom.active = !bloom.active;
        }
    }

    public void ChangeWhiteBalance(Volume volume)
    {
        if (volume.profile.TryGet(out whiteBalance))
        {
            whiteBalance.active = !whiteBalance.active;
        }
    }
}
