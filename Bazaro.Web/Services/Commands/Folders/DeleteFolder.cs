﻿using Bazaro.Web.Models.References;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.Folders
{
    public static class DeleteFolder
    {
        public class Command
        {
            public int Id { get; set; }
        }

        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = await context.Set<UserFolderReference>().Where(x => x.FolderId == request.Id).ToListAsync();

            if (data == null)
                return;

            foreach (var item in data)
            {
                item.IsDeleted = true;
            }

            await context.SaveChangesAsync();
        }
    }
}
