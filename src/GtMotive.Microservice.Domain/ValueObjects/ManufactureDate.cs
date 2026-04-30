using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Domain.ValueObjects
{
    public sealed class ManufactureDate
    {
        public DateTime Value { get; }

        public ManufactureDate(DateTime value)
        {
            if (value > DateTime.UtcNow)
                throw new InvalidOperationException("Manufacture date cannot be in the future.");

            if (value < DateTime.UtcNow.AddYears(-5))
                throw new InvalidOperationException("Vehicles older than 5 years are not permitted.");

            Value = value;
        }

        // Constructor privado — sin validación, solo para reconstitución desde la BD
        private ManufactureDate(DateTime value, bool fromPersistence)
        {
            Value = value;
        }

        // Factory method que EF usa internamente via la conversión
        public static ManufactureDate FromPersistence(DateTime value)
            => new ManufactureDate(value, fromPersistence: true);

        public static implicit operator DateTime(ManufactureDate date) => date.Value; // para no escribir .value cada vez que se use

        public override string ToString() => Value.ToString("yyyy-MM-dd");

        public override bool Equals(object? obj)
            => obj is ManufactureDate other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();

    }
}
