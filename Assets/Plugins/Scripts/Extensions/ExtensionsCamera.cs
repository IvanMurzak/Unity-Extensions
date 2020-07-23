using UnityEngine;

public static partial class ExtensionsCamera
{
    public static Vector3 GUIPositionToWorldPosition(this Camera camera, Vector2 guiPosition)
    {
        return camera.ScreenPointToRay(guiPosition).GetPoint(0);
    }
    public static Vector3 GUIDeltaToWorldDelta(this Camera camera, Vector2 guiDelta)
    {
        Vector3 screenDelta = GUIUtility.GUIToScreenPoint(guiDelta);
        Ray worldRay = camera.ScreenPointToRay(screenDelta);

        Vector3 worldDelta = worldRay.GetPoint(0);
        worldDelta -= camera.ScreenPointToRay(Vector3.zero).GetPoint(0);
        return worldDelta;
    }
    
    public static bool IsObjectVisible(this Camera camera, Renderer renderer)
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), renderer.bounds);
    }

    public static bool IsPointVisible(this Camera camera, Vector3 point)
    {
        var vpp = camera.WorldToViewportPoint(point);
        return vpp.x >= 0 && vpp.x <= 1 && vpp.y >= 0 && vpp.y <= 1;
    }

	public static Rect OrhographicVisibleRect(this Camera camera)
	{
		return new Rect(camera.transform.position - new Vector3(camera.aspect * camera.orthographicSize, camera.orthographicSize),
			new Vector2
			(
				camera.aspect * camera.orthographicSize * 2f,
				camera.orthographicSize * 2f
			));
	}
}