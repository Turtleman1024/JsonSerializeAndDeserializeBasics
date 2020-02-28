using System;
using System.Collections.Generic;
using System.Text;

namespace JsonToDataTable
{
    public class ClassRoom <T>
    {
        public int ClassId { get; set; }
        public T Student { get; set; }
    }
}
