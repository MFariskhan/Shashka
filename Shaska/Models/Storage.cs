using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Shaska.Models
{
    public class Storage
    {
        public void SaveUploadedFile(string path,
                                     HttpPostedFileBase sourceFile)
        {
            string name = Path.GetFileName(sourceFile.FileName);
            sourceFile.SaveAs(path + @"\" + name);
        }
    }
}