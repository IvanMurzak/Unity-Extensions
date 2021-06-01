/// @creator: Slipp Douglas Thompson
/// @license: WTFPL
/// @purpose: A UnityEngine.UI.Graphic subclass that provides only raycast targeting, skipping all drawing.
/// @why: Because this functionality should be built-into Unity.
/// @usage: Add a `NonDrawingGraphic` component to the GameObject you want clickable, but without its own image/graphics.
/// @intended project path: Assets/Plugins/UnityEngine UI Extensions/NonDrawingGraphic.cs
/// @interwebsouce: https://gist.github.com/capnslipp/349c18283f2fea316369

using UnityEngine;
using UnityEngine.UI;



/// A concrete subclass of the Unity UI `Graphic` class that just skips drawing.
/// Useful for providing a raycast target without actually drawing anything.
public class NonDrawingGraphic : Graphic
{
	public override void SetMaterialDirty() { return; }
	public override void SetVerticesDirty() { return; }

	/// Probably not necessary since the chain of calls `Rebuild()`->`UpdateGeometry()`->`DoMeshGeneration()`->`OnPopulateMesh()` won't happen; so here really just as a fail-safe.
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
		return;
	}
}