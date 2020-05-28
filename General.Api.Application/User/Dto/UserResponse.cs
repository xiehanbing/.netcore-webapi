using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace General.Api.Application.User.Dto
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// id
        /// </summary>
        public string PersonId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string PersonName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public GenderType Gender { get; set; }
        /// <summary>
        /// 所属组织路径
        /// </summary>
        public string OrgPath { get; set; }
        /// <summary>
        /// 所属组织唯一标识码
        /// </summary>
        public string OrgIndexCode { get; set; }
        /// <summary>
        /// 所属组织名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public CertificateType Certificate { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string CertificateNo { get; set; }
        /// <summary>
        /// 指纹信息
        /// </summary>
        public List<FingerPrintResponse> FingerPrint { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PhoneNo { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string JobNo { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        //public PersonPhotoResponse PersonPhoto { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public EducationType Education { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public NationType Nation { get; set; }
    }
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum GenderType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnow = 0,
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man = 1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Woman = 2
    }
    /// <summary>
    /// 证件类型
    /// </summary>
    public enum CertificateType
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("身份证")]
        IdentityCard = 111,
        /// <summary>
        /// 护照
        /// </summary>
        [Description("护照")]
        Passport = 414,
        /// <summary>
        /// 驾驶证
        /// </summary>
        [Description("驾驶证")]
        Booklet = 113,
        /// <summary>
        /// 驾驶证
        /// </summary>
        [Description("驾驶证")]
        DriveLicense = 335,
        /// <summary>
        /// 学生证
        /// </summary>
        [Description("学生证")]
        WorkCard = 131,
        /// <summary>
        /// 学生证
        /// </summary>
        [Description("学生证")]
        StudentCard = 133,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 990
    }
    /// <summary>
    /// 学历类型
    /// </summary>
    public enum EducationType
    {
        /// <summary>
        /// 小学
        /// </summary>
        [Description("小学")]
        Grade = 1,
        /// <summary>
        /// 初中
        /// </summary>
        [Description("初中")]
        Junior = 2,
        /// <summary>
        /// 中技
        /// </summary>
        [Description("中技")]
        Technical = 3,
        /// <summary>
        /// 高中
        /// </summary>
        [Description("高中")]
        Senior = 4,
        /// <summary>
        /// 中专
        /// </summary>
        [Description("中专")]
        Secondary = 5,
        /// <summary>
        /// 大专
        /// </summary>
        [Description("大专")]
        College = 6,
        /// <summary>
        /// 本科
        /// </summary>
        [Description("本科")]
        Undergraduate = 7,
        /// <summary>
        /// 硕士
        /// </summary>
        [Description("硕士")]
        Master = 8,
        /// <summary>
        /// 博士
        /// </summary>
        [Description("博士")]
        Doctor = 9,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 10
    }

    /// <summary>
    /// 民族类型
    /// </summary>
    public enum NationType
    {
        /// <summary>
        /// 
        /// </summary>
        Han = 1,
        /// <summary>
        /// 
        /// </summary>
        Meng = 2,
        /// <summary>
        /// 
        /// </summary>
        Hui = 3,
        /// <summary>
        /// 
        /// </summary>
        Zang = 4,
        /// <summary>
        /// 
        /// </summary>
        Wei = 5,
        /// <summary>
        /// 
        /// </summary>
        Miao = 6,
        /// <summary>
        /// 
        /// </summary>
        Yi = 7,
        /// <summary>
        /// 
        /// </summary>
        Zhuang = 8,
        /// <summary>
        /// 
        /// </summary>
        Bu = 9,
        /// <summary>
        /// 
        /// </summary>
        Chao = 10,
        /// <summary>
        /// 
        /// </summary>
        Man = 11,
        /// <summary>
        /// 
        /// </summary>
        Tong = 12,
        /// <summary>
        /// 
        /// </summary>
        Yao = 13,
        /// <summary>
        /// 
        /// </summary>
        Bai = 14,
        /// <summary>
        /// 
        /// </summary>
        TuJia = 15,
        /// <summary>  
        ///    
        /// </summary>
        Ha = 16,
        /// <summary>  
        ///    
        /// </summary>
        HaSa = 17,
        /// <summary>  
        ///    
        /// </summary>
        Dai = 18,
        /// <summary>  
        ///    
        /// </summary>
        Li = 19,
        /// <summary>  
        ///    
        /// </summary>
        Su = 20,
        /// <summary>  
        ///    
        /// </summary>
        Wa = 21,
        /// <summary>  
        ///    
        /// </summary>
        She = 22,
        /// <summary>  
        ///    
        /// </summary>
        Gao = 23,
        /// <summary>  
        ///    
        /// </summary>
        La = 24,
        /// <summary>  
        ///    
        /// </summary>
        Dong = 25,
        /// <summary>  
        ///    
        /// </summary>
        Na = 27,
        /// <summary>  
        ///    
        /// </summary>
        Jing = 28,
        /// <summary>  
        ///    
        /// </summary>
        Ke = 29,
        /// <summary>  
        ///    
        /// </summary>
        Tu = 30,
        /// <summary>  
        ///    
        /// </summary>
        Da = 31,
        /// <summary>  
        ///    
        /// </summary>
        MuLao = 32,
        /// <summary>  
        ///    
        /// </summary>
        Qiang = 33,
        /// <summary>  
        ///    
        /// </summary>
        BuLang = 34,
        /// <summary>  
        ///    
        /// </summary>
        SaLa = 35,
        /// <summary>  
        ///    
        /// </summary>
        MaoNan = 36,
        /// <summary>  
        ///    
        /// </summary>
        QiLao = 37,
        /// <summary>  
        ///    
        /// </summary>
        XiBo = 38,
        /// <summary>  
        ///    
        /// </summary>
        AChang = 39,
        /// <summary>  
        ///    
        /// </summary>
        PuMi = 40,
        /// <summary>  
        ///    
        /// </summary>
        TaJie = 41,
        /// <summary>  
        ///    
        /// </summary>
        Nu = 42,
        /// <summary>  
        ///    
        /// </summary>
        WuZi = 43,
        /// <summary>  
        ///    
        /// </summary>
        ELuoSi = 44,
        /// <summary>  
        ///    
        /// </summary>
        EWen = 45,
        /// <summary>  
        ///    
        /// </summary>
        BengLong = 46,
        /// <summary>  
        ///    
        /// </summary>
        BaoAn = 47,
        /// <summary>  
        ///    
        /// </summary>
        YuGu = 48,
        /// <summary>  
        ///    
        /// </summary>
        JingZu = 49,
        /// <summary>  
        ///    
        /// </summary>
        TaTa = 50,
        /// <summary>  
        ///    
        /// </summary>
        DuLong = 51,
        /// <summary>  
        ///    
        /// </summary>
        ELun = 52,
        /// <summary>  
        ///    
        /// </summary>
        HeZhe = 53,
        /// <summary>  
        ///    
        /// </summary>
        MenBa = 54,
        /// <summary>  
        ///    
        /// </summary>
        LuoBa = 55,
        /// <summary>  
        ///    
        /// </summary>
        JiNuo = 56


    }
}