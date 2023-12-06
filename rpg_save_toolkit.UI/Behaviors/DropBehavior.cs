using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace rpg_save_toolkit.UI.Behaviors
{
    public class DropBehavior : Behavior<UIElement>
    {
        private bool _backup_allowDrop = false;
        protected override void OnAttached()
        {
            base.OnAttached();
            _backup_allowDrop = AssociatedObject.AllowDrop;
            AssociatedObject.AllowDrop = true;

            AssociatedObject.DragEnter += AssociatedObject_DragEnter;
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {

            }
        }


        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.AllowDrop = _backup_allowDrop;
            AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
        }
    }
}
