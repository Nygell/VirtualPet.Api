using System;

namespace VirtualPetBackend.DTOs;

public class UpdatePetDTO
{
    public int Age { get; set; }
    public int Hunger { get; set; }
    public int Happiness { get; set; }
}
