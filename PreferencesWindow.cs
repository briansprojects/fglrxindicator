using Gtk;
using System;
using AppIndicator;

namespace fglrxindicator
{
	public class PreferencesWindow
	{
		public PreferencesWindow ()
		{
			var win = new Window ("Fglrx Indicator");
			win.Resize (300, 200);
			win.Add (new Label("Hello Ubuntu"));
			win.ShowAll ();
			BuildIndicator ();
		}

		private void BuildIndicator()
		{
			ApplicationIndicator indicator = new ApplicationIndicator
			(
				"Fglrx Indicator", 		//id of the the indicator icon
				"catalyst",			        //file name of the icon (will look for app-icon.png) 
				Category.Hardware,
				System.IO.Path.GetDirectoryName (Environment.GetCommandLineArgs () [0])            //the folder where to look for app-icon.png
			);
			var menuItemQuit = new ImageMenuItem ("Quit");
			menuItemQuit.Image = new Gtk.Image (Stock.Quit, IconSize.Menu);
			menuItemQuit.Activated += (sender, e) => Application.Quit ();

			var popupMenu = new Menu ();
			popupMenu.Append(new ImageMenuItem ().Image = new Gtk.Image(Stock.Info, IconSize.Menu));
			popupMenu.Append (menuItemQuit);
			popupMenu.ShowAll();

			indicator.Menu = popupMenu;
			indicator.Status = AppIndicator.Status.Active;	
		}
	}
}

