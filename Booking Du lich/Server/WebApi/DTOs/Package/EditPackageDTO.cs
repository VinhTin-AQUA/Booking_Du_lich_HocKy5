using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Package
{
    public class EditPackageDTO
    {
        public int PackageID { get; set; }
        public string PackageName { get; set; }     
        public string Decription { get; set; }    
        public int MaxPeople { get; set; }
    }
}
