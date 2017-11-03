using System;
using System.Collections.Generic;
namespace MobileAppClass
{
    public class MyViewModel
    {
        List<String> heldItems;

        public MyViewModel()
        {
            // Populate list
            heldItems.Add("Power Bracer");
            heldItems.Add("Macho Brace");

            // Instantiate list
            heldItems = new List<String>;
        }
    }
}
