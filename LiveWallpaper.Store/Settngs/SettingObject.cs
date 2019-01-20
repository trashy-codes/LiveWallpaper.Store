using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveWallpaper.Store.Settngs
{
    public class SettingObject
    {
        public GeneralSetting General { get; set; } = new GeneralSetting();

        internal static SettingObject GetDefaultSetting()
        {
            var result = new SettingObject
            {
                General = GeneralSetting.GetDefault()
            };
            return result;
        }

        //检查是否有配置需要重新生成
        public void CheckDefaultSetting()
        {
            if (General == null)
                General = GeneralSetting.GetDefault();
        }
    }
}
