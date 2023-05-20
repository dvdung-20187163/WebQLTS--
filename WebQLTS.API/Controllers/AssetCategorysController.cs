using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebQLTS.BL;
using WebQLTS.Common.Entities;
using WebQLTS.DL;
using MySqlConnector;

namespace WebQLTS.API.Controllers
{
    public class AssetCategorysController : BasesController<AssetCategory>
    {
        public AssetCategorysController(IBaseBL<AssetCategory> baseBL) : base(baseBL)
        {
        }

        /// <summary>
        /// Lấy danh sách toàn bộ bản ghi
        /// </summary>
        /// <param name="name">Tên để tìm kiếm bản ghi</param>
        /// <param name="code">Mã code để tìm kiếm</param>
        /// <returns>Danh sách các bản ghi sau khi chọn lọc</returns>
        [HttpGet("filter")]
        public IActionResult FilterDepartment(string? name, string? code)
        {
            try
            {
                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                var whereConditions = new List<string>();
                if (code != null) whereConditions.Add($"(fixed_asset_category_code LIKE \'%{code}%\') ");
                if (name != null) whereConditions.Add($" (fixed_asset_category_name LIKE \'%{name}%\')");
                string whereClause = string.Join("AND", whereConditions);
                parameters.Add("d_Where", whereClause);

                // Khai báo tên procedure
                string storedProcedure = "Proc_assetCategory_Filter";

                // Khởi tạo kết nối tới DB MySql
                using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
                {
                    // Thực hiện gọi vào DB
                    var assetCategories = mysqlConnection.Query<AssetCategory>(
                        storedProcedure,
                        parameters,
                        commandType: System.Data.CommandType.StoredProcedure);

                    return StatusCode(StatusCodes.Status200OK, assetCategories);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "e001");
            }

        }
    }
}
