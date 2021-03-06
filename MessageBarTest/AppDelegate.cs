using System;
using Foundation;
using UIKit;
using MonoTouch.Dialog;
using MessageBar;

namespace MessageBarTest
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UINavigationController navigation;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			var menu = new RootElement ("Message Test") {
				new Section { 
					new StringElement ("Show Info", () =>
						MessageBarManager.SharedInstance.ShowMessage
						("Info", "This is information", MessageType.Info, 
							delegate {
								Console.WriteLine ("This is callback!");
							})),

					new StringElement ("Show Error", () =>
						MessageBarManager.SharedInstance.ShowMessage
							("Error", "This is error", MessageType.Error)),

					new StringElement ("Show Success", () =>
						MessageBarManager.SharedInstance.ShowMessage
						("Success", "This is success", MessageType.Success))
				},
				new Section {
					new StringElement ("Hide all", MessageBarManager.SharedInstance.HideAll),
					new StringElement ("Switch to at the bottom or at the top", () => {
						MessageBarManager.SharedInstance.ShowAtTheBottom = MessageBarManager.SharedInstance.ShowAtTheBottom == false;
					})
				}
			};

			var dvc = new DialogViewController (menu);
			navigation = new UINavigationController ();
			navigation.PushViewController (dvc, false);

			// If you have defined a root view controller, set it here:
			window.RootViewController = navigation;

			// make the window visible
			window.MakeKeyAndVisible ();

			return true;
		}
	}
}

