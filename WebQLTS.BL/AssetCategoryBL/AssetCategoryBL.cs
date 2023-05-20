using WebQLTS.Common.Entities;
using WebQLTS.DL;

namespace WebQLTS.BL
{
    public class AssetCategoryBL : BaseBL<AssetCategory>, IAssetCategoryBL
    {
        public AssetCategoryBL(IBaseDL<AssetCategory> baseDL) : base(baseDL)
        {
        }
    }
}
