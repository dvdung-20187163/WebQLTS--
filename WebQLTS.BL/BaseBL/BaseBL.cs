using Dapper;
using WebQLTS.Common.Entities;
using WebQLTS.Common.Entities.DTO;
using WebQLTS.Common.Enums;
using WebQLTS.Common.Resources;
using WebQLTS.DL;
using static WebQLTS.Common.Attributes.QLTSAttribute;

namespace WebQLTS.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        #region API Get

        /// <summary>
        /// Lấy danh sách toàn bộ bản ghi
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi</returns>
        public IEnumerable<T> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="recordId">ID của bản ghi cần lấy</param>
        /// <returns>Bản ghi có ID được truyền vào</returns>
        public T GetRecordById(Guid recordId)
        {
            return _baseDL.GetRecordById(recordId);
        }

        #endregion

        #region API 

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Đối tượng bản ghi cần thêm mới</param>
        /// <returns>ID của bản ghi vừa thêm. Return về Guid rỗng nếu thêm mới thất bại</returns>
        public ServiceResponse InsertRecord(T record)
        {
            var validateResult = ValidateRequestData(record, Guid.Empty);

            if (validateResult != null && validateResult.Success)
            {
                var newRecordID = _baseDL.InsertRecord(record);

                if (newRecordID != Guid.Empty)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Data = newRecordID
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = new ErrorResult(
                            QLTSErrorCode.InsertFailed,
                            Resource.DevMsg_InsertFailed,
                            Resource.UserMsg_InsertFailed,
                            Resource.MoreInfo_InsertFailed)
                    };
                }
            }
            else
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateResult?.Data
                };
            }
        }


        #endregion

        #region API Update

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="recordId">ID bản ghi cần cập nhật</param>
        /// <param name="record">Đối tượng cần cập nhật theo</param>
        /// <returns>Đối tượng sau khi cập nhật</returns>
        public ServiceResponse UpdateRecord(Guid recordId, T record)
        {
            var validateResult = ValidateRequestData(record, recordId);

            if (validateResult != null && validateResult.Success)
            {
                var inputRecordID = _baseDL.UpdateRecord(recordId, record);

                if (inputRecordID != Guid.Empty)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Data = inputRecordID
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = new ErrorResult(
                            QLTSErrorCode.UpdateFailed,
                            Resource.DevMsg_UpdateFailed,
                            Resource.UserMsg_UpdateFailed,
                            Resource.MoreInfo_UpdateFailed)
                    };
                }
            }
            else
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateResult?.Data
                };
            }
        }

        #endregion

        #region API Delete

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordId">ID bản ghi cần xóa</param>
        /// <returns>ID bản ghi vừa xóa</returns>
        public Guid DeleteRecord(Guid recordID)
        {
            return _baseDL.DeleteRecord(recordID);
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="recordIDs">Danh sách ID các bản ghi cần xóa</param>
        /// <returns>Danh sách ID các bản ghi đã xóa</returns>
        public List<string> DeleteMultiRecords(List<string> recordIDs)
        {
            return _baseDL.DeleteMultiRecords(recordIDs);
        }

        #endregion

        /// <summary>
        /// Validate dữ liệu truyền lên từ API
        /// </summary>
        /// <param name="record">Đối tượng cần validate</param>
        /// <returns>Đối tượng ServiceResponse mô tả validate thành công hay thất bại</returns>
        private ServiceResponse ValidateRequestData(T record, Guid recordID)
        {
            // Validate dữ liệu đầu vào
            var properties = typeof(T).GetProperties();
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record);
                var IsNotNullOrEmptyAttribute = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAttribute));
                var IsNotDuplicateAttribute = (IsNotDuplicateAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotDuplicateAttribute));
                if (IsNotNullOrEmptyAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                {
                    validateFailures.Add(IsNotNullOrEmptyAttribute.ErrorMessage);
                }

                 if (IsNotDuplicateAttribute != null)
                {
                    string duplicate = _baseDL.DuplicateRecordCode(propertyValue, recordID);
                    if(duplicate != null) validateFailures.Add(IsNotDuplicateAttribute.ErrorMessage);
                }

            }

            if (validateFailures.Count > 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateFailures
                };
            }
            return new ServiceResponse
            {
                Success = true
            };
        }

        #endregion
    }

}
