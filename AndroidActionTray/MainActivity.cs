using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

using ActionComponents;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;

namespace ActionTrayTest.Android
{
	[Activity (Label = "ActionTrayTest.Android", MainLauncher = true)]
	public class Activity1 : Activity
	{
		#region Private Variables
		private ACTray leftTray, rightTray, toolsTray, propertyTray, paletteTray, documentTray;
		#endregion 

		#region Public Variables
		public ACTrayManager trayManager;
		#endregion

		#region Override Methods
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Gain Access to all views and controls in our layout
			leftTray = FindViewById<ACTray> (Resource.Id.trayLeft);
			rightTray = FindViewById<ACTray> (Resource.Id.trayRight);
			toolsTray = FindViewById<ACTray> (Resource.Id.trayTools);
			propertyTray = FindViewById<ACTray> (Resource.Id.trayProperty);
			paletteTray = FindViewById<ACTray> (Resource.Id.trayPalette);
			documentTray = FindViewById<ACTray> (Resource.Id.trayDocuments);

			// Create a TrayManager to handle a collection of "palette"
			// trays. It will automatically close any open tray when 
			// another tray in this collection is opened.
			trayManager = new ACTrayManager ();

			// Automatically close the left and right trays when any tray
			// in the manager's collection is opened
			trayManager.TrayOpened += (tray) => {
				// Animate the trays being closed
				leftTray.CloseTray (true);
				rightTray.CloseTray (true);
			};

			// Setup the left side tray
			leftTray.trayType = ACTrayType.Draggable;
			leftTray.orientation = ACTrayOrientation.Left;
			leftTray.tabLocation = ACTrayTabLocation.BottomOrRight;
			leftTray.frameType = ACTrayFrameType.EdgeOnly;
			leftTray.tabType = ACTrayTabType.IconAndTitle;
			leftTray.bringToFrontOnTouch=true;

			// Style tray
			leftTray.appearance.background = Color.Gray;
			leftTray.appearance.border = Color.Red;
			leftTray.icon = Resource.Drawable.icon_calendar;
			leftTray.title = "Events";
			leftTray.appearance.tabAlpha=100;
			leftTray.CloseTray (false);

			// Respond to the left tray being touched
			leftTray.Touched+= (tray) => {
				//Yes, close this tray and aminate the closing
				rightTray.CloseTray (true);
				
				// Tell any open palette trays to close
				trayManager.CloseAllTrays ();
				
				// Close document tray
				documentTray.CloseTray (true);
			};

			// Setup the right side tray
			rightTray.trayType = ACTrayType.Popup;
			rightTray.orientation = ACTrayOrientation.Right;
			rightTray.bringToFrontOnTouch = true;
			rightTray.CloseTray (false);

			// Respond to the tray being opened
			rightTray.Opened+= (tray) => {
				//Close this tray and aminate the closing
				leftTray.CloseTray (true);
				
				// Tell any open palette trays to close
				trayManager.CloseAllTrays ();
				
				// Close document tray
				documentTray.CloseTray (true);
			};

			// Set tray type
			documentTray.trayType = ACTrayType.AutoClosingPopup;
			documentTray.orientation = ACTrayOrientation.Bottom;
			documentTray.tabType = ACTrayTabType.GripAndTitle;
			documentTray.bringToFrontOnTouch=true;
			
			// Style tray
			documentTray.tabWidth = 125;
			documentTray.appearance.background = Color.Gray;
			documentTray.title = "Documents";
			documentTray.CloseTray (false);
			
			// Respond to the tray being opened
			documentTray.Opened += (tray) => {
				// Close left and right trays
				leftTray.CloseTray(true);
				rightTray.CloseTray(true);
			};

			//--------------------------------------------------------------------------------------
			// Create three action tray's and use them as a collection via an ActionTrayManager
			//--------------------------------------------------------------------------------------

