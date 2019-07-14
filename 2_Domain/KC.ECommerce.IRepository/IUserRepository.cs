using KC.ECommerce.Domain;
using KC.ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KC.ECommerce.IRepository
{
    public interface IUserRepository: IBaseRepository<User,int>
    {
    }
}
