﻿namespace Yi.Framework.DigitalCollectibles.Domain.Shared.Dtos;

/// <summary>
/// 矿池内容
/// </summary>
public class MiningPoolContent
{
   public MiningPoolContent(DateTime startTime, DateTime endTime)
   {
      StartTime = startTime;
      EndTime = endTime;
   }

   /// <summary>
   /// 普通藏品剩余数量
   /// </summary>
   public int I0_OrdinaryNumber { get; set; }
   
   /// <summary>
   /// 高级藏品剩余数量
   /// </summary>
   public int I1_SeniorNumber { get; set; }
   
   /// <summary>
   /// 稀有藏品剩余数量
   /// </summary>
   public int I2_RareNumber { get; set; }
   
   /// <summary>
   /// 珍品藏品剩余数量
   /// </summary>
   public int I3_GemNumber { get; set; }

   /// <summary>
   /// 传说藏品剩余数量
   /// </summary>
   public int I4_LegendNumber { get; set; }
   
   /// <summary>
   /// 开始时间
   /// </summary>
   public DateTime StartTime{ get; set; }
   
   /// <summary>
   /// 结束时间
   /// </summary>
   public DateTime EndTime{ get; set; }
   
}

