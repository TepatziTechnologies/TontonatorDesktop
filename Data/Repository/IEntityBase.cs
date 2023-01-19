using System;
using System.Collections.Generic;

namespace TontonatorDesktopApp.Data.Repository
{
    public interface IEntityBase
    {
        /// <summary>
        /// This property represents the generic Id property for all models. This is used as identifier in the database. 
        /// </summary>
        string Id { get; set; }

        Dictionary<string, object> ToDictionary();
    }
}

