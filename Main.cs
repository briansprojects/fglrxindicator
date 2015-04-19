using Gtk;
//using AppIndicator;

namespace fglrxindicator
{
	public class MainClass
	{
		public static void Main ()
		{
			Application.Init ();

			var win = new Window ("Fglrx Indicator");

			win.Resize (300, 200);
			win.Add (new Label("Hello Ubuntu"));
			win.ShowAll ();

			Application.Run ();

			//TODO: Actually create an indicator instead of a gtk window
//			var indicator = new ApplicationIndicator
//			(
//			"my-id",
//			"my-na,e",
//			Category.ApplicationStatus
//			);
//
//			indicator.Status = Status.Attention;


		}
	}
}
