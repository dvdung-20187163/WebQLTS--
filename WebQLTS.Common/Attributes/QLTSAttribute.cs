namespace WebQLTS.Common.Attributes
{
    public class QLTSAttribute
    {
        /// <summary>
        /// Atribute dùng để xác định khóa chính
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class PrimaryKeyAttribute : Attribute
        {

        }

        [AttributeUsage(AttributeTargets.Property)]
        public class IsNotNullOrEmptyAttribute : Attribute
        {
            #region Field

            /// <summary>
            /// Message lỗi trả về cho client
            /// </summary>
            public string ErrorMessage;

            #endregion

            #region Constructor
            
            /// <summary>
            /// Attribute dùng để xác định một property không được để trống 
            /// </summary>
            /// <param name="errorMessage"></param>
            public IsNotNullOrEmptyAttribute(string errorMessage)
            {
                ErrorMessage = errorMessage;
            } 
            #endregion

        }

        [AttributeUsage(AttributeTargets.Property)]
        public class IsNotDuplicateAttribute : Attribute
        {
            #region Field

            /// <summary>
            /// Message lỗi trả về cho client
            /// </summary>
            public string ErrorMessage;

            #endregion

            #region Constructor

            /// <summary>
            /// Attribute dùng để xác định mã code không được trùng 
            /// </summary>
            /// <param name="errorMessage"></param>
            public IsNotDuplicateAttribute(string errorMessage)
            {
                ErrorMessage = errorMessage;
            }
            #endregion

        }


    }
}
