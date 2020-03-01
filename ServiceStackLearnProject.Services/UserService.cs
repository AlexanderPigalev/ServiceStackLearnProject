using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using Mapster;
using ServiceStack;
using ServiceStackLearnProject.Data.Mappers.Interfaces;
using ServiceStackLearnProject.Enteties;
using ServiceStackLearnProject.Models;
using ServiceStackLearnProject.Services.Properties;

namespace ServiceStackLearnProject.Services
{
    public class UserService : Service
    {
        public IUserMapper UserMapper { get; set; }

        /// <summary>
        /// Запрос на получение списка всех пользователей
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <returns>Список зарегистрированных пользователей</returns>
        public object Any(GetUserList request)
        {
            return UserMapper.Get().ToJson();
        }

        /// <summary>
        /// Запрос на удаление пользователя
        /// </summary>
        /// <param name="request">Запрос с данными</param>
        /// <returns>Результат запроса</returns>
        public object Post(DeleteUser request)
        {
            if (string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.SecondName))
            {
                return new DeletedUserResponse
                {
                    Response = "Вы не указали имя или фамилию студента!"
                }.ToJson();
            }

            if (!UserMapper.Exist(request.FirstName, request.SecondName))
            {
                return new DeletedUserResponse
                {
                    Response = $"Студента {request.FirstName} {request.SecondName} не существует"
                }.ToJson();
            }

            UserMapper.DeleteUser(new User
            {
                FirstName = request.FirstName,
                SecondName = request.SecondName,
            });

            return new DeletedUserResponse() { Response = $"Студент {request.FirstName} {request.SecondName} был удалён." }.ToJson();
        }


        /// <summary>
        /// Запрос на создание пользователя
        /// </summary>
        /// <param name="request">Запрос с данными</param>
        /// <returns>Результат запроса</returns>
        public object Post(CreateUser request)
        {
            if (string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.SecondName))
            {
                return new CreatedUserResponse
                {
                    Response = "Вы не указали имя или фамилию студента!"
                }.ToJson();
            }

            if (UserMapper.Exist(request.FirstName, request.SecondName))
            {
                return new CreatedUserResponse { Response = "Такой студент уже существует!" }.ToJson();
            }

            string filename = PhotoService.GetFreeFileName();

            UserMapper.CreateUser(new User
            {
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                PhotoPath = Resources.LinkToPhotos + filename
            });

            PhotoService.Save(Request.Files[0], filename);

            return new CreatedUserResponse
            {
                Response = $"Студент {request.FirstName} {request.SecondName} был добавлен"
            }.ToJson();

        }
    }
}
