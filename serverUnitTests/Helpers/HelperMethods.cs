using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace serverUnitTests.Helpers
{
  public class HelperMethods
  {
    public static T GetObjectResultContent<T>(ActionResult<T> result)
    {
      return (T)((ObjectResult)result.Result!).Value!;
    }
  }
}