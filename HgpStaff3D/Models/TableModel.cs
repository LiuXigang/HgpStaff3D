using System.Collections.Generic;

namespace HgpStaff3D.Models
{
    public class TableModel
    {
        public List<string> Org { get; set; } = new List<string>();
        public List<List<string>> RowsData { get; set; } = new List<List<string>>();
    }
}
