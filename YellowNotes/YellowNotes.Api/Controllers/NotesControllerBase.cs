using System.Collections.Concurrent;
using System.Security.Claims;
using System.Web.Http;
using YellowNotes.Dto;

namespace YellowNotes.Api.Controllers
{
    public abstract class NotesControllerBase : ApiController
    {
        private readonly ClaimsIdentity _claimsIdentity;

        protected NotesControllerBase()
        {
            _claimsIdentity = RequestContext.Principal.Identity as ClaimsIdentity;
        }

        protected static readonly ConcurrentDictionary<int, NoteDto> Notes =
            new ConcurrentDictionary<int, NoteDto>
            {
                [1] = new NoteDto {Id = 1, Title = "Title 1", Content = "Content 1"},
                [2] = new NoteDto {Id = 2, Title = "Title 2", Content = "Content 2"},
            };

        protected string Device => _claimsIdentity.IsAuthenticated
            ? _claimsIdentity.FindFirst(ApiConstants.ClaimDevice).Value
            : string.Empty;
    }
}