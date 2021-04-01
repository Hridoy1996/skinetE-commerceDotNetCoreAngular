﻿using AspNetCore.Identity.MongoDbCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Identity
{
	public class ApplicationRole : MongoIdentityRole<Guid>
	{
		public ApplicationRole() : base()
		{
		}

		public ApplicationRole(string roleName) : base(roleName)
		{
		}
	}
}
