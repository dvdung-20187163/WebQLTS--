using Microsoft.AspNetCore.Mvc;
using WebQLTS.BL;
using WebQLTS.Common.Entities.DTO;
using WebQLTS.Common.Enums;
using WebQLTS.Common.Resources;

namespace WebQLTS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    { 

        #region Field 

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

	    #endregion

        #region Method

        /// <summary>
        /// API Lấy danh sách toàn bộ bản ghi trong 1 bảng
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi trong bảng</returns>
        [HttpGet]
        [Route("")]
        public IActionResult GetAllRecords()
        {
            try
            {
                var records = _baseBL.GetAllRecords();
                if (records != null)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                        QLTSErrorCode.Exception,
                        Resource.DevMsg_GetAllFailed,
                        Resource.UserMsg_GetAllFailed,
                        Resource.MoreInfo_GetAllFailed,
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

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="recordId">ID của bản ghi cần lấy</param>
        /// <returns>Bản ghi có ID được truyền vào</returns>
        [HttpGet("{recordID}")]
        public IActionResult GetRecordById([FromRoute] Guid recordID)
        {
            try
            {
                T record = _baseBL.GetRecordById(recordID);
                if (record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.InsertFailed,
                        Resource.DevMsg_InsertFailed,
                        Resource.UserMsg_InsertFailed,
                        Resource.MoreInfo_InsertFailed,
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

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Đối tượng cần thêm mới</param>
        /// <returns>ID đối tượng vừa thêm mới</returns>
        [HttpPost]
        public IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                var result = _baseBL.InsertRecord(record);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status201Created, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.ValidateFailed,
                        Resource.DevMsg_ValidateFailed,
                        Resource.UserMsg_ValidateFailed,
                        result.Data,
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

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="recordId">ID bản ghi cần cập nhật</param>
        /// <param name="record">Đối tượng cần cập nhật theo</param>
        /// <returns>Đối tượng sau khi cập nhật</returns>
        [HttpPut("{recordID}")] 
        public IActionResult UpdateRecord([FromRoute] Guid recordID, [FromBody] T record)
        {
            try
            
            {
                var result = _baseBL.UpdateRecord(recordID, record);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.ValidateFailed,
                        Resource.DevMsg_ValidateFailed,
                        Resource.UserMsg_ValidateFailed,
                        result.Data,
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

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordID">ID bản ghi cần xóa</param>
        /// <returns>ID bản ghi vừa xóa</returns>
        [HttpDelete("{recordID}")]
        public IActionResult DeleteRecord([FromRoute] Guid recordID)
        {
            try
            {
                var result = _baseBL.DeleteRecord(recordID);

                if (result != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.DeleteFailed,
                        Resource.DevMsg_DeleteFailed,
                        Resource.UserMsg_DeleteFailed,
                        result,
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

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="recordIDs">Danh sách ID các bản ghi cần xóa</param>
        /// <returns>Danh sách ID các bản ghi đã xóa</returns>
        [HttpPost("batch-delete")]
        public IActionResult DeleteMultiRecords([FromBody] List<string> recordIDs)
        {
            try
            {
                List<string> results = _baseBL.DeleteMultiRecords(recordIDs);

                if (results.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, results);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        QLTSErrorCode.BatchDeleteFailed,
                        Resource.DevMsg_BatchDeleteFailed,
                        Resource.UserMsg_BatchDeleteFailed,
                        recordIDs,
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


        #endregion

    }
}
