using System.Collections.Generic;
using UnityEngine;

public static class MyTooltip
{
    private static Tooltip tooltipOnScreen;

    public static void CreateTooltip(Vector2 position, List<string> contentElements, string title = "")
    {
        var tooltipContainer = GameObject.FindGameObjectWithTag("TooltipContainer");

        if (tooltipContainer == null)
        {
            Debug.LogError("TooltipContainer could not been found in the scene. Assing the TooltipContainer tag the container object in the scene");
            return;
        }

        tooltipOnScreen = Object.Instantiate(Resources.Load<Tooltip>("Prefabs/TooltipPrefab"), tooltipContainer.transform);

        tooltipOnScreen.SePosition(position);
        tooltipOnScreen.SetTitle(title);
        tooltipOnScreen.SetContent(contentElements);
    }

    public static void DestroyTooltip()
    {
        if (tooltipOnScreen == null)
        {
            Debug.LogWarning("Tooltip is null");
            return;
        }

        Object.Destroy(tooltipOnScreen.gameObject);
    }
}
