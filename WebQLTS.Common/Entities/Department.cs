using static WebQLTS.Common.Attributes.QLTSAttribute;

namespace WebQLTS.Common.Entities
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// ID phòng ban
        /// </summary>
        [PrimaryKey]
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string department_name { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Có phải lớp cha không
        /// </summary>
        public string is_parent { get; set; }

    }
}
