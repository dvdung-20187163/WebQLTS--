using Dapper;
using WebQLTS.Common.Entities;
using WebQLTS.Common.Entities.DTO;
using MySqlConnector;

namespace WebQLTS.DL
{
    
    public class AssetDL : BaseDL<Asset>, IAssetDL
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
            int offset)
        {
            // Chuẩn bị tham số đầu vào cho procedure
            var parameters = new DynamicParameters();
            parameters.Add("d_Limit", limit);
            parameters.Add("d_Offset", offset);
            parameters.Add("d_Sort", "");

            var whereConditions = new List<string>();
            if (keyword != null) whereConditions.Add($"(fixed_asset_code LIKE \'%{keyword}%\' OR fixed_asset_name LIKE \'%{keyword}%\')");
            if (departmentID != null) whereConditions.Add($"department_id LIKE \'{departmentID}\'");
            if (assetCategoryID != null) whereConditions.Add($"fixed_asset_category_id LIKE \'{assetCategoryID}\'");
            string whereClause = string.Join(" AND ", whereConditions);

            parameters.Add("d_Where", whereClause);

            // Khai báo tên prodecure Insert
            string storedProcedureName = "Proc_asset_GetPaging";

            // Khởi tạo kết nối tới DB MySQL
            string connectionString = DataContext.MySqlConnectionString;
            var filterResponse = new PagingData<Asset>();
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                // Thực hiện gọi vào DB để chạy procedure
                var multiAssets = mysqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý dữ liệu trả về
                var assets = multiAssets.Read<Asset>();
                var totalCount = multiAssets.Read<int>().Single();
                var totalQuantity = multiAssets.Read<int>().Single();
                var totalCost = multiAssets.Read<decimal>().Single();
                var totalDepreciation = multiAssets.Read<float>().Single();
                var totalCostRemain = multiAssets.Read<decimal>().Single();
                filterResponse = new PagingData<Asset>(assets, totalCount, totalQuantity, totalCost, totalDepreciation, totalCostRemain) ;
            }

            return filterResponse;
        }

        /// <summary>
        /// Lấy mã tài sản lớn nhất
        /// </summary>
        /// <returns>Mã tài sản lớn nhất</returns>
        public string GetMaxCode()
        {
            // Khai báo tên prodecure Insert
            string storedProcedureName = "Proc_asset_GetMaxCode";

            // Khởi tạo kết nối tới DB MySQL
            string connectionString = DataContext.MySqlConnectionString;
            string maxCode = "";
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                maxCode = mysqlConnection.QueryFirstOrDefault<string>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);
            }
            // Xử lý dữ liệu trả về
            if (maxCode != null)
            {
                return maxCode;
            }
            else
            {
                return "";
            }
        }

    }
}
