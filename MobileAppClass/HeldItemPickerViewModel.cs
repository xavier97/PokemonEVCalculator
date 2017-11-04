using System;
using UIKit;
using System.Collections.Generic;
namespace MobileAppClass
// https://stackoverflow.com/questions/36510230/xamarin-ios-uipickerview-tutorial/44926538#44926538
// https://developer.xamarin.com/guides/ios/user_interface/controls/picker/

{
    public partial class HeldItemPicker : UIPickerViewModel
    {
        private List<string> _heldItems = new List<string>();
        protected int selectedIndex = 0;

        public HeldItemPicker(List<string> items)
        {
            _heldItems = items;
        }

        public string SelectedItem
        {
            get { return _heldItems[selectedIndex]; }
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return _heldItems.Count;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return _heldItems[(int)row];
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            selectedIndex = (int)row;
        }
    }
}
