using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDbGenericRepository.Attributes;

namespace Core.Entities.Identity
{
	[CollectionName("Users")]
	public class ApplicationUser : MongoIdentityUser<Guid>
	{
		

		public ApplicationUser() : base()
		{
		}

		public ApplicationUser(string userName, string email) : base(userName, email)
		{
		}
		public ApplicationUser(string userName, string email, string displayName) : base(userName, email)
		{
			this.DisplayName = displayName;
		}
		public string DisplayName { get; set; }
		
	}

}
