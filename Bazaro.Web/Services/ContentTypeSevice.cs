using Bazaro.Web.Services.Commands.ContentTypes;
using Bazaro.Web.Services.Queries.ContentTypes;
using Bazaro.Web.Services.ViewModels;

namespace Bazaro.Web.Services
{
    public class ContentTypeSevice
    {
        public readonly BazaroContext _context;

        public ContentTypeSevice(BazaroContext context)
        {
            _context = context;
        }

        public Task<List<ContentTypeModel>> GetContentTypes(GetContentTypes.Query request) => Queries.ContentTypes.GetContentTypes.Handle(_context, request);

        public Task Insert(InsertContentType.Command request) => InsertContentType.Handle(_context, request);
        public Task Update(UpdateContentType.Command request) => UpdateContentType.Handle(_context, request);
        public Task Delete(DeleteContentType.Command request) => DeleteContentType.Handle(_context, request);
    }
}
