using System.ComponentModel;

namespace HgpStaff3D.Domain.AggregatesModel
{
    public enum Organization
    {
        [Description("CEO办公室")]
        Ceo = 0,
        [Description("内控管理部")]
        InternalControl = 1,
        [Description("战略投资部")]
        StrategicInvestment = 2,
        [Description("人力资源部")]
        HumanResources = 3,
        [Description("财务管理部")]
        FinancialManagement = 4,
        [Description("产品研发部")]
        ProductDevelopment = 5,
        [Description("供应链")]
        SupplyChain = 6,
        [Description("用户中心")]
        UserCenter = 7,
        [Description("外部联系人")]
        ExternalContact = 8
    }
}
