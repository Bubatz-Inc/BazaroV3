using Bazaro.Web.Models;

namespace Bazaro.Web.Services.Commands.ContentTypes
{
    public static class InsertContentType
    {
        public class Command
        {
            public string Title { get; set; }
        }

        /// <summary>
        /// Inserts ContentType
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns></returns>
        public static Task Handle(BazaroContext context, Command request)
        {
            context.Add(new ContentType
            {
                Title = request.Title,
                Created = DateTime.Now
            });

            return context.SaveChangesAsync();  
        }
    }
}
