using CreatingDigitalImages.Models;
using System.Collections;
using System.Collections.Generic;

namespace CreatingDigitalImages.ViewModels
{
    public class DirectoryViewModel
    {
        public IEnumerable<CDI> directory { get; set; }
        public string parent { get; set; }
        public string name { get; set; }

    }
}
