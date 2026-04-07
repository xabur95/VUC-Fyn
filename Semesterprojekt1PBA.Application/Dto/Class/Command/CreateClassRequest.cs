using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Application.Dto.Class.Command;
    public record CreateClassRequest(
        string Title,
        DateOnly StartDate,
        DateOnly EndDate,
        Guid SchoolId
        );
 
