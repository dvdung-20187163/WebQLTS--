using WebQLTS.Common.Entities;
using WebQLTS.Common.Entities.DTO;
using WebQLTS.DL;

namespace WebQLTS.BL
{
    public class AssetBL : BaseBL<Asset>, IAssetBL
    {
        #region Field

        private IAssetDL _assetDL;

        #endregion

        #region Constructer

        public AssetBL(IAssetDL assetDL) : base(assetDL)
        {
            _assetDL = assetDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách các tài sản có chọn lọc
        /// </summary>
        /// <param name="keyword">Từ khóa để tìm kiếm theo mã và tên tài sản</param>
        /// <param name="departmentID">ID phòng ban</param>
        /// <param name="assetCategoryID">ID loại tài sản</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="offset">vị trí bản ghi bắt đầu lấy</param>
        /// <returns>Danh sách các tài sản sau khi lọc và các tổng giá trị khác</returns>
        public PagingData<Asset> FilterAssets(string? keyword, string? departmentID, string? assetCategoryID, int limit, int offset)
        {
            return _assetDL.FilterAssets(keyword, departmentID, assetCategoryID, limit, offset);
        }

        /// <summary>
        /// Lấy mã tài sản lớn nhất
        /// </summary>
        /// <returns>Mã tài sản lớn nhất</returns>
        public string GetMaxCode()
        {
            return _assetDL.GetMaxCode();
        }

        #endregion
    }
}
