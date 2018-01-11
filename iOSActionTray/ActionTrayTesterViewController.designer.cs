// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;

namespace ActionTrayTester
{
	[Register ("ActionTrayTesterViewController")]
	partial class ActionTrayTesterViewController
	{
		[Outlet]
		ActionComponents.ACTray leftTray { get; set; }

		[Outlet]
		ActionComponents.ACTray rightTray { get; set; }

		[Outlet]
		ActionComponents.ACTray paletteTray { get; set; }

		[Outlet]
		ActionComponents.ACTray propertyTray { get; set; }

		[Outlet]
		ActionComponents.ACTray toolsTray { get; set; }

		[Outlet]
		ActionComponents.ACTray documentTray { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (leftTray != null) {
				leftTray.Dispose ();
				leftTray = null;
			}

			if (rightTray != null) {
				rightTray.Dispose ();
				rightTray = null;
			}

			if (paletteTray != null) {
				paletteTray.Dispose ();
				paletteTray = null;
			}

			if (propertyTray != null) {
				propertyTray.Dispose ();
				propertyTray = null;
			}

			if (toolsTray != null) {
				toolsTray.Dispose ();
				toolsTray = null;
			}

			if (documentTray != null) {
				documentTray.Dispose ();
				documentTray = null;
			}
		}
	}
}
