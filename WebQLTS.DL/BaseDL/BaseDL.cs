using Dapper;
using WebQLTS.Common.Resources;
using MySqlConnector;
using static WebQLTS.Common.Attributes.QLTSAttribute;

namespace WebQLTS.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region DL GET

        /// <summary>
        /// Lấy danh sách toàn bộ bản ghi
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi</returns>
        public IEnumerable<T> GetAllRecords()
        {
            // Khai báo tên stored procedure
            string storedProcedureName = String.Format(Resource.Proc_GetAll, typeof(T).Name);

            // Khởi tạo kết nối tới DB MySQL
            using (var mysqlConnection = new MySqlConnection(DataContext.MySqlConnectionString))
            {
                // Thực hiện gọi vào DB
                var records = mysqlConnection.Query<T>(
                    storedProcedureName,
                    commandType: System.Data.CommandType.StoredProcedure);

                return records;
            }
        }

        /// <summary>
        /// Lấy 1 tài sản theo id
        /// </summary>
        /// <param name="recordID">ID của bản ghi cần lấy</param>
        /// <returns>Bản ghi có ID được truyền vào</returns>
        public T GetRecordById(Guid recordID)
        {
            // Chuẩn bị tham số đầu vào cho procedure
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var primaryKeyAttribute = (PrimaryKeyAttribute)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));
                if (primaryKeyAttribute != null)
                {
                    parameters.Add($"d_{property.Name}", recordID);
                    break;
                }
            }

            // Khởi tạo kết nối tới DB MySQL
            string connectionString = DataContext.MySqlConnectionString;
            T record;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                // Khai báo tên prodecure
                string storedProcedureName = String.Format(Resource.Proc_GetDetailOne, typeof(T).Name);

                // Thực hiện gọi vào DB để chạy procedure
                record = mysqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return record;
        }

        #endregion

        #region DL POST

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Đối tượng bản ghi cần thêm mới</param>
        /// <returns>ID của bản ghi vừa thêm. Return về Guid rỗng nếu thêm mới thất bại</returns>
        public Guid InsertRecord(T record)
        {
            // Chuẩn bị tham số đầu vào cho procedure
            var parameters = new DynamicParameters();
            var newRecordID = Guid.NewGuid();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                object propertyValue;
                var primaryKeyAttribute = (PrimaryKeyAttribute)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));
                if (primaryKeyAttribute != null)
                {
                    propertyValue = newRecordID;
                }
                else
                {
                    propertyValue = property.GetValue(record, null);
                }
                
                parameters.Add($"d_{propertyName}", propertyValue);
            }

            // Khởi tạo kết nối tới DB MySQL
            string connectionString = DataContext.MySqlConnectionString;
            int numberOfAffectedRows = 0;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                // Khai báo tên prodecure
                string storedProcedureName = String.Format(Resource.Proc_InsertOne, typeof(T).Name);

                // Thực hiện gọi vào DB để chạy procedure
                numberOfAffectedRows = mysqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            // Xử lý dữ liệu trả về
            if (numberOfAffectedRows > 0)
            {
                return newRecordID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        #endregion

        #region DL PUT

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="recordID">ID bản ghi cần cập nhật</param>
        /// <param name="record">Đối tượng cần cập nhật theo</param>
        /// <returns>ID của bản ghi sau khi cập nhật. Return về Guid rỗng nếu cập nhật thất bại</returns>
        public Guid UpdateRecord(Guid recordID, T record)
        {
            // Chuẩn bị tham số đầu vào cho procedure
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                object propertyValue;
                var primaryKeyAttribute = (PrimaryKeyAttribute)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));
                if (primaryKeyAttribute != null)
                {
                    propertyValue = recordID;
                }
                else
                {
                    propertyValue = property.GetValue(record, null);
                }
                parameters.Add($"d_{propertyName}", propertyValue);
            }

            // Khởi tạo kết nối tới DB MySQL
            string connectionString = DataContext.MySqlConnectionString;
            int numberOfAffectedRows = 0;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                // Khai báo tên prodecure
                string storedProcedureName = String.Format(Resource.Proc_Update, typeof(T).Name);

                // Thực hiện gọi vào DB để chạy procedure
                numberOfAffectedRows = mysqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            // Xử lý dữ liệu trả về
            if (numberOfAffectedRows > 0)
            {
                return recordID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        #endregion

        #region DL DELETE

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordID">ID bản ghi cần xóa</param>
        /// <returns>ID bản ghi vừa xóa</returns>
        public Guid DeleteRecord(Guid recordID)
        {
            // Chuẩn bị tham số đầu vào cho procedure
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var primaryKeyAttribute = (PrimaryKeyAttribute)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));
                if (primaryKeyAttribute != null)
                {
                    parameters.Add($"d_{property.Name}", recordID);
                    break;
                }
            }

            // Khởi tạo kết nối tới DB MySQL
            string connectionString = DataContext.MySqlConnectionString;
            int numberOfAffectedRows = 0;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                // Khai báo tên prodecure
                string storedProcedureName = String.Format(Resource.Proc_DeleteOne, typeof(T).Name);

                // Thực hiện gọi vào DB để chạy procedure
                numberOfAffectedRows = mysqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            // Xử lý dữ liệu trả về
            if (numberOfAffectedRows > 0)
            {
                return recordID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="recordIDs">Danh sách ID các bản ghi cần xóa</param>
        /// <returns>Danh sách ID các bản ghi đã xóa</returns>
        public List<string> DeleteMultiRecords(List<string> recordIDs)
        {
            // Chuẩn bị tham số đầu vào cho procedure
            var properties = typeof(T).GetProperties();
            var propertyName = "";
            foreach (var property in properties)
            {
                var primaryKeyAttribute = (PrimaryKeyAttribute)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));
                if (primaryKeyAttribute != null)
                {
                    propertyName = property.Name;
                    break;
                }
            }

            // Khởi tạo kết nối tới DB MySQL
            string connectionString = DataContext.MySqlConnectionString;
            int numberOfAffectedRows = 0;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                // Khai báo tên prodecure
                string storedProcedureName = String.Format(Resource.Proc_BatchDelete, typeof(T).Name);

                mysqlConnection.Open();

                // Sử dụng transaction
                using (var transaction = mysqlConnection.BeginTransaction())
                {
                    foreach (string recordID in recordIDs)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add($"d_{propertyName}", recordID);
                        numberOfAffectedRows += mysqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);
                    }

                    if (numberOfAffectedRows == recordIDs.Count)
                    {
                        transaction.Commit();
                        return recordIDs;
                    }
                    else
                    {
                        transaction.Rollback();
                        return new List<string>();
                    }
                }
            }
        }

        /// <summary>
        /// Kiểm tra trùng mã bản ghi
        /// </summary>
        /// <param name="recordCode"></param>
        /// <param name="recordID"></param>
        /// <returns>Mã bản ghi bị trùng</returns>
        public string DuplicateRecordCode(object recordCode, Guid recordID)
        {
            // Khai báo tên prodecure
            string storedProcedureName = String.Format(Resource.Proc_DuplicateCode, typeof(T).Name);

            // Chuẩn bị tham số đầu vào cho procedure
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var isNotDuplicateAttribute = (IsNotDuplicateAttribute)Attribute.GetCustomAttribute(property, typeof(IsNotDuplicateAttribute));
                if (isNotDuplicateAttribute != null)
                {
                    parameters.Add($"d_{property.Name}", recordCode);
                }

                var primaryKeyAttribute = (PrimaryKeyAttribute)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));
                if (primaryKeyAttribute != null)
                {
                    parameters.Add($"d_{property.Name}", recordID);
                }
            }

            // Khởi tạo kết nối tới DB MySQL
            string connectionString = DataContext.MySqlConnectionString;
            string duplicates = "";
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                duplicates = mysqlConnection.QueryFirstOrDefault<string>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return duplicates;
        }

        #endregion
    }
}
