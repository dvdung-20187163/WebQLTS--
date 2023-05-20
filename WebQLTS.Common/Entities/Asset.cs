using static WebQLTS.Common.Attributes.QLTSAttribute;

namespace WebQLTS.Common.Entities
{
    public class Asset : BaseEntity
    {
        /// <summary>
        /// ID tài sản
        /// </summary>
        [PrimaryKey]
        public Guid fixed_asset_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        [IsNotNullOrEmpty("Mã tài sản không được để trống")]
        [IsNotDuplicateAttribute("Mã tài sản không được trùng")]
        public string fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        [IsNotNullOrEmpty("Tên tài sản không được để trống")]
        public string fixed_asset_name { get; set; }

        /// <summary>
        /// ID bộ phận sử dụng
        /// </summary>
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã bộ phận sử dụng
        /// </summary>
        [IsNotNullOrEmpty("Mã bộ phận sử dụng không được để trống")]
        public string? department_code { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        public string? department_name { get; set; }

        /// <summary>
        /// ID loại tài sản
        /// </summary>
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        [IsNotNullOrEmpty("Mã loại tài sản không được để trống")]
        public string fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string fixed_asset_category_name { get; set; }

        /// <summary>
        /// Giá tiền
        /// </summary>
        [IsNotNullOrEmpty("Nguyên giá không được để trống")]
        public double cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        [IsNotNullOrEmpty("Số lượng không được để trống")]
        public int quantity { get; set; }

        /// <summary>
        /// Tỉ lệ hao mòn (%)
        /// </summary>
        [IsNotNullOrEmpty("Tỷ lệ hao mòn không được để trống")]
        public Decimal depreciation_rate { get; set; }

        /// <summary>
        /// Năm theo dõi
        /// </summary>
        public int? tracked_year { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        [IsNotNullOrEmpty("Số năm sử dụng không được để trống")]
        public int life_time { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        [IsNotNullOrEmpty("Ngày mua không được để trống")]
        public DateTime purchase_date { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        [IsNotNullOrEmpty("Ngày bắt đầu sử dụng không được để trống")]
        public DateTime production_date { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm không được để trống
        /// </summary>
        [IsNotNullOrEmpty("Giá trị hao mòn năm không được để trống")]
        public double depreciation_year { get; set; }

    }

}
