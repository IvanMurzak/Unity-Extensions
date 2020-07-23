using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Firebase.Crashlytics;
using UnityEngine;
using Debug	= UnityEngine.Debug;
using Object = UnityEngine.Object;

public static class DebugFormat
{
	static readonly bool	LOG_CRASHLYTICS		= false;

	static readonly int		mainThreadId		= Thread.CurrentThread.ManagedThreadId;
	static			bool	IsMainThread		=> Thread.CurrentThread.ManagedThreadId == mainThreadId;

#if UNITY_EDITOR
	private static	string	Format<T>			(string message = "", int deep = 3)	=> $"<b><color={Colors.Log}>[{TypeName(typeof(T))}]</color></b> - <b>{MethodName(deep)}</b>: {message}";
	private static	string	FormatWarning<T>	(string message = "", int deep = 3)	=> $"<b><color={Colors.Warning}>[{TypeName(typeof(T))}]</color></b> - <b>{MethodName(deep)}</b>: {message}";
	private static	string	FormatError<T>		(string message = "", int deep = 3)	=> $"<b><color={Colors.Error}>[{TypeName(typeof(T))}]</color></b> - <b>{MethodName(deep)}</b>: {message}";
#else
	private static	string	Format<T>			(string message = "", int deep = 3)	=> $"[{TypeName(typeof(T))}] - {MethodName(deep)}: {message}";
	private static	string	FormatWarning<T>	(string message = "", int deep = 3)	=> $"[{TypeName(typeof(T))}] - {MethodName(deep)}: {message}";
	private static	string	FormatError<T>		(string message = "", int deep = 3)	=> $"[{TypeName(typeof(T))}] - {MethodName(deep)}: {message}";
#endif


#if UNITY_EDITOR
	private static	string	MethodName			(int depth = 2)			=> new StackTrace()?.GetFrame(depth).GetMethod().Name ?? "";
#else
	private static	string	MethodName			(int depth = 2)			=> "***";
#endif

	private static	string	TypeName			(Type t)				=> t.IsGenericType ? $"{t.Name}<{String.Join(",", t.GetGenericArguments().Select(TypeName).ToArray())}>" : t.Name;

	private static  Object	Target<T>			(T instance, Object target = null) 
		=> target == null ? (instance is Object ? instance as Object : null) : (target);

	public static	string	Log<T>			()																			{ var str = Format<T>();						Debug.Log(str);															try { if (LOG_CRASHLYTICS) Crashlytics.Log(str); } catch (Exception e) { Debug.LogException(e); } return str; }
	public static	string	Log<T>			(				string message,		 Object target = null, int deep = 3)	{ var str = Format<T>(message, deep);			Debug.Log(str, target);													try { if (LOG_CRASHLYTICS) Crashlytics.Log(str); } catch (Exception e) { Debug.LogException(e); } return str; }
	public static	string	Log<T>			(T instance,	string message = "", Object target = null, int deep = 3)	{ var str = Format<T>(message, deep);			Debug.Log(str, Target(instance, target));								try { if (LOG_CRASHLYTICS) Crashlytics.Log(str); } catch (Exception e) { Debug.LogException(e); } return str; }

	// --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	public static	string	LogWarning<T>	()																			{ var str = FormatWarning<T>();					Debug.LogWarning(str);													try { if (LOG_CRASHLYTICS) Crashlytics.Log(str); } catch (Exception e) { Debug.LogException(e); } return str; }
	public static	string	LogWarning<T>	(				string message,		 Object target = null, int deep = 3)	{ var str = FormatWarning<T>(message, deep);	Debug.LogWarning(str, target);											try { if (LOG_CRASHLYTICS) Crashlytics.Log(str); } catch (Exception e) { Debug.LogException(e); } return str; }
	public static	string	LogWarning<T>	(T instance,	string message = "", Object target = null, int deep = 3)	{ var str = FormatWarning<T>(message, deep);	Debug.LogWarning(FormatWarning<T>(message), Target(instance, target));	try { if (LOG_CRASHLYTICS) Crashlytics.Log(str); } catch (Exception e) { Debug.LogException(e); } return str; }

