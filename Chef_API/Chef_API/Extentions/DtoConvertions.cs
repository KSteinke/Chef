using Chef_API.Entities;
using Chef_Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.IO;
using System.Net.Mime;

namespace Chef_API.Extentions
{
    public static class DtoConvertions
    {
        
        /// <summary>
        /// Converts IEnumerable that cointains Recipe objects to IEnumerable that contains RecipeDto objects
        /// </summary>
        /// <param name="recipies">IEnumerable list of Recipe objects</param>
        /// <returns></returns>
        public static IEnumerable<RecipeDto> ConvertoToDto(this IEnumerable<Recipe> recipies)
        {
            //IEnumerable<RecipeDto> recipesDto = new List<RecipeDto>();


            IEnumerable<RecipeDto> recipesDto =  (from recipe in recipies
                    select new RecipeDto
                    {
                        Id = recipe.Id,
                        Name = recipe.Name,
                        Description = recipe.Description,
                        Category = recipe.Category,
                        LunchBox = recipe.LunchBox,
                        Diet_Category = recipe.Diet_Category,
                        RecipePhotoURL = recipe.RecipePhotoURL,
                        Author = recipe.Category
                    }).ToList();
            foreach (RecipeDto recipe in recipesDto)
            {
                //recipe.RecipeImg = ReadImg(recipe.RecipePhotoURL);
                //recipe.RecipeImg = ReadImgMultipart(recipe.RecipePhotoURL);
                recipe.RecipeImgBase64 = ReadImgByte(recipe.RecipePhotoURL);
            }
            return recipesDto;
            
        }

        public static IFormFile ReadImg(string imgUrl)
        {
            try
            {
                string recipeImgPath = Path.Combine(Config.RecipeImgPath, imgUrl);

                using (var fileStream = new FileStream(recipeImgPath, FileMode.Open, FileAccess.Read))
                {
                    // Create a MemoryStream to hold the file data
                    var memoryStream = new MemoryStream();

                    // Copy the file data from FileStream to MemoryStream
                    fileStream.CopyTo(memoryStream);

                    // Create a FormFile from the MemoryStream
                    var formFile = new FormFile(memoryStream, 0, memoryStream.Length, null, Path.GetFileName(imgUrl))
                    {
                        Headers = new HeaderDictionary(),   //Important to find why it works
                        ContentType = "image/*"
                    };

                    // Now you can use the formFile as needed
                    // For example, you can pass it to a method that requires a FormFile parameter
                    
                    // Remember to dispose the MemoryStream when done
                    
                    
                    memoryStream.Dispose();
                    return formFile;
                }
                

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static MultipartFormDataContent ReadImgMultipart(string imgUrl)
        {
            try
            {
                string recipeImgPath = Path.Combine(Config.RecipeImgPath, imgUrl);

                using (var fileStream = new FileStream(recipeImgPath, FileMode.Open, FileAccess.Read))
                {
                    //using (var content = new MultipartFormDataContent())
                    //{
                        var fileName = Path.GetFileName(recipeImgPath);

                    HttpContent httpContent = new StreamContent(fileStream);
                    

                    var content = new MultipartFormDataContent();
                    
                    //content.Add(new StreamContent(fileStream), "file", fileName);
                    content.Add(
                    content: httpContent,
                    name: "\"files\"",
                    fileName: fileName);

                    return content;
                        
                    //}
                    
                }


            }
            catch (Exception)
            {

                throw;
            }

        }


        public static string ReadImgByte(string imgUrl)
        {
            try
            {
                string recipeImgPath = Path.Combine(Config.RecipeImgPath, imgUrl);
                byte[] recipeImgBytes = File.ReadAllBytes(recipeImgPath);
                string recipeImgBase64 = Convert.ToBase64String(recipeImgBytes, 0, recipeImgBytes.Length);
                //File.WriteAllBytes("D:\\Programowanie\\Chef\\Images_Tests\\Test.jpg", Convert.FromBase64String(recipeImgBase64));
                return recipeImgBase64;

            }
            catch (Exception)
            {

                throw;
            }

        }



    }
}
