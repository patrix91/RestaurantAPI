using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccesor)
        {
            this.httpContextAccessor = httpContextAccesor;
        }

        public ClaimsPrincipal User => this.httpContextAccessor.HttpContext?.User;

        public int? GetUserId => User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
