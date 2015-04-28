using Gtk;
using System;
using AppIndicator;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace fglrxindicator
{
	public class PreferencesWindow
	{
		MenuItem _sensorOutput;
		Label _label;

		public PreferencesWindow ()
		{
			BuildIndicator ();
			Thread thread1 = new System.Threading.Thread(new ThreadStart(WriteSensor));
			thread1.Start();
		}

		private void WriteSensor() //Since there arent mono bindings for lm-sensors, we must use the shell.. 
		{
			Process proc = new Process 
			{
				StartInfo = new ProcessStartInfo 
				{
					FileName = "/usr/bin/amdconfig",
					Arguments = @"--od-gettemperature",
					UseShellExecute = false,
					RedirectStandardOutput = true
				}
			};
			proc.Start();
			_label.Text = proc.StandardOutput.ReadToEnd();
			Thread.Sleep (2000);
			proc.Close();
			WriteSensor ();
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
			_sensorOutput = new MenuItem ();
			_label = new Label ();
			var popupMenu = new Menu ();
			var menuItemQuit = new MenuItem ("Quit");
			menuItemQuit.Activated += (sender, e) => Application.Quit ();

			popupMenu.Append (_sensorOutput);
			popupMenu.Append (menuItemQuit);
			popupMenu.ShowAll();

			indicator.Menu = popupMenu;
			indicator.Status = AppIndicator.Status.Active;
			_sensorOutput.Add(_label);
		}
	}
}

