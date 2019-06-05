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
        public string  PersonId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string  PersonName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public GenderType Gender { get; set; }
        /// <summary>
        /// 所属组织路径
        /// </summary>
        public string  OrgPath { get; set; }
        /// <summary>
        /// 所属组织唯一标识码
        /// </summary>
        public string  OrgIndexCode { get; set; }
        /// <summary>
        /// 所属组织名称
        /// </summary>
        public string  OrgName { get; set; }
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
        public string  PhoneNo { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string  Address { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string  Email { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string  JobNo { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public PersonPhotoResponse PersonPhoto { get; set; }
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
        UnKnow=0,
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man=1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Woman =2
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
        Passport =414,
        /// <summary>
        /// 驾驶证
        /// </summary>
        [Description("驾驶证")]
        Booklet =113,
        /// <summary>
        /// 驾驶证
        /// </summary>
        [Description("驾驶证")]
        DriveLicense =335,
        /// <summary>
        /// 学生证
        /// </summary>
        [Description("学生证")]
        WorkCard =131,
        /// <summary>
        /// 学生证
        /// </summary>
        [Description("学生证")]
        StudentCard =133,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other =990
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
        Grade=1,
        /// <summary>
        /// 初中
        /// </summary>
        [Description("初中")]
        Junior =2,
        /// <summary>
        /// 中技
        /// </summary>
        [Description("中技")]
        Technical =3,
        /// <summary>
        /// 高中
        /// </summary>
        [Description("高中")]
        Senior =4,
        /// <summary>
        /// 中专
        /// </summary>
        [Description("中专")]
        Secondary =5,
        /// <summary>
        /// 大专
        /// </summary>
        [Description("大专")]
        College =6,
        /// <summary>
        /// 本科
        /// </summary>
        [Description("本科")]
        Undergraduate =7,
        /// <summary>
        /// 硕士
        /// </summary>
        [Description("硕士")]
        Master =8,
        /// <summary>
        /// 博士
        /// </summary>
        [Description("博士")]
        Doctor =9,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other =10
    }

    /// <summary>
    /// 民族类型
    /// </summary>
    public enum NationType
    {
        Han=1,
        Meng=2,
        Hui=3,
        Zang=4,
        Wei=5,
        Miao=6,
        Yi=7,
        Zhuang=8,
        Bu=9,
        Chao=10,
        Man=11,
        Tong=12,
        Yao=13,
        Bai=14,
        TuJia=15,
        Ha=16,
        HaSa=17,
        Dai=18,
        Li=19,
        Su=20,
        Wa=21,
        She=22,
        Gao=23,
        La=24,
        Dong=25,
        Na=27,
        Jing=28,
        Ke=29,
        Tu=30,
        Da=31,
        MuLao=32,
        Qiang=33,
        BuLang=34,
        SaLa=35,
        MaoNan=36,
        QiLao=37,
        XiBo=38,
        AChang=39,
        PuMi=40,
        TaJie=41,
        Nu=42,
        WuZi=43,
        ELuoSi=44,
        EWen=45,
        BengLong=46,
        BaoAn=47,
        YuGu=48,
        JingZu=49,
        TaTa=50,
        DuLong=51,
        ELun=52,
        HeZhe=53,
        MenBa=54,
        LuoBa=55,
        JiNuo=56

    }
}