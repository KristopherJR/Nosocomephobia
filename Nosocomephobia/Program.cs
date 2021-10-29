using System;

namespace Nosocomephobia
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Kernel())
                game.Run();
        }
    }
}
