using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class HexCell : BaseMonoBehaviour
{
#if UNITY_EDITOR
    private const float X_RADIUS = 1f;
    private static LinkedList<HexCell> all = new LinkedList<HexCell>();

    [OdinSerialize, HideInInspector] bool autoGravity = false;

    [BoxGroup("Data")] public float padding = 0;
    [BoxGroup("Data"), HorizontalGroup("Data/2")] public float offset = 0;
    [BoxGroup("Data"), HorizontalGroup("Data/2")] public bool inside = true;


    Subject<Unit> onLateUpdate = new Subject<Unit>();
    Subject<Pair<Vector3>> onMoved = new Subject<Pair<Vector3>>();

    float outerRadius;
    float innerRadius;

    Vector3 settedPosition = Vector3.one * float.MaxValue;

    CompositeDisposable disposables = new CompositeDisposable();

    private void Start()
    {
        UpdateSizeFromMesh();
    }

    [HorizontalGroup("Buttons"), HideIf("autoGravity")]
    [Button("Disabled Auto\n(press to enable)", ButtonSizes.Large), GUIColor(1f, .5f, .5f)]
    private void StartListeningEvents()
    {
        disposables.Clear();
        autoGravity = true;
        onLateUpdate.Select(x => transform.localPosition).Pairwise()
            .Where(x => x.Previous != x.Current && x.Current != settedPosition)
            .Subscribe(onMoved.OnNext).AddTo(disposables);

        onMoved.Where(x => autoGravity).Subscribe(x => Gravity()).AddTo(disposables);

        var onPaddingChanged = onLateUpdate.Select(x => padding).Pairwise()
            .Where(x => x.Previous != x.Current).Select(x => Unit.Default);
        var onOffsetChanged = onLateUpdate.Select(x => offset).Pairwise()
            .Where(x => x.Previous != x.Current).Select(x => Unit.Default);
        var onInsideChanged = onLateUpdate.Select(x => inside).Pairwise()
            .Where(x => x.Previous != x.Current).Select(x => Unit.Default);

        Observable.Merge(onPaddingChanged, onOffsetChanged, onInsideChanged)
            .Where(x => autoGravity)
            .Subscribe(x => Gravity()).AddTo(disposables);
    }

    [HorizontalGroup("Buttons"), ShowIf("autoGravity")]
    [Button("Enabled Auto\n(press to disable)", ButtonSizes.Large), GUIColor(.5f, 1, .5f)]
    private void StopListeningEvents()
    {
        disposables.Clear();
        autoGravity = false;
    }

    private void LateUpdate()
    {
        onLateUpdate.OnNext(Unit.Default);
    }

    private void OnEnable()
    {
        if (!all.Contains(this)) all.AddLast(this);
        if (autoGravity) StartListeningEvents();
    }

    private void OnDisable()
    {
        all.Remove(this);
        StopListeningEvents();
    }

	private void OnDrawGizmosSelected()
	{
		var floorPoint = transform.localPosition.SetZ(0);
		if (transform.parent != null) floorPoint = transform.parent.TransformPoint(floorPoint);

		var hex = GetNearestHex();
		if (hex != null)
		{
			var nearestPos = GetNearestPosition(hex).SetZ(0);
			if (transform.parent != null) nearestPos = transform.parent.TransformPoint(nearestPos);

			Gizmos.color = Color.white;
			Gizmos.DrawLine(floorPoint, nearestPos);

			Gizmos.color = Color.red;
			foreach (var x in GetAnchors(hex))
			{
				if (transform.parent != null)
					GizmosHelper.DrawX(transform.parent.TransformPoint(x), X_RADIUS);
				else GizmosHelper.DrawX(x, X_RADIUS);
			}
		}

		Gizmos.color = Color.gray;
		Gizmos.DrawLine(transform.position, floorPoint);
	}

	[HorizontalGroup("Buttons")]
    [Button(ButtonSizes.Large)]
    private void Gravity()
    {
        var nearest = GetNearestHex();
        if (nearest != null)
        {
            transform.localPosition = settedPosition = GetNearestPosition(nearest);
        }
    }

    private Vector3[] GetAnchors(HexCell hex)
    {
        var p1 = transform.localPosition.SetZ(0);
        var p2 = hex.transform.localPosition.SetZ(0);

        var length = padding + hex.padding
            + innerRadius / 2 + hex.innerRadius / 2;

        var offsetLength = offset * hex.outerRadius / 2
                            + offset * outerRadius / 2;
        if (inside) offsetLength -= offset * outerRadius;
        offsetLength += padding * offset;

        var angle = 0;
        var fixedAngle = 60;
        var count = 360 / fixedAngle;
        var anchors = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            var dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
            var offsetDir = dir.Rotate(90 * Mathf.Deg2Rad);

            anchors[i] = p2.Add(dir * length).Add(offsetDir * offsetLength).SetZ(transform.localPosition.z);
            angle += fixedAngle;
        }
        return anchors;
    }

    private Vector3 GetNearestPosition(HexCell hex)
    {
        var p1 = transform.localPosition.SetZ(0);

        var anchors = GetAnchors(hex);

        var minP = anchors[0];
        var minLength = float.MaxValue;

        foreach(var p in anchors)
        {
            var len = Vector3.Distance(p1, p);
            if (len < minLength)
            {
                minLength = len;
                minP = p;
            }
        }

        return minP;
    }
    
    private HexCell GetNearestHex()
    {
        float minLength = float.MaxValue;
        HexCell nearest = null;
        foreach (var hex in all)
        {
            if (hex != this)
            {
                var length = Vector3.Distance(hex.transform.localPosition.SetZ(0), transform.localPosition.SetZ(0));
                if (length < minLength)
                {
                    minLength = length;
                    nearest = hex;
                }
            }
        }
        return nearest;
    }
    
    [BoxGroup("Utils"), HorizontalGroup("Utils/buttons"), Button]
    private void UpdateSizeFromMesh()
    {
        var bounds = GetComponentInChildren<MeshFilter>().sharedMesh.bounds;
        // version for wrong rotated Hexagons
        // outerRadius = Mathf.Max(bounds.size.x * transform.localScale.x, bounds.size.z * transform.localScale.z);
        // innerRadius = Mathf.Min(bounds.size.x * transform.localScale.x, bounds.size.z * transform.localScale.z); // * 0.866025404f

        // version for right rotated Hexagons
        outerRadius = Mathf.Max(bounds.size.x * transform.localScale.x, bounds.size.y * transform.localScale.y);
        innerRadius = Mathf.Min(bounds.size.x * transform.localScale.x, bounds.size.y * transform.localScale.y); // * 0.866025404f

        // if (autoGravity) Gravity();
    }

    [BoxGroup("Utils"), HorizontalGroup("Utils/buttons"), Button, GUIColor(1, .5f, .5f)]
    private void RebuildAll()
    {
        print("all count = " + all.Count + " (old)");
        all.Clear();

        var roots = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var root in roots)
        {
            var cells = root.GetComponentsInChildren<HexCell>();
            foreach (var cell in cells)
            {
                if (cell.isActiveAndEnabled)
                {
                    cell.UpdateSizeFromMesh();
                    all.AddLast(cell);
                }
            }
        }
        print("all count = " + all.Count + " (new)");
    }

    public static Vector2[] Edges(MeshFilter meshFilter)
    {
        var transform = meshFilter.transform;
        var bounds = meshFilter.sharedMesh.bounds;
        var outerRadius = Mathf.Max(bounds.size.x * transform.lossyScale.x, bounds.size.z * transform.lossyScale.z);
        var innerRadius = Mathf.Min(bounds.size.x * transform.lossyScale.x, bounds.size.z * transform.lossyScale.z); // * 0.866025404f

        var angle = 30;
        var fixedAngle = 60;
        var count = 360 / fixedAngle;
        var dirs = new Vector2[count];
        for (int i = 0; i < count; i++)
        {
            var dir = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
            var pos = dir * (outerRadius / 2);

            dirs[i] = transform.parent.localPosition + (transform.parent.localRotation * pos);
            angle += fixedAngle;
        }
        return dirs;
    }
#endif
}
