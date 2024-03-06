using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HubtelCommerce.Helpers
{
	public class UserIdHelper
	{
		private readonly IHttpContextAccessor _accessor;

		public UserIdHelper(IHttpContextAccessor accessor)
		{
			_accessor = accessor;
		}

		public string GetCustomerId()
		{
			return _accessor.HttpContext!.User.FindFirst(ClaimTypes.PrimarySid)!.Value;
		}
	}
}

