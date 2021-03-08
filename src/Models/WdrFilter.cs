namespace WdrFilterConverter.Models
{
    using System.Collections.Generic;

    public abstract class WdrFilter : Dictionary<string, object>
    {
        public virtual string Type { get; set; }
    }
}