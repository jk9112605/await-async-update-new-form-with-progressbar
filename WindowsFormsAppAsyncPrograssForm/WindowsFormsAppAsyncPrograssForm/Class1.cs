using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppAsyncPrograssForm
{
    class Class1
    {
        public async Task MyMethodAsync(IProgress<int> progress = null)
        {
            bool done = false;
            int percentComplete = 0;
            while (!done)
            {
                
                percentComplete++;
                progress?.Report(percentComplete);
                if (percentComplete >= 100)
                {
                    done = true;
                }
                await Task.Delay(100);
            }
        }
    }
}
