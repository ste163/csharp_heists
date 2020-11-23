using System;

namespace heist
{
    public class ASCII
    {

        public string DisplayCrewHeading()
        {
            return @"
-------------------------------
▀█▀ █▄█ ██▀   ▄▀▀ █▀▄ ██▀ █   █
 █  █ █ █▄▄   ▀▄▄ █▀▄ █▄▄ ▀▄▀▄▀
-------------------------------";
        }
        public string DisplayCrewHire()
        {
            return @"
---------------------------------
█▄█ █ █▀▄ ██▀   ▄▀▀ █▀▄ ██▀ █   █
█ █ █ █▀▄ █▄▄   ▀▄▄ █▀▄ █▄▄ ▀▄▀▄▀                   
---------------------------------";
        }

        public string DisplayCriminalFace()
        {
            int r = new Random().Next(1, 8);
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
,-'      `.
/           \
| __         |
\   \__  -.  |
( o( o) 7/ /
/  /     -|/
\ (_     / |
\ --.  /  |
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
| /`|/=*===*=| (
/`/` /|<o>)(o>( 
\  \/\|    /  /
\`/` \/\ `=' /
|/\/` \/`._.'
|/";
                    break;
            }

            return face;
        }

        public string DisplayPlanning()
        {
          return @"
--------------------------------
█▀▄ █   ▄▀▄ █▄ █ █▄ █ █ █▄ █ ▄▀ 
█▀  █▄▄ █▀█ █ ▀█ █ ▀█ █ █ ▀█ ▀▄█
--------------------------------
";  
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
            return @"
--------------------------------------------------------------------.
| .--                    FEDERAL RESERVE NOTE                    .-- |
| |_       ......    THE UNTIED STATES OF AMERICA                |_  |
| __)    ``````````             ______            B93810455B     __) |
|      2        ___            /      \                     2        |
|              /|~\\          /  _-\\  \           __ _ _ _  __      |
|             | |-< |        |  //   \  |         |_  | | | |_       |
|              \|_//         | |-  o o| |         |   | `.' |__      |
|               ~~~          | |\   b.' |                            |
|       B83910455B           |  \ '~~|  |                            |
| .--  2                      \_/ ```__/    ....            2    .-- |
| |_        ///// ///// ////   \__\'`\/      ``  //// / ////     |_  |
| __)                   M A J O R  D O L L A R S                   __) |
`--------------------------------------------------------------------'
";
// Thank you for visiting https://asciiart.website/
// This ASCII pic can be found at
// https://asciiart.website/index.php?art=objects/money

        }

        public string DisplayArrested()
        {
            return @"
================================
||     ||<(.)>||<(.)>||     || 
||    _||     ||     ||_    || 
||   (__D     ||     C__)   || 
||   (__D     ||     C__)   ||
||   (__D     ||     C__)   ||
||   (__D     ||     C__)   ||
||     ||     ||     ||     ||
================================    
";
        }
        public string DisplayHouse()
        {
            return @"
                            .     .
                            !!!!!!!
                    .       [[[|]]]    .
                    !!!!!!!!|--_--|!!!!!
                    [[[[[[[[\_(X)_/]]]]]
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
    }
}


// Thank you for visiting https://asciiart.website/
// This ASCII pic can be found at
// https://asciiart.website/index.php?art=people/faces


// // Bank of America
// ____________________________________________
// |____________________________________________|
// |__||  ||___||  |_|___|___|__|  ||___||  ||__|
// ||__|  |__|__|  |___|___|___||  |__|__|  |__||
// |__||  ||___||  |_|___|___|__|  ||___||  ||__|
// ||__|  |__|__|  |    || |    |  |__|__|  |__||
// |__||  ||___||  |    || |    |  ||___||  ||__|
// ||__|  |__|__|  |    || |    |  |__|__|  |__||
// |__||  ||___||  |    || |    |  ||___||  ||__|
// ||__|  |__|__|  |    || |    |  |__|__|  |__||
// |__||  ||___||  |   O|| |O   |  ||___||  ||__|
// ||__|  |__|__|  |    || |    |  |__|__|  |__||
// |__||  ||___||  |    || |    |  ||___||  ||__|
// ||__|  |__|__|__|____||_|____|  |__|__|  |__||
// |LLL|  |LLLLL|______________||  |LLLLL|  |LLL|
// |LLL|  |LLL|______________|  |  |LLLLL|  |LLL|
// |LLL|__|L|______________|____|__|LLLLL|__|LLL|


// // 7 -Eleven
//                     __________________________
//                    ||    7 - Eleven         ||
//    ________________||_______________________||_____________
//   |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_||
//   |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|| /|
//   |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_||/||
//   |_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|||/|
//   |_|_|_|_|_|_|_|_|_|     _      _     |_|_|_|_|_|_|_|_|_|_|/||
//   |_| candy  drinks |    (_)    (_)    |                 |_|/||
//   |_|.              |__________________|                 |_||/|
//   |_|*`.            |_|      ||      |_|                 |_|/||
//   |_| S `.          |_|      ||      |_|                 |_||/|
//   |_|`. A `.        |_|      ||      |_|                 |_|/||
//   |_|  `. L `.      |_|     [||]     |_|                 |_||/|
//   |_|    `. E `.    |_|      ||      |_|                 |_|/||
//   |_|______`__*_`___|_|      ||      |_|_________________|_||/|
//   |_|_|_|_|_|_|_|_|_|_|______||______|_|_|_|_|_|_|_|_|_|_|_|/||
// __|_|_|_|_|_|_|_|_|_|_|______||______|_|_|_|_|_|_|_|_|_|_|_||/________
//  /     /     /     /     /     /     /     /     /     /     /     /
// /_____/_____/_____/_____/_____/_____/_____/_____/_____/_____/_____/