using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEIIS.Models
{
    public interface IControllerInformationRepository
    {
        List<ControllerInfo> GetAll();
    }
}
