using Bazaro.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.ContentTypes
{
    public class UpdateContentType
    {
        public class Command
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }

        /// <summary>
        /// Updates ContentType
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns></returns>
        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = await context.Set<ContentType>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (data == null)
                return;

            data.Title = request.Title;
            data.Updated = DateTime.Now;
            
            await context.SaveChangesAsync();
        }
    }
}
