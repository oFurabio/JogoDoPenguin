using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + ", " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Set4KResolution()
    {
        SetResolution(0); // Assuming 4K resolution is the first option in the dropdown
    }

    public void SetQuadHDResolution()
    {
        SetResolution(1); // Assuming QuadHD resolution is the second option in the dropdown
    }

    public void SetFullHDResolution()
    {
        SetResolution(2); // Assuming Full HD resolution is the third option in the dropdown
    }

    public void SetHDResolution()
    {
        SetResolution(3); // Assuming HD resolution is the fourth option in the dropdown
    }
}
