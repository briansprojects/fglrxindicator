using Gtk;
using System;
using AppIndicator;
using System.Threading;
using System.Diagnostics;

namespace fglrxindicator
{
	public class FglrxIndicator
	{
		private MenuItem _sensorOutput;
		private Label _label;

		private void WriteSensor() //Since there arent mono bindings for lm-sensors, we must use the shell
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
			WriteSensor();
		}

		public FglrxIndicator ()
		{
			ApplicationIndicator indicator = new ApplicationIndicator
				(
					"Fglrx Indicator", //ID
					"catalyst", //File Name
					Category.Hardware,
					System.IO.Path.GetDirectoryName (Environment.GetCommandLineArgs () [0])
				);

			_sensorOutput = new MenuItem ();
			_label = new Label ();
			var popupMenu = new Menu ();

			popupMenu.Append (_sensorOutput);
			popupMenu.ShowAll();

			indicator.Menu = popupMenu;
			indicator.Status = AppIndicator.Status.Active;
			_sensorOutput.Add(_label);

			Thread thread1 = new System.Threading.Thread(new ThreadStart(WriteSensor));
			thread1.Start();
		}
	}
}

