//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VideoStore
{
    using System;
    using System.Collections.Generic;
    
    public partial class VideoOrder
    {
        public int VideoOrderId { get; set; }
        public int VideoId { get; set; }
        public int OrderId { get; set; }
    
        public virtual Video Video { get; set; }
    }
}
