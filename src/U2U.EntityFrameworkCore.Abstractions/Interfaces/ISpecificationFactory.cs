using System;
using System.Collections.Generic;
using System.Text;

namespace U2U.EntityFrameworkCore.Abstractions.Interfaces
{
  public interface ISpecificationFactory
  {
    bool ForType(Type t);
  }
}
