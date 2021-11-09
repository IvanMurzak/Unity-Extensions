using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseMonoBehaviour : SerializedMonoBehaviour
{
    [HideInInspector, System.NonSerialized] private Transform mTransform;
    public new Transform transform		=> mTransform ? mTransform : mTransform = base.transform;

	[HideInInspector, System.NonSerialized] private Animation mAnimation;
    public new Animation animation		=> mAnimation ? mAnimation : mAnimation = GetComponent<Animation>();

    [HideInInspector, System.NonSerialized] private Animator mAnimator;
    public     Animator animator		=> mAnimator ? mAnimator : mAnimator = GetComponent<Animator>();

    [HideInInspector, System.NonSerialized] private AudioSource mAudio;
    public new AudioSource audio		=> mAudio ? mAudio : mAudio = GetComponent<AudioSource>();

    [HideInInspector, System.NonSerialized] private Collider mCollider;
    public new Collider collider		=> mCollider ? mCollider : mCollider = GetComponent<Collider>();

    [HideInInspector, System.NonSerialized] private Collider2D mCollider2D;
    public new Collider2D collider2D	=> mCollider2D ? mCollider2D : mCollider2D = GetComponent<Collider2D>();

    [HideInInspector, System.NonSerialized] private Rigidbody mRigidbody;
    public new Rigidbody rigidbody		=> mRigidbody ? mRigidbody : mRigidbody = GetComponent<Rigidbody>();

    [HideInInspector, System.NonSerialized] private Rigidbody2D mRigidbody2D;
    public new Rigidbody2D rigidbody2D	=> mRigidbody2D ? mRigidbody2D : mRigidbody2D = GetComponent<Rigidbody2D>();

	// -----------------------------------------------------
	// ---------------------- UI ---------------------------
	// -----------------------------------------------------

	[HideInInspector, System.NonSerialized] private RectTransform mRectTransform;
	public RectTransform rectTransform  => mRectTransform ? mRectTransform : mRectTransform = GetComponent<RectTransform>();

	[HideInInspector, System.NonSerialized] private RectTransform mParentRectTransform;
	public RectTransform parentRectTransform => mParentRectTransform ? mParentRectTransform : mParentRectTransform = transform.parent?.GetComponent<RectTransform>();

	[HideInInspector, System.NonSerialized] private Camera mCamera;
	public Camera mainCamera		    => mCamera ? mCamera : mCamera = Camera.main;

	[HideInInspector, System.NonSerialized] private Canvas mCanvas;
	public Canvas parentCanvas		    => mCanvas ? mCanvas : mCanvas = GetComponentInParent<Canvas>();
}