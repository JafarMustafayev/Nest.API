﻿namespace Nest.Application.DTOs.AuthDTOs;

public class ConfirmEmailDTO
{
    public string UserId { get; set; }

    public string Token { get; set; }
}