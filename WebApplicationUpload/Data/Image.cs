using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationUpload.Data
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