			//--------------------------------------------------------------------------------------
			// Palette 1
			// Set tray type
			paletteTray.trayType = ACTrayType.AutoClosingPopup;
			paletteTray.orientation = ACTrayOrientation.Top;
			paletteTray.tabLocation = ACTrayTabLocation.TopOrLeft;
			paletteTray.tabType = ACTrayTabType.IconAndTitle;
			paletteTray.CloseTray (false);
			
			// Style tray
			paletteTray.tabWidth = 125;
			paletteTray.appearance.background = Color.Gray;
			paletteTray.icon = Resource.Drawable.icon_palette;
			paletteTray.title="Palette";
			
			// Add this tray to the manager's collection
			trayManager.AddTray (paletteTray);

			//--------------------------------------------------------------------------------------
			// Palette 2
			// Setup property tray type
			propertyTray.trayType = ACTrayType.Popup;
			propertyTray.orientation = ACTrayOrientation.Top;
			propertyTray.tabLocation = ACTrayTabLocation.TopOrLeft;
			propertyTray.tabType = ACTrayTabType.IconAndTitle;
			propertyTray.CloseTray (false);
			
			// Style tray
			propertyTray.tabWidth = 125;
			propertyTray.appearance.background = Color.Rgb (38,38,38);
			propertyTray.icon=Resource.Drawable.icon_measures;
			propertyTray.title="Properties";
			
			// Add this tray to the manager's collection
			trayManager.AddTray (propertyTray);

			//--------------------------------------------------------------------------------------
			// Palette 3
			// Setup tools tray type
			toolsTray.trayType = ACTrayType.AutoClosingPopup;
			toolsTray.orientation = ACTrayOrientation.Top;
			toolsTray.tabType = ACTrayTabType.IconOnly;
			toolsTray.CloseTray (false);
			
			// Style tools tray
			toolsTray.tabWidth = 50;
			toolsTray.tabLocation = ACTrayTabLocation.BottomOrRight;
			toolsTray.appearance.background = Color.Rgb (38,38,38);
			toolsTray.tabType = ACTrayTabType.CustomDrawn;
			toolsTray.icon = Resource.Drawable.icon_pencil;

			// Custom draw this tab
			toolsTray.CustomDrawDragTab += (tray, canvas, rect) => {
				//Draw background
				var body= new ShapeDrawable(new RectShape());
				body.Paint.Color=tray.appearance.background;
				body.SetBounds (rect.Left, rect.Top, rect.Right, rect.Bottom);
				body.Draw (canvas);

				//Define icon paint
				var iPaint=new Paint();
				iPaint.Alpha=tray.appearance.tabAlpha;

				//Load bitmap
				var bitmap=BitmapFactory.DecodeResource(Resources,tray.icon);
				
				//Draw image
				canvas.DrawBitmap (bitmap, rect.Left+1, rect.Top+5, iPaint);
			};
			
			// Add this tray to the manager's collection
			trayManager.AddTray (toolsTray);
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			//Save the state of all trays on the screen
			outState.PutString("leftTray",leftTray.SaveState);
			outState.PutString("rightTray",rightTray.SaveState);
			outState.PutString("documentTray",documentTray.SaveState);
			outState.PutString("paletteTray",paletteTray.SaveState);
			outState.PutString("propertyTray",propertyTray.SaveState);
			outState.PutString("toolsTray",toolsTray.SaveState);
			
			base.OnSaveInstanceState (outState);
		}

		protected override void OnRestoreInstanceState (Bundle savedInstanceState)
		{
			//Restore all trays to their previous states
			leftTray.RestoreState(savedInstanceState.GetString("leftTray"));
			rightTray.RestoreState(savedInstanceState.GetString("rightTray"));
			documentTray.RestoreState(savedInstanceState.GetString("documentTray"));
			paletteTray.RestoreState(savedInstanceState.GetString("paletteTray"));
			propertyTray.RestoreState(savedInstanceState.GetString("propertyTray"));
			toolsTray.RestoreState(savedInstanceState.GetString("toolsTray"));

			base.OnRestoreInstanceState (savedInstanceState);
		}
		#endregion 
	}
}


