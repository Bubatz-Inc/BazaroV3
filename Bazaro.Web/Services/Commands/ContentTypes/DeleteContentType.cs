using Bazaro.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.ContentTypes
{
    public static class DeleteContentType
    {
        public class Command
        {
            public int Id { get; set; }
        }

        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = await context.Set<ContentType>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (data == null)
                return;

            context.Remove(data);

            await context.SaveChangesAsync();
        }
    }
}
