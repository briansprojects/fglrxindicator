using Gtk;
using System;
using AppIndicator;
using System.Threading;
using System.Diagnostics;

namespace fglrxindicator
{
	public class PreferencesWindow
	{
		MenuItem _sensorOutput;

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
					FileName = "/usr/bin/sensors",
					UseShellExecute = false,
					RedirectStandardOutput = true
				}
			};

			proc.Start();
			while (true) //TODO: This is a piss-poor way of doing an auto-referesh but hey, it works. Barely.
			{
				var label = new Label(proc.StandardOutput.ReadToEnd());
				proc.Close();

				_sensorOutput.Add(label);
				Thread.Sleep (1000);
				_sensorOutput.Remove (label);
				WriteSensor ();
			}
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
			var popupMenu = new Menu ();
			var menuItemQuit = new MenuItem ("Quit");
			menuItemQuit.Activated += (sender, e) => Application.Quit ();

			popupMenu.Append (_sensorOutput);
			popupMenu.Append (menuItemQuit);
			popupMenu.ShowAll();

			indicator.Menu = popupMenu;
			indicator.Status = AppIndicator.Status.Active;
		}
	}
}

