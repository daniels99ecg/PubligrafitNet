using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoPubligrafit.Models
{
    public class Compras
    {
        [Key] 
    public int id_compra { get; set; }
    public string? proveedor { get; set; }
    public int? cantidad { get; set; } 
    public DateTime fecha { get; set; }
    public decimal? total { get; set; }
    public bool? estado { get; set; }
    }
}      
