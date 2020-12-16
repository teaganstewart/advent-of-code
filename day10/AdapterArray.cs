using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// </summary>
    class CustomCustoms
    {
        static void Main(string[] args)
        {
            CustomCustoms c = new CustomCustoms();
            c.ReadInputFile();
            c.ProcessYesAnswers();
            c.ProcessIntersectAnswer();
        }
    }
}
