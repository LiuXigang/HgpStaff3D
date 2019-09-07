using System.ComponentModel;

namespace HgpStaff3D.Domain.AggregatesModel
{
    public enum CapUpdate
    {
        [Description("没有被更新")]
        UnUpdate = 0,
        [Description("被更新")]
        Updated = 1,
    }
}
