using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace UniRx.Triggers
{
    public static partial class ObservableTriggerExtensions
    {
        #region ObservableMouseTrigger

        public static IObservable<Vector2> OnMouseDownGlobalAsObservable(this Component component, int button)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Vector2>();
            return GetOrAddComponent<MouseTriggers>(EventSystem.current.gameObject).OnMouseDownGlobalAsObservable(button);
        }

        public static IObservable<Vector3> OnMouseDragGlobalAsObservable(this Component component)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Vector3>();
            return GetOrAddComponent<MouseTriggers>(EventSystem.current.gameObject).OnMouseDragGlobalAsObservable();
        }
        
        public static IObservable<Vector2> OnMouseUpGlobalAsObservable(this Component component, int button)
        {
            if (component == null || component.gameObject == null) return Observable.Empty<Vector2>();
            return GetOrAddComponent<MouseTriggers>(EventSystem.current.gameObject).OnMouseUpGlobalAsObservable(button);
        }

		#endregion

		#region ObservableTouchTrigger

		public static IObservable<Touch> OnTouchDownGlobalAsObservable(this Component component)
		{
			if (component == null || component.gameObject == null) return Observable.Empty<Touch>();
			return GetOrAddComponent<TouchTriggers>(EventSystem.current.gameObject).OnTouchDownGlobalAsObservable();
		}

		public static IObservable<Touch> OnTouchUpGlobalAsObservable(this Component component)
		{
			if (component == null || component.gameObject == null) return Observable.Empty<Touch>();
			return GetOrAddComponent<TouchTriggers>(EventSystem.current.gameObject).OnTouchUpGlobalAsObservable();
		}

		#endregion


		static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
    }
}