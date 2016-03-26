using Gtk;

namespace fglrxindicator
{
	public class MainClass
	{
		public static void Main ()
		{
			Application.Init ();
			var win = new FglrxIndicator ();
			Application.Run ();
		}
	}
}
