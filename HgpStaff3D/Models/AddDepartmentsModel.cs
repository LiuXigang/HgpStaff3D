using System;
using System.ComponentModel.DataAnnotations;

namespace HgpStaff3D.Models
{
    public class AddDepartmentsModel
    {
        [Display(Name = "时间")]
        public DateTime Time { get; set; }
        [Display(Name = "CEO办公室")]
        public int CeoNo { get; set; }
        [Display(Name = "内控管理部")]
        public int InternalControlNo { get; set; }
        [Display(Name = "战略投资部")]
        public int StrategicInvestmentNo { get; set; }
        [Display(Name = "人力资源部")]
        public int HumanResourcesNo { get; set; }
        [Display(Name = "财务管理部")]
        public int FinancialManagementNo { get; set; }
        [Display(Name = "产品研发部")]
        public int ProductDevelopmentNo { get; set; }
        [Display(Name = "供应链")]
        public int SupplyChainNo { get; set; }
        [Display(Name = "用户中心")]
        public int UserCenterNo { get; set; }
        [Display(Name = "外部联系人")]
        public int ExternalContactrNo { get; set; }
    }
}
