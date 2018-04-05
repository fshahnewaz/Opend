using System;
using Android.Content;
using Android.Graphics;
using System.Linq;
using System.Collections.Generic;
using Android.Runtime;

namespace portal.PortalExInteractive.Droid
{
  public static class ContextExtensions
  {
    private static IntPtr _typefaceClassRef;
    private static IEnumerable<System.Reflection.FieldInfo> _typefaceFields;
    private static readonly IDictionary<String, Typeface> _cachedTypefaces = new Dictionary<String, Typeface>();

    private static void Initialize()
    {
      Type typefaceType = typeof(Typeface);
      _typefaceFields = typefaceType.
        GetFields(
          System.Reflection.BindingFlags.Static | 
          System.Reflection.BindingFlags.Public | 
          System.Reflection.BindingFlags.NonPublic);
      var classRefProperty = typefaceType.GetProperty ("class_ref", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
      System.Diagnostics.Debug.Assert (classRefProperty != null);
      if (classRefProperty != null)
      {
        _typefaceClassRef = (IntPtr)classRefProperty.GetValue (null);
      }
    }

    public static void SetDefaultFont(this Context context, String staticTypefaceFieldName, String fontAssetName)
    {
      if (_typefaceFields == null)
        Initialize ();

      Typeface typeface;
      if (!_cachedTypefaces.TryGetValue(staticTypefaceFieldName, out typeface))
      {
          typeface = Typeface.CreateFromAsset(context.Assets, fontAssetName);
          _cachedTypefaces[staticTypefaceFieldName] = typeface;
      }

      System.Diagnostics.Debug.Assert (typeface != null, "Expected to find a Typeface asset named '" + fontAssetName + "'");
      if (typeface != null)
      {
        ReplaceFont (staticTypefaceFieldName, typeface);
      }
    }

    private static void ReplaceFont(String staticTypefaceFieldName, Typeface typeface)
    {
      IntPtr fieldId = IntPtr.Zero;
      try
      {
        fieldId = JNIEnv.GetStaticFieldID (_typefaceClassRef, staticTypefaceFieldName, "Landroid/graphics/Typeface;");
      }
      catch (Java.Lang.NoSuchFieldError)
      {
      }
      System.Diagnostics.Debug.Assert(fieldId != IntPtr.Zero, "Expected to have found a Typeface field named " + staticTypefaceFieldName);
       
      if (_typefaceClassRef != IntPtr.Zero && fieldId != IntPtr.Zero)
      {
        IntPtr currentValue = JNIEnv.GetStaticObjectField (_typefaceClassRef, fieldId);
        IntPtr intPtr = JNIEnv.ToLocalJniHandle (typeface);
        JNIEnv.SetStaticField (_typefaceClassRef, fieldId, intPtr);
      }
      else
      {
        System.Diagnostics.Debug.Assert(false, "Expected to have found a Typeface class reference and a field Id.");
      }
    }
  }
}