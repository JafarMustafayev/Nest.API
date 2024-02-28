using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Persistence.MapperProfile;

public class AppUserMapper : Profile
{
    public AppUserMapper()
    {
        CreateMap<AppUser, RegisterDTO>().ReverseMap();
    }
}