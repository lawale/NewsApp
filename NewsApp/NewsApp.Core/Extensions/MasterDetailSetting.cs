using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Core.Extensions
{
    public enum MasterDetailSetting
    {
        /// <summary>
        /// Closes the MasterPage after Navigation
        /// </summary>
        CloseMasterAfterNavigation,
        /// <summary>
        /// Leaves the MasterPage open after Navigation
        /// </summary>
        LeaveMasterOpenAfterNavigation
    }
}
