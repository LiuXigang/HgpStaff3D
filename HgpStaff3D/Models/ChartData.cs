using System.Collections.Generic;

namespace HgpStaff3D.Models
{
    public class ChartData
    {
        public string Title { get; set; }
        public List<string> Legend { get; set; } = new List<string>();

        public List<string> XAxisData { get; set; } = new List<string>();

        public Dictionary<string, List<int>> Series { get; set; } = new Dictionary<string, List<int>>();

    }
}
