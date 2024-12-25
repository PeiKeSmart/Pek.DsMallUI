namespace Pek.DsMallUI.Models;

/// <summary>
/// 折叠实体
/// </summary>
public class Hierarchy
{
    /// <summary>
    /// 当前层级/深度
    /// </summary>
    public Int32 deep { get; set; }

    /// <summary>
    /// 编号
    /// </summary>
    public Int64 gc_id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public String? gc_name { get; set; }

    /// <summary>
    /// 父编号
    /// </summary>
    public Int64 gc_parent_id { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public Int32 type { get; set; }

    /// <summary>
    /// 父区域编号
    /// </summary>
    public Int64 ParentCode { get; set; }

    /// <summary>
    /// 区域编号
    /// </summary>
    public Int64 AreaCode { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public Int64 Sort { get; set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    public Int32 gc_show { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public Int32 gc_sort { get; set; }

    /// <summary>
    /// 是否有子节点
    /// </summary>
    public Int32 have_child { get; set; }

    /// <summary>
    /// 添加人
    /// </summary>
    public String CreateUser { get; set; } = "";

    /// <summary>
    /// 添加时间
    /// </summary>
    public String? CreateTime { get; set; }

    /// <summary>
    /// 是否系统
    /// </summary>
    public Boolean IsSystem { get; set; }
}