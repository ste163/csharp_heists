// ASCII CREDITS (I TAKE NO CREDIT FOR ANY ARTWORK. I did modify most artwork to fit the screen better)
    // https://textfancy.com/multiline-text-art/
    // https://asciiart.website/
    // https://asciiart.website/index.php?art=people/faces
    // https://simpletextart.blogspot.com/2013/12/money-bag-picture-made-with-ascii-text.html
    // https://www.asciiart.eu/buildings-and-places/cities
    // http://www.asciiworld.com/-Buildings-.html
using System;

namespace heist
{
    public class ASCII
    {

        public string DisplayWhoAreYou()
        {
            return @"
█   █ █▄█ ▄▀▄   ▄▀▄ █▀▄ ██▀   ▀▄▀ ▄▀▄ █ █
▀▄▀▄▀ █ █ ▀▄▀   █▀█ █▀▄ █▄▄    █  ▀▄▀ ▀▄█
-----------------------------------------";
        }

        public string DisplayCrewHeading()
        {
            return @"-------------------------------
▀█▀ █▄█ ██▀   ▄▀▀ █▀▄ ██▀ █   █
 █  █ █ █▄▄   ▀▄▄ █▀▄ █▄▄ ▀▄▀▄▀
-------------------------------";
        }
        public string DisplayCrewHire()
        {
            return @"---------------------------------
█▄█ █ █▀▄ ██▀   ▄▀▀ █▀▄ ██▀ █   █
█ █ █ █▀▄ █▄▄   ▀▄▄ █▀▄ █▄▄ ▀▄▀▄▀                   
---------------------------------";
        }

        public string DisplayPlanning()
        {
          return @"--------------------------------
█▀▄ █   ▄▀▄ █▄ █ █▄ █ █ █▄ █ ▄▀ 
█▀  █▄▄ █▀█ █ ▀█ █ ▀█ █ █ ▀█ ▀▄█
--------------------------------
";  
        }

        public string DisplayStakeout()
        {
            return @"-------------------------------
▄▀▀ ▀█▀ ▄▀▄ █▄▀ ██▀ ▄▀▄ █ █ ▀█▀
▄██  █  █▀█ █ █ █▄▄ ▀▄▀ ▀▄█  █ 
-------------------------------";
        }

        public string DisplayIce()
        {
            return @"-----------------------------------------------------------
█ ▄▀▀ ██▀   ▄▀▀ █▀▄ ██▀ █   █   █▄ ▄█ ██▀ █▄ ▄█ ██▄ ██▀ █▀▄
█ ▀▄▄ █▄▄   ▀▄▄ █▀▄ █▄▄ ▀▄▀▄▀   █ ▀ █ █▄▄ █ ▀ █ █▄█ █▄▄ █▀▄
-----------------------------------------------------------
            ";
        }

