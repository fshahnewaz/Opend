using Plugin.Settings;
using Plugin.Settings.Abstractions;


namespace portal.PortalExInteractive.Droid
{
	/// <summary>
	/// This is the SettingsViewModel static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public class SettingsService : BaseSettingsService
	{
		protected override ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}
	}
}