using WebQLTS.Common.Entities;
using WebQLTS.DL;


namespace WebQLTS.BL
{
    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {
        #region Field

        private IDepartmentDL _departmentDL;

        #endregion

        #region Constructor

        public DepartmentBL(IDepartmentDL departmentDL) : base(departmentDL)
        {
        }

        #endregion
        
        /*public IEnumerable<Department> FilterDepartment(string? name, string? code)
        {
            return _departmentDL.FilterDepartment(name, code);
        }*/

    }
}
