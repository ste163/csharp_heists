//ASCII CREDITS(I TAKE NO CREDIT FOR ANY ARTWORK.I did modify most artwork to fit the screen better)
    // https://textfancy.com/multiline-text-art/
    // https://asciiart.website/
    // https://asciiart.website/index.php?art=people/faces
    // https://simpletextart.blogspot.com/2013/12/money-bag-picture-made-with-ascii-text.html
    // https://www.asciiart.eu/buildings-and-places/cities
    // http://www.asciiworld.com/-Buildings-.html
using System;

namespace CSharpHeists.ASCII
{
    public class Misc
    {
        public static string DisplayMoney()
        {
            return @"--------------------------------------------------------------------.
| .--                        MONEY MONEY                         .-- |
| |_       ......     DEFINITELY NOT MARKED BILLS                |_  |
| __)    ``````````             ______                           __) |
|               ___            /      \                              |
|              /|~\\          /  _-\\  \           __ _ _ _  __      |
|             | |-< |        |  //   \  |         |_  | | | |_       |
|              \|_//         | |-  X X| |         |   | `.' |__      |
|               ~~~          | |\   b.' |                            |
|                            |  \ '~~|  |                            |
| .--                         \_/ ```__/    ....                 .-- |
| |_        ///// ///// ////   \__\'`\/      ``  //// / ////     |_  |
| __)                   M A J O R  D O L L A R S                 __) |
`--------------------------------------------------------------------'
";
        }

        public static string DisplayGun()
        {
            return @"
          ^
         | |
       @#####@
     (###   ###)-.
   .(###     ###) \
  /  (###   ###)   )
 (=-  .@#####@|_--
 /\    \_|l|_/ (\
(=-\     |l|    /
 \  \.___|l|___/
 /\      |_|   /
(=-\._________/\
 \             /
   \._________/
     #  ----  #
     #   __   #
     \########/
     ";
        }

        public static string DisplayMoneyBag()
        {
            return @"                     ██
                   ████
                 ██████
                 ▌─────▌
                 ███─█████
             ███████─────██████
          ███████─────────██████
        █████████───██─██████████
       ███████████──────────██████
       ████████████─────█───███████
      █████████████████─██───██████
     ██████████████████──██──███████
    ████████████████████─██───██████
    ████████████████████──────███████
     ██████████████──────────███████
        ███████████───────████████
            ███████████──██████";
        }
    }
}
