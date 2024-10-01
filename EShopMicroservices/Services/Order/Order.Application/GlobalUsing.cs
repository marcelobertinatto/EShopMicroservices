global using BuildingBlocks.CQRS;
global using Order.Application.Data;
global using Order.Application.DTOs;
global using Order.Domain.ValueObjects;
global using Order.Application.Orders.Exceptions;
global using FluentValidation;
global using MediatR;
global using Microsoft.Extensions.Logging;
global using Order.Domain.Events;
global using Microsoft.EntityFrameworkCore;
global using Order.Application.Extensions;