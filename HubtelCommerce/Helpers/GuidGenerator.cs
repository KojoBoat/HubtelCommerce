using System;
namespace HubtelCommerce.Helpers
{
	public class GuidGenerator : IGuidGenerator
	{
        public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

