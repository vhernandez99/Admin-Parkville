using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Admin_Parkville.iOS;
using Admin_Parkville.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace Admin_Parkville.iOS
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            // remove the border form the entry
            if (Control != null)
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }
    }
}