using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExamenFinal
{
    [Table("CL_DEPARTAMENTOS")]
    public partial class ClDepartamento
    {
        public ClDepartamento()
        {
            ClEmpleados = new HashSet<ClEmpleado>();
        }

        [Key]
        [Column("ID_DPTO")]
        public int IdDpto { get; set; }
        [Column("NOMBRE_DPTO")]
        [StringLength(100)]
        [Unicode(false)]
        public string? NombreDpto { get; set; }
        [Column("ID_GERENTE")]
        public int? IdGerente { get; set; }
        [Column("ID_LOCALIDAD")]
        public int? IdLocalidad { get; set; }

        [InverseProperty("IdDptoNavigation")]
        public virtual ICollection<ClEmpleado> ClEmpleados { get; set; }
    }
}
