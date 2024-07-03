using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turbo.Database.Attributes;
using Turbo.Database.Entities;
using Turbo.Database.Entities.Furniture;

namespace TurboWiredPlugin.Entities
{
    [TurboEntity, Table("furniture_wired_data"), Index(nameof(FurnitureEntityId), IsUnique = true)]
    public class FurnitureWiredDataEntity : Entity
    {
        [Column("furniture_id")]
        public int FurnitureEntityId { get; set; }

        [Column("wired_data")]
        public string? WiredData { get; set; }

        [ForeignKey(nameof(FurnitureEntityId))]
        public FurnitureEntity FurnitureEntity { get; set; }
    }
}
