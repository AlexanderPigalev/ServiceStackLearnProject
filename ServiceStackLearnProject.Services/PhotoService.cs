using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using ServiceStack;
using ServiceStack.Web;

namespace ServiceStackLearnProject.Services
{
    /// <summary>
    /// Сервис работы с фотографиями
    /// </summary>
    public class PhotoService
    {
        /// <summary>
        /// Сохраняет фото в файл на сервере
        /// </summary>
        /// <param name="photoFile">Файл, отправленный в запросе для сохранения</param>
        /// <param name="pathToSave">Название файла</param>
        public static void Save(IHttpFile photoFile, string fileName)
        {
            if (!Directory.Exists("pictures"))
            {
                Directory.CreateDirectory("pictures");
            }
            
            #region Проверка на пустоту

            _ = photoFile ?? throw new ArgumentNullException(nameof(photoFile));
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            #endregion

            try
            {
                using (var file = File.Create("pictures\\" + fileName))
                {
                    photoFile.WriteTo(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static bool Exist(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return File.Exists("pictures\\" + fileName);
        }

        /// <summary>
        /// Получает путь, который не занят другой фоткой
        /// </summary>
        /// <returns>Название файла</returns>
        public static string GetFreeFileName()
        {
            if (!Directory.Exists("pictures"))
            {
                Directory.CreateDirectory("pictures");
            }

            string fileName = RandomString(10) + ".png";
            while (true)
            {
                if (File.Exists("pictures\\" + fileName))
                {
                    fileName = RandomString(10);
                }
                else
                {
                    break;
                }
            }

            return fileName;
        }

        /// <summary>
        /// Создание строки с рандомным набором символов
        /// </summary>
        /// <param name="length">Длина строки</param>
        /// <returns>Строка с рандомным набором символов</returns>
        private static string RandomString(int length)
        {
            if (length < 1)
            {
                throw new ArgumentNullException("length не может быть меньше 1", nameof(length));
            }
            //Символы из которых будет создаваться строка
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