        public string DisplayCriminalFace(int r)
        {
            string face = "";

            switch(r)
            {
                case 1:
                    face = @"
  \\\\\\\|
 | _   _ |       
( (o) (o) )     
 |  . .  |           
  \  _  /         
   \___/";
                    break;
                case 2:
                    face = @"
  |||||   
{. @ @ .} 
 |  o  |        
  \_U_/";
                    break;
                case 3:
                    face = @"
  .xxxx. 
 |(o)(o)|
(  (__)  )
  | __ |
   \__/ ";
                    break;
                case 4:
                    face = @"
    _____
  /`---'- \
 (_    _ \_\
 `o'/ 'o` 7)
  \`-     |
   \`~'  /\
    `---'  ";
                    break;
                case 5:
                    face = @"
    ______
 /  `       \
| __   ~~~~ |
\   \__  -. |
( o( o)---/ /
/  /     -|/
\  =   /  |
(_____/   |";
                    break;
                case 6:
                    face = @"
   .-```'.
  /   \    \
 /   / `\__/
| .'  _  _|
\(\   6  6
 | \   _\ |
 |\   `= `/
   '.___.'";
                    break;
                case 7:
                    face = @"
      _.===.
   _,/_ \\\ `-.
  /`/| /      _\
 // ` /////////\
/`/` /|<o>)(o>( 
\  \/\|    /  /
\`/` \/\ `=' /
|/\/` \/`._.'";
                    break;
            }

            return face;
        }

        public string DisplayHeadingIced()
        {
            return @"

██╗ ██████╗███████╗██████╗ 
██║██╔════╝██╔════╝██╔══██╗
██║██║     █████╗  ██║  ██║
██║██║     ██╔══╝  ██║  ██║
██║╚██████╗███████╗██████╔╝
╚═╝ ╚═════╝╚══════╝╚═════╝ 
";
        }

        public string DisplayCriminalFaceIced(int r)
        {
            string face = "";

            switch(r)
            {
                case 1:
                    face = @"
  \\\\\\\|   
 | _   _/ *      
( (X)(X( *   * 
 |  . . \ *          
  \  o  /    *      
   \___/";
                    break;
                case 2:
                    face = @"
  |||||/*   
{. X X( * *
 |  o  \ *       
  \_O_/";
                    break;
                case 3:
                    face = @"
  .xxxx. 
 |(X)(X/*
(  (_( * *
  | O \ *   *
   \__/ ";
                    break;
                case 4:
                    face = @"
    _____
  /`---'- \
 (-    -  / *
 `X / X  ( *  * 
  \`-     \ * *
   \ O   /\     *
    `---'  ";
                    break;
                case 5:
                    face = @"
    ______
 /  `     /
| __   ~~/ * *
\   \__ (* **
( X( X)--\ *
/  /     |  *
\  O   / \     *
(_____/   \";
                    break;
                case 6:
                    face = @"
   .-```'.   *
  /     /* *
 /   /  \* *
| .'  _  \ *  * 
\(\   X  X    *
 | \   _\ |
 |\    O  /
   '.___.'";
                    break;
                case 7:
                    face = @"
      _.===.
   _,/_ \\\ `\
  /`/| /     /  *
 // ` //////(*  *
/`/` /| X  X(* *  
\  \/\|    / \* *
\`/` \/\  O  /    *
|/\/` \/`._.'";
                    break;
            }

            return face;
        }

        public string DisplayNashville()
        {

return @"

      \  |              |             _)  |  |       
       \ |  _` |   __|  __ \  \ \   /  |  |  |   _ \ 
     |\  | (   | \__ \  | | |  \ \ /   |  |  |   __/ 
    _| \_|\__._| ____/ _| |_|   \_/   _| _| _| \___|                                           

                       .|
                       | |
                       |'|            ._____
               ___    |  |            |.   |' .---|
       _    .-'   '-. |  |     .--'|  ||   | _|    |
    .-'|  _.|  |    ||   '-__  |   |  |    ||      |
    |' | |.    |    ||       | |   |  |    ||      |
 ___|  '-'     '    ||       '-'   '-.'    '`      |____
 ";
        }

        public string DisplayMoney()
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

        public string DisplayArrested()
        {
            return @"==============================
||     ||<(.)>||<(.)>||     || 
||    _||     ||     ||_    || 
||   (__D     ||     C__)   || 
||   (__D     ||     C__)   ||
||   (__D     ||     C__)   ||
||   (__D     ||     C__)   ||
||     ||     ||     ||     ||
==============================
";
        }

        public string DisplayPoliceCar()
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

        public string DisplayCrewCreated()
        {
            return @"

 ██████╗██████╗ ███████╗██╗    ██╗     ██████╗██████╗ ███████╗ █████╗ ████████╗███████╗██████╗ 
██╔════╝██╔══██╗██╔════╝██║    ██║    ██╔════╝██╔══██╗██╔════╝██╔══██╗╚══██╔══╝██╔════╝██╔══██╗
██║     ██████╔╝█████╗  ██║ █╗ ██║    ██║     ██████╔╝█████╗  ███████║   ██║   █████╗  ██║  ██║
██║     ██╔══██╗██╔══╝  ██║███╗██║    ██║     ██╔══██╗██╔══╝  ██╔══██║   ██║   ██╔══╝  ██║  ██║
╚██████╗██║  ██║███████╗╚███╔███╔╝    ╚██████╗██║  ██║███████╗██║  ██║   ██║   ███████╗██████╔╝
 ╚═════╝╚═╝  ╚═╝╚══════╝ ╚══╝╚══╝      ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝╚═════╝ 
";
        }

        public string DisplayHeistSuccess()
        {
            return @"

███████╗██╗   ██╗ ██████╗ ██████╗███████╗███████╗███████╗
██╔════╝██║   ██║██╔════╝██╔════╝██╔════╝██╔════╝██╔════╝
███████╗██║   ██║██║     ██║     █████╗  ███████╗███████╗
╚════██║██║   ██║██║     ██║     ██╔══╝  ╚════██║╚════██║
███████║╚██████╔╝╚██████╗╚██████╗███████╗███████║███████║
╚══════╝ ╚═════╝  ╚═════╝ ╚═════╝╚══════╝╚══════╝╚══════╝
";
        }

        public string DisplaySuccessOveriew()
        {
            return @"▄▀▄ █ █ ██▀ █▀▄ █ █ █ ██▀ █   █
▀▄▀ ▀▄▀ █▄▄ █▀▄ ▀▄▀ █ █▄▄ ▀▄▀▄▀
-------------------------------";
        }

        public string DisplayHeadingTraitor()
        {
            return @"

████████╗██████╗  █████╗ ██╗████████╗ ██████╗ ██████╗ 
╚══██╔══╝██╔══██╗██╔══██╗██║╚══██╔══╝██╔═══██╗██╔══██╗
   ██║   ██████╔╝███████║██║   ██║   ██║   ██║██████╔╝
   ██║   ██╔══██╗██╔══██║██║   ██║   ██║   ██║██╔══██╗
   ██║   ██║  ██║██║  ██║██║   ██║   ╚██████╔╝██║  ██║
   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝
";
        }

        public string DisplaySubheadingTraitor()
        {
            return @"▄▀▀ █▀▄ ██▀ █   █   █▄ ▄█ ██▀ █▄ ▄█ ██▄ ██▀ █▀▄   ▀█▀ █ █ █▀▄ █▄ █ ██▀ █▀▄   ▄▀▄ ▄▀  ▄▀▄ █ █▄ █ ▄▀▀ ▀█▀   █ █ ▄▀▀
▀▄▄ █▀▄ █▄▄ ▀▄▀▄▀   █ ▀ █ █▄▄ █ ▀ █ █▄█ █▄▄ █▀▄    █  ▀▄█ █▀▄ █ ▀█ █▄▄ █▄▀   █▀█ ▀▄█ █▀█ █ █ ▀█ ▄██  █    ▀▄█ ▄██
-----------------------------------------------------------------------------------------------------------------";
        }

        public string DisplayHeadingArrested()
        {
            return @"

█████╗ ██████╗ ██████╗ ███████╗███████╗████████╗███████╗██████╗ 
██╔══██╗██╔══██╗██╔══██╗██╔════╝██╔════╝╚══██╔══╝██╔════╝██╔══██╗
███████║██████╔╝██████╔╝█████╗  ███████╗   ██║   █████╗  ██║  ██║
██╔══██║██╔══██╗██╔══██╗██╔══╝  ╚════██║   ██║   ██╔══╝  ██║  ██║
██║  ██║██║  ██║██║  ██║███████╗███████║   ██║   ███████╗██████╔╝
╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚══════╝   ╚═╝   ╚══════╝╚═════╝ 
";
        }

        public string DisplayHeadingEscaped()
        {
            return @"
            
███████╗███████╗ ██████╗ █████╗ ██████╗ ███████╗██████╗ 
██╔════╝██╔════╝██╔════╝██╔══██╗██╔══██╗██╔════╝██╔══██╗
█████╗  ███████╗██║     ███████║██████╔╝█████╗  ██║  ██║
██╔══╝  ╚════██║██║     ██╔══██║██╔═══╝ ██╔══╝  ██║  ██║
███████╗███████║╚██████╗██║  ██║██║     ███████╗██████╔╝
╚══════╝╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝     ╚══════╝╚═════╝  
";
        }

        public string DisplayHeadingSplit()
        {
            return @"

███████╗██████╗ ██╗     ██╗████████╗     ██████╗ █████╗ ███████╗██╗  ██╗
██╔════╝██╔══██╗██║     ██║╚══██╔══╝    ██╔════╝██╔══██╗██╔════╝██║  ██║
███████╗██████╔╝██║     ██║   ██║       ██║     ███████║███████╗███████║
╚════██║██╔═══╝ ██║     ██║   ██║       ██║     ██╔══██║╚════██║██╔══██║
███████║██║     ███████╗██║   ██║       ╚██████╗██║  ██║███████║██║  ██║
╚══════╝╚═╝     ╚══════╝╚═╝   ╚═╝        ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝
";
        }

        public string DisplaySubheadingSplit()
        {
            return @"█▀▄ ▄▀▄ █▀▄ ▀█▀   █   █ ▄▀▄ ▀▄▀ ▄▀▀   ▄▀▄ █▀▄   ▄▀▄ █▀▄ ██▀ █▄ █   █▀ █ █▀▄ ██▀
█▀  █▀█ █▀▄  █    ▀▄▀▄▀ █▀█  █  ▄██   ▀▄▀ █▀▄   ▀▄▀ █▀  █▄▄ █ ▀█   █▀ █ █▀▄ █▄▄
-------------------------------------------------------------------------------";
        }

        public string DisplayHeadingGameOver()
        {
            return @"

 ██████╗  █████╗ ███╗   ███╗███████╗     ██████╗ ██╗   ██╗███████╗██████╗ 
██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔═══██╗██║   ██║██╔════╝██╔══██╗
██║  ███╗███████║██╔████╔██║█████╗      ██║   ██║██║   ██║█████╗  ██████╔╝
██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗
╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║
 ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝     ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝
";
        }

        public string DisplaySubHeadingSummary()
        {
            return @"▄▀▀ █ █ █▄ ▄█ █▄ ▄█ ▄▀▄ █▀▄ ▀▄▀
▄██ ▀▄█ █ ▀ █ █ ▀ █ █▀█ █▀▄  █ 
-------------------------------";
        }

        public string DisplaySubheadingArrested()
        {
            return @"▄▀▀ █▀▄ ██▀ █   █   █▄ ▄█ ██▀ █▄ ▄█ ██▄ ██▀ █▀▄   ▄▀▄ █▀▄ █▀▄ ██▀ ▄▀▀ ▀█▀ ██▀ █▀▄
▀▄▄ █▀▄ █▄▄ ▀▄▀▄▀   █ ▀ █ █▄▄ █ ▀ █ █▄█ █▄▄ █▀▄   █▀█ █▀▄ █▀▄ █▄▄ ▄██  █  █▄▄ █▄▀
---------------------------------------------------------------------------------";
        }

        public string DisplaySubheadingEscaped()
        {
            return @"██▄ ██▀ █▀ ▄▀▄ █▀▄ ██▀   █▀▄ ▄▀▄ █   █ ▄▀▀ ██▀   ▄▀▄ █▀▄ █▀▄ █ █ █ ██▀ █▀▄
█▄█ █▄▄ █▀ ▀▄▀ █▀▄ █▄▄   █▀  ▀▄▀ █▄▄ █ ▀▄▄ █▄▄   █▀█ █▀▄ █▀▄ █ ▀▄▀ █▄▄ █▄▀
--------------------------------------------------------------------------";
        }

        public string DisplaySubheadingWanted()
        {
            return @"

█   █ ▄▀▄ █▄ █ ▀█▀ ██▀ █▀▄
▀▄▀▄▀ █▀█ █ ▀█  █  █▄▄ █▄▀
";
        }

        public string DisplaySubheadingSpeech()
        {
            return @"
-----------------------
▄▀▀ █▀▄ ██▀ ██▀ ▄▀▀ █▄█
▄██ █▀  █▄▄ █▄▄ ▀▄▄ █ █
-----------------------";
        }

        public string DisplayHouse()
        {
            return @"                    [[[[[[[[\_(X)_/]]]]]
            .-.     /-_--__-/_--_-\-_--\
            |=|    /-_---__/__-__-_\__-_\
        . . |=| ._/-__-__\===========/-__\_
        !!!!!!!!!\========[ /]]|[[\ ]=====/
       /-_--_-_-_[[[[[[[[[||==  == ||]]]]]]
      /-_--_--_--_|=  === ||=/^|^\ ||== =|
     /-_-/^|^\-_--| /^|^\=|| | | | ||^\= |
    /_-_-| | |-_--|=| | | ||=|_|_|=|| |==|
   /-__--|_|_|_-_-| |_|_|=||______=||_| =|
  /_-__--_-__-___-|_=__=_.`---------'._=_|__
 /-----------------------\===========/-----/
^^^\^^^^^^^^^^^^^^^^^^^^^^[[|]]|[[|]]=====/
    |.' ..==::''::==.. '.[ /~~~~~\ ]]]]]]]
    | .'=[[[|]]|[[|]]]=`._||==  =  || =\ ]
    ||== =|/ _____ \|== = ||=/^|^\=||^\ ||
    || == `||-----||' = ==|| | | |=|| |=||
    ||= == ||:^s^:|| = == ||=| | | || |=||
    || = = ||:___:||= == =|| |_|_| ||_|=||
   _||_ = =||o---.|| = ==_||_= == =||==_||_
   \__/= = ||:   :||= == \__/[][][][][]\__/
   [||]= ==||:___:|| = = [||]\\//\\//\\[||]";
        }

    public string Display711()
        {
            return @"                    __________________________
                   ||    7 - Eleven         ||
   ________________||_______________________||_____________
  |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_||
  |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|| /|
  |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_||/||
  |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|||/|
  |_|_|_|_|_|_|_|_|_|     _      _     |_|_|_|_|_|_|_|_|_|_|/||
  |_| candy  drinks |    (_)    (_)    |                 |_|/||
  |_|.              |__________________|                 |_||/|
  |_|*`.            |_|      ||      |_|      GAS        |_|/||
  |_| S `.          |_|      ||      |_|      GAS        |_||/|
  |_|`. A `.        |_|      ||      |_|      GAS        |_|/||
  |_|  `. L `.      |_|     [||]     |_|      GAS        |_||/|
  |_|    `. E `.    |_|      ||      |_|                 |_|/||
  |_|______`__*_`___|_|      ||      |_|_________________|_||/|
  |_|_|_|_|_|_|_|_|_|_|______||______|_|_|_|_|_|_|_|_|_|_|_|/||
  |_|_|_|_|_|_|_|_|_|_|______||______|_|_|_|_|_|_|_|_|_|_|_||/
 /     /     /     /     /     /     /     /     /     /     / 
/_____/_____/_____/_____/_____/_____/_____/_____/_____/_____/";
        }

        public string DisplayWF()
        {
            return @" _____________________________________________
|                 Welts Fargo                |
|____________________________________________|
|__||  ||___||  |_|___|___|__|  ||___||  ||__|
||__|  |__|__|  |___|___|___||  |__|__|  |__||
|__||  ||___||  |_|___|___|__|  ||___||  ||__|
||__|  |__|__|  |    || |    |  |__|__|  |__||
|__||  ||___||  |    || |    |  ||___||  ||__|
||__|  |__|__|  |    || |    |  |__|__|  |__||
|__||  ||___||  |    || |    |  ||___||  ||__|
||__|  |__|__|  |    || |    |  |__|__|  |__||
|__||  ||___||  |   O|| |O   |  ||___||  ||__|
||__|  |__|__|  |    || |    |  |__|__|  |__||
|__||  ||___||  |    || |    |  ||___||  ||__|
||__|  |__|__|__|____||_|____|  |__|__|  |__||
|LLL|  |LLLLL|______________||  |LLLLL|  |LLL|
|LLL|  |LLL|______________|  |  |LLLLL|  |LLL|
|LLL|__|L|______________|____|__|LLLLL|__|LLL|";
        }

        public string DisplayBOA()
        {
            return @"                                   |--|--|--|--|--|--|
                     ______________|__|__|__|__|__|_ |
__              .   /______________________________|-|
__|  .      .   .  //______________________________| :
__|   /|\      _|_|//       ooooooooooooooooooooo  |-|
__|  |/|\|__   ||l|/,-------8                   8 -| |
__|._|/|\|||.l |[=|/,-------8      BANK  OF     8 -|-|
__|[+|-|-||||li|[=|---------8      AMEREEKA     8 -| |
_-----.|/| //:\_[=|\`-------8                   8 -|-|
 /|  /||//8/ :  8_|\`------ 8ooooooooooooooooooo8 -| |
/=| //||/ |  .  | |\\_____________  ____  _________|-|
==|//||  /   .   \ \\_____________ |X|  | _________| `
==| ||  /         \ \_____________ |X| \| _________|  
==| |~ /     .     \
LS|/  /             \________________________ ____________";
        }

        public string DisplayPNB()
        {
            return @"
    [=U=U=U=U=U=U=U=U=U=U=U=U=U=U=U=]
    |.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.|
    |        +-+-+-+-+-+-+-+        |
    |   Pinnackle National Bank     |
    |        +-+-+-+-+-+-+-+        |
    |.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.|
    |  _________  __ __  _________  |
  _ | |         ||[]|[]||  _      | | _
 (!)||| _ _ (!)_|| ,| ,||_(!)_ ___| |(!)
.T~T|:.....:T~T.:|__|__|:.T~T.:....:|T~T.
||_||||||||||_|||||||||||||_||||||||||_||
~\=/~~~~~~~~\=/~~~~~~~~~~~\=/~~~~~~~~\=/~
  | -------- | ----------- | -------- |";
        }

        public string DisplayAssociateRanWithCash()
        {
            return @"                       _________________
  { )_  <-            |              |   \
 |  <<\  _      <--   |  JUST-A-VAN   |____\_____
 ||\  \_//            | _____         |    |_ __ |
($)  // `-`    <--    [/ ___ \       |   / ___ \|
   \\                []_/.-.\_\______|__/_/.-.\_[]
   (/  <--              |(O)|             |(O)|
                         '-'               '-'";
        }

        public string DisplayAssociateRanInFear()
        {
            return @"                       _________________
  { )_  <-            |              |   \
 |  <<\  _       <--  |  JUST-A-VAN   |____\_____
 ||\  \_//            | _____        |    |_ __ |
 -  // `-`    <--     [/ ___ \       |   / ___ \|
   \\                []_/.-.\_\______|__/_/.-.\_[]
   (/  <--              |(O)|             |(O)|
                         '-'               '-'";
        }

        public string DisplayTitle()
        {
            return @"

  ______    __  __        __    __          __            __              
 /      \  |  \|  \      |  \  |  \        |  \          |  \             
|  ▓▓▓▓▓▓\_| ▓▓| ▓▓_     | ▓▓  | ▓▓ ______  \▓▓ _______ _| ▓▓_    _______ 
| ▓▓   \▓▓   ▓▓  ▓▓ \    | ▓▓__| ▓▓/      \|  \/       \   ▓▓ \  /       \
| ▓▓      \▓▓▓▓▓▓▓▓▓▓    | ▓▓    ▓▓  ▓▓▓▓▓▓\ ▓▓  ▓▓▓▓▓▓▓\▓▓▓▓▓▓ |  ▓▓▓▓▓▓▓
| ▓▓   __|   ▓▓  ▓▓ \    | ▓▓▓▓▓▓▓▓ ▓▓    ▓▓ ▓▓\▓▓    \  | ▓▓ __ \▓▓    \ 
| ▓▓__/  \\▓▓▓▓▓▓▓▓▓▓    | ▓▓  | ▓▓ ▓▓▓▓▓▓▓▓ ▓▓_\▓▓▓▓▓▓\ | ▓▓|  \_\▓▓▓▓▓▓\
 \▓▓    ▓▓ | ▓▓| ▓▓      | ▓▓  | ▓▓\▓▓     \ ▓▓       ▓▓  \▓▓  ▓▓       ▓▓
  \▓▓▓▓▓▓   \▓▓ \▓▓       \▓▓   \▓▓ \▓▓▓▓▓▓▓\▓▓\▓▓▓▓▓▓▓    \▓▓▓▓ \▓▓▓▓▓▓▓                                                                                     


 ▄▀▄ █▄ █ ██▀   █▀▄ ▄▀▄ ▀▄▀     █▀ █ █ █ ██▀   █▄█ ██▀ █ ▄▀▀ ▀█▀ ▄▀▀     ▄▀▄ █▄ █ ██▀   ▄▀▀ █▀▄ █   █ ▀█▀
 ▀▄▀ █ ▀█ █▄▄   █▄▀ █▀█  █  █   █▀ █ ▀▄▀ █▄▄   █ █ █▄▄ █ ▄██  █  ▄██ █   ▀▄▀ █ ▀█ █▄▄   ▄██ █▀  █▄▄ █  █ 

---------------------------------------------------------------------------------------------------------";
        }

        public string DisplayGun()
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

        public string DisplayMoneyBag()
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

        public String DisplayEndingBeach()
        {
            return @"
You had enough cash to leave the country and live a new, secret life.

              ,.  _~-.,               
           ~'`_ \/,_. \_
          / ,'_>@`,__`~.)                  
          | |  @@@@'  ',!                      
          |/   ^^@     .!                     
          `' .^^^     ,'              |        .             
           .^^^                               /          
          .^^^                \       |      /        
.,.,.     ^^^             ` .   .,+~'`^`'~+,.      '
&&&&&&,  ,^^^^.  . ._ ..__ _  .'             '. '_ __ ____ __ _ .. .  .
%%%%%%%%%^^^^^^%%&&;_,.-=~'`^`'~=-.,__,.-=~'`^`'~=-.,_    `^`'~=-.,
&&&&&%%%%%%%%%%%%%%%%%%&&;,.-=~         .,__,.-    -.    ,.        ~=
%%%%%&&&&&&&&&&&%%%%&&&_,.;^`'~=-   .-=~'`^`'~=    -.,__,.-=~'`^`'~=-.,__,
%%%%%%%%%&&&&&&&&&-=~'`^`'~      _,.-=~'`^`'               ,__,.-=~'
##mjy#####*''
_           ~=-.,__,.-=~         ~'`^`'~=-.,.-=~'`^`'~=-.,__,.-=~'

~`'^`'~=-.,__,.-=~'`^`'~=-.,__,.-=~'`^`             =~'`^`'~=-.,__,.-=~'`^";
        }

        public string DisplayEndingRoad()
        {
            return @"
You escaped with enough cash to travel west.
It's a big country. Hopefully you'll find somewhere to hide.

                                               
                 ___                          (_)
               _/XXX\
_             /XXXXXX\_                                    __
X\__    __   /X XXXX XX\                          _       /XX\__      ___
    \__/  \_/__       \ \                       _/X\__   /XX XXX\____/XXX\
  \  ___   \/  \_      \ \               __   _/      \_/  _/  -   __  -  \__/
 ___/   \__/   \ \__     \\__           /  \_//  _ _ \  \     __  /  \____//
/  __    \  /     \ \_   _//_\___     _/    //           \___/  \/     __/
__/_______\________\__\_/________\_ _/_____/_____________/_______\____/_______
                                  /|\
                                 / | \
                                /  |  \
                               /   |   \
                              /    |    \
                             /     |     \";
        }

        public string DisplayEndingCamp()
        {
            return @"
You had just enough cash to get out of town.
Hopefully you can hide in the woods long enough for people to forget your crimes.

           (                 ,---.
            )                .,.||
           (  (              \=__/
               )             ,'-'.
         (    (  ,,      _.__|/ /|
          ) /\ -((------((_|___/ |
        (  // | (`'      ((  `'--|
      _ -.;_/ \\--._      \\ \-._/.
     (_;-// | \ \-'.\    <_,\_\`--'|
     ( `.__ _  ___,')      <_,-'__,'
      `'(_ )_)(_)_)'";
        }
    }
}

// When I add guards, show this message. The muscle will have to deal with this punk
//                       ________________
//                       \      __      /         __
//                        \_____()_____/         /  )
//                        '============`        /  /
//                         #---\  /---#        /  /
//                        (# @\| |/@  #)      /  /
//                         \   (_)   /       /  /
//                         |\ '---` /|      /  /
//                 _______/ \\_____// \____/ o_|
//                /       \  /     \  /   / o_|
//               / |           o|        / o_| \
//              /  |  _____     |       / /   \ \
//             /   |  |===|    o|      / /\    \ \
//            |    |   \@/      |     / /  \    \ \
//            |    |___________o|__/----)   \    \/
//            |    '              ||  --)    \     |
//            |___________________||  --)     \    /
//                 |           o|   ''''   |   \__/
//                 |            |          |

//                   "DON'T CROSS ME... !"
// Rosebud