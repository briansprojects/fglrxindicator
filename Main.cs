using Gtk;
//using AppIndicator;

namespace fglrxindicator
{
	public class MainClass
	{
		public static void Main ()
		{
			Application.Init ();
			var win = new PreferencesWindow ();
			Application.Run ();
		}
	}
}
