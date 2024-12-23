﻿using AutoMapper;
using GameStore.Application.DTOs.CartDTO;
using GameStore.Application.DTOs.CategoryDTO;
using GameStore.Application.DTOs.GameDTO;
using GameStore.Application.DTOs.LibraryDTO;
using GameStore.Application.DTOs.SystemRequirementsDTO;
using GameStore.Application.DTOs.TransactionDTO;
using GameStore.Application.DTOs.UserDTO;
using GameStore.Application.DTOs.WishlistDTO;
using GameStore.Domain.Entities;
using GameStore.Domain.Entities.Authentication;
using System;
using System.Linq;

namespace GameStore.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginUserDTO, User>();
            CreateMap<RegisterUserDTO, User>();
            CreateMap<CategoryUpdateDTO, Category>();
            CreateMap<Category, CategoriesRetrieveDTO>();
            CreateMap<Cart, CartRetrieveDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Game!.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Game!.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Game!.Price))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.Game!.ReleaseDate));
            CreateMap<CartCommandsDTO, Cart>();
            CreateMap<Game, GamesRetrieveDTO>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories!.Select(x => x.Category!.CategoryName).ToList()))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher!.UserName))
                .ForMember(dest => dest.Developer, opt => opt.MapFrom(src => src.Developer!.UserName));
            CreateMap<GameUploadUpdateDTO, Game>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
                src.CategoryIds!.Select(id => new GameCategory { CategoryId = id }).ToList()));
            CreateMap<Library, LibraryRetrieveDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Game!.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Game!.Description))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Game!.Categories!.Select(x=>x.Category!.CategoryName).ToList()));
            CreateMap<SystemRequirementsUploadUpdateDTO, SystemRequirement >();
            CreateMap<SystemRequirement, SystemRequirementsRetrieveDTO>();
            CreateMap<FIllBalanceTransactionDTO, User>();
            CreateMap<Transaction, TransactionRetrieveDTO>();
            CreateMap<WishlistUploadDTO, Wishlist>();
            CreateMap<Wishlist, WishlistRetrieveDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Game!.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Game!.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Game!.Price))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.Game!.ReleaseDate))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Game!.Publisher!.UserName))
                .ForMember(dest => dest.Developer, opt => opt.MapFrom(src => src.Game!.Developer!.UserName))
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Game!.Id))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Game!.Categories!.Select(x => x.Category!.CategoryName).ToList()));
            

        }
    }
}
