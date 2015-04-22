using Gtk;
using System;
using AppIndicator;
using System.Diagnostics;

namespace fglrxindicator
{
	public class PreferencesWindow
	{
		string _line;

		public PreferencesWindow ()
		{
			var win = new Window ("Fglrx Indicator");
			win.Resize (300, 200);
			win.Add (new Label("Hello Ubuntu"));
			win.ShowAll ();
			BuildIndicator ();
		}

		private string ReadSensor()
		{
			Process proc = new Process 
			{
				StartInfo = new ProcessStartInfo 
				{
					FileName = "/usr/bin/sensors",
					UseShellExecute = false,
					RedirectStandardOutput = true
				}
			};

			proc.Start();
			while (!proc.StandardOutput.EndOfStream) 
			{
				_line = proc.StandardOutput.ReadToEnd();
			}
			proc.Close (); //Probably won't need this but doesn't hurt to have it
			return _line;
		}

		private void BuildIndicator()
		{
			ApplicationIndicator indicator = new ApplicationIndicator
			(
				"Fglrx Indicator", //ID
				"catalyst", //File Name
				Category.Hardware,
				System.IO.Path.GetDirectoryName (Environment.GetCommandLineArgs () [0]) //TODO: Factor this so that it isn't so ugly
			);
			//TODO: Figure out what is needed and what is not
			var menuItemQuit = new ImageMenuItem ("Quit");
			menuItemQuit.Image = new Gtk.Image (Stock.Quit, IconSize.Menu);
			menuItemQuit.Activated += (sender, e) => Application.Quit ();

			var popupMenu = new Menu ();
			popupMenu.Append(new ImageMenuItem ().Image = new Gtk.Image(Stock.Info, IconSize.Menu));
			popupMenu.Append (menuItemQuit);
			popupMenu.ShowAll();

			indicator.Menu = popupMenu;
			indicator.Status = AppIndicator.Status.Active;
			ReadSensor (); //TODO: Put the output of this into the actual indicator
		}
	}
}

