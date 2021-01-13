using System;
using System.IO;

namespace PASS4
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Main())
                game.Run();
          
        }
    }
}
