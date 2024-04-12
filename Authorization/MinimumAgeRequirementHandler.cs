using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace RestaurantAPI.Authorization
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> logger;
        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
        {
            this.logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c =>c.Type == "DateOfBirth").Value);

            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            logger.LogInformation($"Użytkownik: {userEmail} urodzony: [{dateOfBirth}].");

            if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                logger.LogInformation("Autoryzacja zakończona sukcesem.");
                context.Succeed(requirement);
            }
            else
            {
                logger.LogInformation("Autoryzacja nieudana.");
            }

            return Task.CompletedTask;
        }
    }
}
