using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;


namespace bytePassion.Lib.WpfLib
{
	/// <summary>
	/// The shared resource dictionary is a specialized resource dictionary
	/// that loads it content only once. If a second instance with the same source
	/// is created, it only merges the resources from the cache.
	/// </summary>
	public class SharedResourceDictionary : ResourceDictionary
	{
		private static readonly Dictionary<Uri, ResourceDictionary> CachedDictionaries =
			new Dictionary<Uri, ResourceDictionary>();

		
		private Uri sourceUri;
		
		public new Uri Source
		{
			get 
			{ 
				if (IsInDesignMode) 
					return base.Source;

				return sourceUri;
			}
			set
			{
				if (IsInDesignMode)
				{
					base.Source = value;
					return;
				} 

				sourceUri = new Uri(value.OriginalString);

				lock (((ICollection)CachedDictionaries).SyncRoot)
				{
					if (!CachedDictionaries.ContainsKey(value))
					{
						base.Source = value;
						CachedDictionaries.Add(value, this);
						return;
					}

					lock (((ICollection)MergedDictionaries).SyncRoot)
					{
						MergedDictionaries.Add(CachedDictionaries[value]);
					}
				}								
			}
		}

		private static bool IsInDesignMode
		{
			get
			{
				return (bool)DependencyPropertyDescriptor
								.FromProperty(DesignerProperties.IsInDesignModeProperty,typeof(DependencyObject))
								.Metadata
								.DefaultValue;
			}
		} 		
	}
}