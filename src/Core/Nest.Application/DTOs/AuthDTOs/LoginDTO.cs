﻿namespace Nest.Application.DTOs.AuthDTOs;

public class LoginDTO
{
    public string? EmailOrUsername { get; set; }

    public string? Password { get; set; }
}