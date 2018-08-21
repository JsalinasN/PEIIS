using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NimBaseNetCore20.Models
{
    public interface IControllerInformationRepository
    {
        List<ControllerInfo> GetAll();
    }
}
