using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Exceptions;

public class BusinessException(string message) : global::System.Exception(message)
{
}
