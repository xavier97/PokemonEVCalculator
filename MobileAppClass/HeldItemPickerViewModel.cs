using System;
using UIKit;
using System.Collections.Generic;
namespace MobileAppClass
{
    public partial class HeldItemPicker : UIPickerViewModel
    {
        private List<string> _heldItems;
        protected int selectedIndex = 0;

        public HeldItemPicker(List<string> items)
        {
            _heldItems = items;
        }

        public string SelectedItem
        {
            get { return _heldItems[selectedIndex]; }
        }

        public override nint GetComponentCount(UIPickerView picker)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView picker, nint component)
        {
            return _heldItems.Count;
        }

        public override string GetTitle(UIPickerView picker, nint row, nint component)
        {
            return _heldItems[(int)row];
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            selectedIndex = (int)row;
        }
    }
}
