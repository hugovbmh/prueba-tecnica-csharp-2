using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExamenFinal
{
    [Table("CL_EMPLEADOS")]
    public partial class ClEmpleado
    {
        [Key]
        [Column("ID_EMPLEADO")]
        public int IdEmpleado { get; set; }
        [Column("NOMBRES")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Nombres { get; set; }
        [Column("APELLIDOS")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Apellidos { get; set; }
        [Column("EMAIL")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("TELEFONO")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Telefono { get; set; }
        [Column("FECHA_INGRESO", TypeName = "date")]
        public DateTime? FechaIngreso { get; set; }
        [Column("ID_CARGOS")]
        [StringLength(50)]
        [Unicode(false)]
        public string? IdCargos { get; set; }
        [Column("SUELDO")]
        public int? Sueldo { get; set; }
        [Column("PCT_COMISION", TypeName = "decimal(16, 2)")]
        public decimal? PctComision { get; set; }
        [Column("ID_GERENTE")]
        public int? IdGerente { get; set; }
        [Column("ID_DPTO")]
        public int? IdDpto { get; set; }

        [ForeignKey("IdDpto")]
        [InverseProperty("ClEmpleados")]
        public virtual ClDepartamento? IdDptoNavigation { get; set; }
    }
}
