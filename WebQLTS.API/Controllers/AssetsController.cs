using Microsoft.AspNetCore.Mvc;
using WebQLTS.BL;
using WebQLTS.Common.Entities;
using WebQLTS.Common.Entities.DTO;
using WebQLTS.Common.Enums;
using WebQLTS.Common.Resources;

namespace WebQLTS.API.Controllers
{
    public class AssetsController : BasesController<Asset>
    {
        #region Field

        private IAssetBL _assetBL;

        #endregion

        #region Constructor

        public AssetsController(IAssetBL assetBL) : base(assetBL)
        {
            _assetBL = assetBL;
        }

        #endregion

        [HttpGet("filters")] 
        public IActionResult FilterAssets([FromQuery] string? keyword, [FromQuery] string? departmentID, [FromQuery] string? assetCategoryID, [FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            try
            {
                var filterResponse = _assetBL.FilterAssets(keyword, departmentID, assetCategoryID, limit, offset);
                if (filterResponse != null)
                {

                    return StatusCode(StatusCodes.Status200OK, filterResponse);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.FilterFailed,
                        Resource.DevMsg_FilterFailed,
                        Resource.UserMsg_FilterFailed,
                        Resource.MoreInfo_FilterFailed,
                        HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    QLTSErrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier)) ;
            }
        }

        /// <summary>
        /// Lấy mã tài sản lớn nhất
        /// </summary>
        /// <returns>Mã tài sản lớn nhất</returns>
        [HttpGet("maxCode")]
        public IActionResult GetMaxCode()
        {
            try
            {
                string maxCode = _assetBL.GetMaxCode();

                // Xử lý dữ liệu trả về
                if (maxCode != "")
                {
                    return StatusCode(StatusCodes.Status200OK, maxCode);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.GetMaxCodeFailed,
                        Resource.DevMsg_GetMaxCodeFailed,
                        Resource.UserMsg_GetMaxCodeFailed,
                        Resource.MoreInfo_GetMaxCodeFailed,
                        HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    QLTSErrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception, 
                    HttpContext.TraceIdentifier));
            }
        }
    }
}
