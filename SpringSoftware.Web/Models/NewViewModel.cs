﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpringSoftware.Web.Models
{
    public class NewViewModel : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
     
        public NewViewModel() : base("name=NewViewModel")
        {
        }
         
        public System.Data.Entity.DbSet<SpringSoftware.Core.DbModel.News> News { get; set; }
    
    }
}