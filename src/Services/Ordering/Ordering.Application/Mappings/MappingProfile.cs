﻿using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, CreateOrderCommand>().ReverseMap();
        }
    }
}
