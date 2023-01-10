using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsNeuralInfrastructure.Entities
{
    public class User : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string Username { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Role { get; set; }
    }
}