#nullable enable
using System;

namespace InsureAI.Models
{
    public class Vehicle
    {
        // Non-nullable reference properties required at initialization
        public required string RegistrationNumber { get; init; }
        public required string Make { get; init; }
        public required string Model { get; init; }

        // Value types
        public int Year { get; init; }
        public decimal InsuredDeclaredValue { get; init; }
    }
}
