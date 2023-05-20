using static WebQLTS.Common.Attributes.QLTSAttribute;

namespace WebQLTS.Common.Entities
{
    /// <summary>
    /// Loại tài sản
    /// </summary>
    public class AssetCategory : BaseEntity
    {
        /// <summary>
        /// ID loại tài sản
        /// </summary>
        [PrimaryKey]
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string fixed_asset_category_name { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        public Decimal depreciation_rate { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public string life_time { get; set; }

    }
}
