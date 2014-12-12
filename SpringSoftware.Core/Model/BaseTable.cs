﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringSoftware.Core.Model
{
    public class BaseTable
    {

        public virtual int Id { get; set; }

        [DisplayName("创建时间")]
        public virtual DateTime CreateDate { get; set; }
        [DisplayName("最后修改时间")]
        public virtual DateTime LastModifyDate { get; set; }
        [DisplayName("是否删除")]
        public virtual bool IsDelete { get; set; }
        [DisplayName("创建者")]
        public virtual string Creater { get; set; }
        [DisplayName("最后修改者")]
        public virtual string LastModifier { get; set; }
    }
}
