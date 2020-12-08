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
    public class Vehicle
    {
        public static string DisplayPoliceCar()
        {
            return @"
                      (<$$$$$$>#####<::::::>)
                   _/~~~~~~~~~~~~~~~~~~~~~~~~~\_   
                 /~                             ~\ 
               .~                                 ~. 
           ()\/_____                           _____\/()       
          .-''      ~~~~~~~~~~~~~~~~~~~~~~~~~~~     ``-.  
        -~              __________________              ~-.  
       `~~/~~~~~~~~~~~~TTTTTTTTTTTTTTTTTTTT~~~~~~~~~~~~\~~'   
       | | | #### #### || | | | [] | | | || #### #### | | | 
       ;__\|___________|++++++++++++++++++|___________|/__;
        (~~====___________________________________====~~~)
         \------_____________[ POLICE ]__________-------/ 
            |      ||         ~~~~~~~~       ||      |
             \_____/                          \_____/
             ";
        }

        public static string DisplayVan()
        {
            return @"  _________________
 |              |   \
 |  JUST-A-VAN  |____\_____
 | _____        |    |_ __ |
 [/ ___ \       |   / ___ \|
[]_/.-.\_\______|__/_/.-.\_[]
   |(O)|             |(O)|
    '-'               '-'";
        }

        public static string DisplayAssociateRanWithCash()
        {
            return @"                       _________________
  { )_  <-            |              |   \
 |  <<\  _      <--   |  JUST-A-VAN  |____\_____
 ||\  \_//            | _____        |    |_ __ |
($)  // `-`    <--    [/ ___ \       |   / ___ \|
   \\                []_/.-.\_\______|__/_/.-.\_[]
   (/  <--              |(O)|             |(O)|
                         '-'               '-'";
        }

        public static string DisplayAssociateRanInFear()
        {
            return @"                       _________________
  { )_  <-            |              |   \
 |  <<\  _       <--  |  JUST-A-VAN  |____\_____
 ||\  \_//            | _____        |    |_ __ |
 -  // `-`    <--     [/ ___ \       |   / ___ \|
   \\                []_/.-.\_\______|__/_/.-.\_[]
   (/  <--              |(O)|             |(O)|
                         '-'               '-'";
        }
    }
}
