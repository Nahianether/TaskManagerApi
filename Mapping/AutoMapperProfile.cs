using AutoMapper;
using TaskManagerApi.Dtos;
using TaskManagerApi.Models;

namespace TaskManagerApi.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User ↔ DTOs
        CreateMap<User, UserReadDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();

        // Task Mappings
        CreateMap<TaskItem, TaskReadDto>();
        CreateMap<TaskCreateDto, TaskItem>();
        CreateMap<TaskUpdateDto, TaskItem>();
    }
}
