namespace Yi.Framework.GoView.Application.Contracts.Dtos
{
    /// <summary>
    /// GoView 返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GoViewResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; } = string.Empty;

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int? Count { get; set; }
    }
}
