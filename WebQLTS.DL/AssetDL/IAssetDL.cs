using WebQLTS.Common.Entities;
using WebQLTS.Common.Entities.DTO;

namespace WebQLTS.DL
{

    public interface IAssetDL : IBaseDL<Asset>
    {
        /// <summary>
        /// Lấy danh sách các tài sản có chọn lọc
        /// </summary>
        /// <param name="keyword">Từ khóa để tìm kiếm theo mã và tên tài sản</param>
        /// <param name="departmentID">ID phòng ban</param>
        /// <param name="assetCategoryID">ID loại tài sản</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="offset">vị trí bản ghi bắt đầu lấy</param>
        /// <returns>Danh sách các tài sản sau khi lọc và các tổng giá trị khác</returns>
        public PagingData<Asset> FilterAssets(
            string? keyword, 
            string? departmentID, 
            string? assetCategoryID, 
            int limit, 
            int offset);

        /// <summary>
        /// Lấy mã tài sản lớn nhất
        /// </summary>
        /// <returns>Mã tài sản lớn nhất</returns>
        public string GetMaxCode();

    }

    

}
