using System;

[assembly: Xamarin.Forms.Dependency(typeof(portal.PortalExInteractive.Droid.DroidBaseUrl))]
namespace portal.PortalExInteractive.Droid
{
  public class DroidBaseUrl : IBaseUrl
  {
    public DroidBaseUrl()
    {
    }

    #region IBaseUrl implementation

    public string GetBaseUrlForAssets()
    {
      return "file:///android_asset/";
    }

    #endregion
  }
}

