using WebQLTS.Common.Entities.DTO;
using WebQLTS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebQLTS.BL
{
    public interface IBaseBL<T>
    {
        #region API Get

        /// <summary>
        /// Lấy danh sách toàn bộ bản ghi
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi</returns>
        public IEnumerable<T> GetAllRecords();

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="recordId">ID của bản ghi cần lấy</param>
        /// <returns>Bản ghi có ID được truyền vào</returns>
        public T GetRecordById(Guid recordId);

        #endregion

        #region API Insert

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Đối tượng bản ghi cần thêm mới</param>
        /// <returns>ID của bản ghi vừa thêm. Return về Guid rỗng nếu thêm mới thất bại</returns>
        public ServiceResponse InsertRecord(T record);

        #endregion

        #region API Update

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="recordID">ID bản ghi cần cập nhật</param>
        /// <param name="record">Đối tượng cần cập nhật theo</param>
        /// <returns>Đối tượng sau khi cập nhật</returns>
        public ServiceResponse UpdateRecord(Guid recordID, T record);

        #endregion

        #region API Delete

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordID">ID bản ghi cần xóa</param>
        /// <returns>ID bản ghi vừa xóa</returns>
        public Guid DeleteRecord(Guid recordID);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="recordIDs">Danh sách ID các bản ghi cần xóa</param>
        /// <returns>Danh sách ID các bản ghi đã xóa</returns>
        public List<string> DeleteMultiRecords(List<string> recordIDs);

        /// <summary>
        /// Kiểm tra trùng mã bản ghi
        /// </summary>
        /// <param name="recordCode"></param>
        /// <param name="recordID"></param>
        /// <returns>Mã bản ghi bị trùng</returns>

        #endregion
    }
}