	// --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	public static	string	LogError<T>		()																			{ var str = FormatError<T>();					Debug.LogError(str);													try { if (LOG_CRASHLYTICS) Crashlytics.Log(str); } catch (Exception e) { Debug.LogException(e); } return str; }
	public static	string	LogError<T>		(				string message,		 Object target = null, int deep = 3)	{ var str = FormatError<T>(message, deep);		Debug.LogError(str, target);											try { if (LOG_CRASHLYTICS) Crashlytics.Log(str); } catch (Exception e) { Debug.LogException(e); } return str; }
	public static	string	LogError<T>		(T instance,	string message = "", Object target = null, int deep = 3)	{ var str = FormatError<T>(message, deep);		Debug.LogError(str, Target(instance, target));							try { if (LOG_CRASHLYTICS) Crashlytics.Log(str); } catch (Exception e) { Debug.LogException(e); } return str; }

	// --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	public static	void	LogException<T>	(				Exception ex)												{												Debug.LogException(ex);													try { if (LOG_CRASHLYTICS) Crashlytics.LogException(ex); } catch (Exception e) { Debug.LogException(e); }  }
	public static	void	LogException<T>	(				Exception ex, Object target = null)							{												Debug.LogException(ex, target);											try { if (LOG_CRASHLYTICS) Crashlytics.LogException(ex); } catch (Exception e) { Debug.LogException(e); }  }
	public static	void	LogException<T>	(T instance,	Exception ex, Object target = null)							{												Debug.LogException(ex, Target(instance, target));						try { if (LOG_CRASHLYTICS) Crashlytics.LogException(ex); } catch (Exception e) { Debug.LogException(e); }  }

	// --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	public static	void	PrintStackTrace<T>	(T instance, Object target = null)
	{
		var stackTrace = new StackTrace().GetFrames().Skip(1)
			.Select(x => $"Filename: {x.GetFileName()} Method: {x.GetMethod()} Line: {x.GetFileLineNumber()} Column: {x.GetFileColumnNumber()}");

		Debug.Log($"[{TypeName(typeof(T))}] - {MethodName(2)}: StackTrace:\n{string.Join("\n", stackTrace)}", Target(instance, target));
	}

	public static	void	PrintObjectStructure (object obj)
	{
		try					{ Debug.Log(PrintProperties(obj, 0)); }
		catch (Exception e) { Debug.LogException(e); }
	}
	private static	string	PrintProperties(object obj, int indent)
	{
		if (obj == null)	return "null";
		var indentFrame		= new string(' ', indent * 2);
		indent++;
		var indentString	= new string(' ', indent * 2);
		var str				= indentFrame + "{\n";
		var objType			= obj.GetType();
		var fields			= objType.GetFields();

		foreach (var field in fields)
		{
			var fieldValue = field.GetValue(obj);
			
			if (field.FieldType.IsArray)
			{
				Array array = fieldValue as Array;
				str += $"{indentString}{field.Name} [{array.Length}]:\n{indentString}" + "{\n";
				foreach (var element in array)
				{
					str += PrintProperties(element, indent + 1);
				}
				str += $"{indentString}" + "}\n";
			}
			else if (field.FieldType.Assembly == objType.Assembly && !field.FieldType.IsEnum)
			{
				str += $"{indentString}{field.Name}:";
				str += PrintProperties(fieldValue, indent + 2);
			}
			else
			{
				str += $"{indentString}{field.Name}: {fieldValue}\n";
			}
		}
		return str + indentFrame + "}\n";
	}

	private static class Colors
	{
		public static string Log		=> IsMainThread ? (Application.isPlaying ? "#2222FF" : "#1E7CAA") : "#3F6B7F";
		public static string Warning	=> IsMainThread ? (Application.isPlaying ? "#FF6A00" : "#FF8449") : "#A0684E";
		public static string Error		=> IsMainThread ? (Application.isPlaying ? "#FF0000" : "#FF3278") : "#891F33";
	}
}