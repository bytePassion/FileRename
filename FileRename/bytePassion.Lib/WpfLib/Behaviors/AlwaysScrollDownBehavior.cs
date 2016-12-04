using System;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace bytePassion.Lib.WpfLib.Behaviors
{
	public class AlwaysScrollDownBehavior : Behavior<ScrollViewer>
	{
		protected override void OnAttached ()
		{
			AssociatedObject.LayoutUpdated += OnLayoutUpdated;						
		}

		protected override void OnDetaching ()
		{
			AssociatedObject.LayoutUpdated -= OnLayoutUpdated;
		}

		private void OnLayoutUpdated(object sender, EventArgs eventArgs)
		{
			AssociatedObject.ScrollToEnd();
		}		
	}
}