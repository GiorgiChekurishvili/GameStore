using AutoMapper;
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<LoginUserDTO, User>();
            CreateMap<RegisterUserDTO, User>();
            CreateMap<CategoryUpdateDTO, Category>();
            CreateMap<Category, GameByCategoryRetrieveDTO>();
            CreateMap<Category,CategoriesRetrieveDTO>();
            CreateMap<Cart, CartRetrieveDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Game!.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Game!.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Game!.Price))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.Game!.ReleaseDate));
            CreateMap<CartCommandsDTO, Cart>();
            CreateMap<Game, GamesRetrieveDTO>();
            CreateMap<GameUploadUpdateDTO, Game>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => 
                src.CategoryIds!.Select(id => new GameCategory { CategoryId = id}).ToList()));
            CreateMap<Library, LibraryRetrieveDTO>();
            CreateMap<SystemRequirement, SystemRequirementsUploadUpdateDTO>();
            CreateMap<SystemRequirementsRetrieveDTO, SystemRequirement>();
            CreateMap<FIllBalanceTransactionDTO, User>();
            CreateMap<Transaction, TransactionRetrieveDTO>();
            CreateMap<WishlistUploadDTO, Wishlist>();
            CreateMap<Wishlist, WishlistRetrieveDTO>();

        }
    }
}
