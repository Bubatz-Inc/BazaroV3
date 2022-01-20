using Bazaro.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.Items
{
    // TODO: Verschieben implementieren
    public static class UpdateItem
    {
        public class Command
        {
            public int Id { get; set; }

            public int ContentTypeId { get; set; }
            public byte[] Content { get; set; }

            // TODO: Props für verschieben hinzufügen
        }

        public static async Task Handler(BazaroContext context, Command request)
        {
            var data = await context.Set<Item>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (data == null)
                return;

            data.ContentTypeId = request.ContentTypeId;
            data.Content = request.Content;

            await context.SaveChangesAsync();
        }
    }
}
