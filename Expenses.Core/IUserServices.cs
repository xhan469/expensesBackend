using System;
using System.Threading.Tasks;
using Expenses.Core.DTO;
using Expenses.DB;

namespace Expenses.Core
{
	public interface IUserServices
	{
		Task<AuthenticatedUser> signUp(User user);
		Task<AuthenticatedUser> signIn(User user);
    }
}

