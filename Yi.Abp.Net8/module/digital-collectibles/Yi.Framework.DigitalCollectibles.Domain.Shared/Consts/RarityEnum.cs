using System.ComponentModel.DataAnnotations;

namespace Yi.Framework.DigitalCollectibles.Domain.Shared.Consts;

/// <summary>
/// 稀有度枚举
/// </summary>
public enum RarityEnum
{
    [Display(Name = "普通")] Ordinary = 0,
    [Display(Name = "高级")] Senior = 1,
    [Display(Name = "稀有")] Rare = 2,
    [Display(Name = "珍品")] Gem = 3,
    [Display(Name = "传说")] Legend = 4
}