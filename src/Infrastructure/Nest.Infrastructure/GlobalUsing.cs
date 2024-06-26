﻿global using Azure.Storage.Blobs;
global using Azure.Storage.Blobs.Models;
global using MailKit;
global using MailKit.Net.Imap;
global using MailKit.Net.Smtp;
global using MailKit.Search;
global using MailKit.Security;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using MimeKit;
global using Nest.Application.Abstractions.Services;
global using Nest.Application.Abstractions.Storage;
global using Nest.Application.Abstractions.Storage.Azure;
global using Nest.Application.Abstractions.Storage.Local;
global using Nest.Application.Abstractions.TokenHandlerService;
global using Nest.Application.Consts;
global using Nest.Application.DTOs;
global using Nest.Application.DTOs.AuthDTOs;
global using Nest.Application.DTOs.Mail;
global using Nest.Application.Exceptions;
global using Nest.Application.Helper;
global using Nest.Domain.Entities.Identity;
global using Nest.Infrastructure.Services.MailSender;
global using Nest.Infrastructure.Services.Storage;
global using Nest.Infrastructure.Services.TokenHandlerService;
global using Nest.Infrastructure.Settings;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;