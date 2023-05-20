namespace WebQLTS.Common.Entities.DTO
{ 
    public class PagingData <T>
    {
        /// <summary>
        /// Danh sách các bản ghi hiển thị
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int? TotalCount { get; set; }

        /// <summary>
        /// Tổng số lượng 
        /// </summary>
        public int? TotalQuantity { get; set; }

        /// <summary>
        /// Tổng nguyên giá
        /// </summary>
        public decimal? TotalCost { get; set; }

        /// <summary>
        /// Tổng hao mòn lũy kế
        /// </summary>
        public float? TotalDepreciation { get; set; }

        /// <summary>
        /// Tổng giá trị còn lại
        /// </summary>
        public decimal? TotalCostRemain { get; set; }


        public PagingData() { }

        public PagingData(IEnumerable<T> data, int? totalCount, int? totalQuantity, decimal? totalCost, float? totalDepreciation, decimal? totalCostRemain)
        {
            Data = data;
            TotalCount = totalCount;
            TotalQuantity = totalQuantity;
            TotalCost = totalCost;
            TotalDepreciation = totalDepreciation;
            TotalCostRemain = totalCostRemain;
        }
    }


}
