using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace PKMNEVCalc
{
    /// <summary>
    /// This is our simple picker model. it uses a list of strings as it's data and exposes
    /// a ValueChanged event when the picker changes.
    /// </summary>
    public class PickerDataModel : UIPickerViewModel // NOTE: May one to place this as a protected class inside PokemonSettingViewController
    {
        public event EventHandler<EventArgs> ValueChanged;

        /// <summary>
        /// The items to show up in the picker
        /// </summary>
        public List<string> Items { get; private set; }

        /// <summary>
        /// The current selected item
        /// </summary>
        public string SelectedItem
        {
            get { return Items[selectedIndex]; }
        }

        int selectedIndex = 0;

        public PickerDataModel()
        {
            Items = new List<string>();
        }

        /// <summary>
        /// Called by the picker to determine how many rows are in a given spinner item
        /// </summary>
        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return Items.Count;
        }

        /// <summary>
        /// called by the picker to get the text for a particular row in a particular
        /// spinner item
        /// </summary>
        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return Items[(int)row];
        }

        /// <summary>
        /// called by the picker to get the number of spinner items
        /// </summary>
        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        /// <summary>
        /// called when a row is selected in the spinner
        /// </summary>
        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            selectedIndex = (int)row;
            if (ValueChanged != null)
            {
                ValueChanged(this, new EventArgs());
            }
        }
    }
}