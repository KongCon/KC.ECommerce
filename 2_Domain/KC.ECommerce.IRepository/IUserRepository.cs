using KC.ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace KC.ECommerce.IRepository
{
    public interface IUserRepository: IBaseRepository<User,int>
    {
    }
}
