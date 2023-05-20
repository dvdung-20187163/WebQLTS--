﻿using WebQLTS.Common.Resources;

namespace WebQLTS.Common.Entities
{
    /// <summary>
    /// Lớp cơ sở
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Người tạo
        /// </summary>
        public string? created_by { get; set; } = Resource.DefaultUser;

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? created_date { get; set; } = DateTime.Now;

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string? modified_by { get; set; } = Resource.DefaultUser;

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime? modified_date { get; set; } = DateTime.Now;
    }
}
