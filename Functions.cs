using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetlistFullCRUD
{
    public class Functions
    {
        static AssetlistContext context = new AssetlistContext();

        public static void ClearDatabase()
        {
            context.RemoveRange(context.Assets);


            context.SaveChanges();
        }

       

        
    }
}
