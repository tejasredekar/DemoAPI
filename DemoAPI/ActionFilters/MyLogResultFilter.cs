using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace DemoAPI.ActionFilters
{
    public class MyLogResultFilter: IResultFilter
    {
       
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>

            public void OnResultExecuted(ResultExecutedContext context)
            {
                Debug.WriteLine(context.ActionDescriptor.DisplayName + " has ended");
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>

            public void OnResultExecuting(ResultExecutingContext context)
            {
                Debug.WriteLine(context.ActionDescriptor.DisplayName + " Was run");
            }
        }
    }


